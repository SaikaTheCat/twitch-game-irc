using System;
using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using TwitchChatConnect.Manager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Tokki tokki;
	[SerializeField] private Transform tokkisContainer;

	private List<string> existingUsers = new List<string>();
	private List<Tokki> tempTokkiList = new List<Tokki>();

	public bool hadStarted = false;
	private void Start()
	{
		UIManager uIManager = FindObjectOfType<UIManager>();
		InitializeUI(uIManager);
		TwitchChatClient.instance.Init(() =>
		{
			Debug.Log("Connected!");
			TwitchChatClient.instance.onChatMessageReceived += ShowMessage;
			TwitchChatClient.instance.onChatCommandReceived += ShowCommand;
			TwitchChatClient.instance.onChatRewardReceived += ShowReward;

			TwitchUserManager.OnUserAdded += twitchUser =>
			{
				Debug.Log($"{twitchUser.Username} has connected to the chat.");
			};

			TwitchUserManager.OnUserRemoved += username =>
			{
				Debug.Log($"{username} has left the chat.");
			};
		},
			message =>
			{
				// Error when initializing.
				Debug.LogError(message);
			});
	}

	private void InitializeUI(UIManager uIManager)
	{
		AudioSource audioSource = FindObjectOfType<AudioSource>();
		if (uIManager != null)
		{
			uIManager.SetStartButtonAction(() =>
			{
				audioSource.PlayOneShot(uIManager.StartClickAudio);
				hadStarted = true;
				uIManager.StartGame();
			});
			uIManager.SetYesButtonAction(() =>
			{
				audioSource.PlayOneShot(uIManager.StartClickAudio);
				StartCoroutine(DestroyTokkis(true, uIManager));
			});
			uIManager.SetNoButtonAction(() =>
			{
				audioSource.PlayOneShot(uIManager.StartClickAudio);
				StartCoroutine(DestroyTokkis(false, uIManager));
			});
		}
	}

	IEnumerator DestroyTokkis(bool isYes, UIManager uIManager)
	{
		AudioSource audioSource = FindObjectOfType<AudioSource>();
		List<Tokki> tokkisToRemove = new List<Tokki>();
		foreach (var tokki in tempTokkiList)
		{
			if (tokki.answer != isYes)
			{
				tokkisToRemove.Add(tokki);
				audioSource.PlayOneShot(uIManager.PewAudio);
				tokki.gameObject.SetActive(false);
			}
			yield return new WaitForSeconds(1f);
		}
		foreach (var tokki in tokkisToRemove)
		{
			tempTokkiList.Remove(tokki);
			existingUsers.Remove(tokki.username.text);
		}
	}

	private void ShowReward(TwitchChatReward chatReward)
	{
		throw new NotImplementedException();
	}

	private void ShowCommand(TwitchChatCommand chatCommand)
	{
		if (chatCommand.Command == "!join" && !hadStarted)
		{
			Debug.Log("entra aca");
			if (existingUsers.Contains(chatCommand.User.DisplayName))
			{
				//return if user is repeated
				return;
			}
			if (chatCommand.User.IsBroadcaster)
			{
				return;
			}
			existingUsers.Add(chatCommand.User.DisplayName);
			var bunny = Instantiate(tokki, tokkisContainer) as Tokki;
			tempTokkiList.Add(bunny);
			bunny.SetName(chatCommand.User.DisplayName);
			bunny.SetSprite();
		}
		if(hadStarted && existingUsers.Contains(chatCommand.User.DisplayName))
		{
			foreach (var tokki in tempTokkiList)
			{
				if (tokki.username.text.Equals(chatCommand.User.DisplayName))
				{
					switch (chatCommand.Command)
					{
						case "!si":
							tokki.SetChoice(true);
							tokki.Check(true);
							break;
						case "!no":
							tokki.SetChoice(false);
							tokki.Check(true);
							break;
					}
				}
			}
		}

	}

	private void ShowMessage(TwitchChatMessage chatMessage)
	{
		if (existingUsers.Contains(chatMessage.User.DisplayName))
		{
			//return if user is repeated
			return;
		}
		/*if (chatMessage.User.DisplayName == "Sagwacito")
		{
			sagwa.gameObject.SetActive(true);
		}
		else if (chatMessage.User.DisplayName == "mrmoemoekyun")
		{
			momoe.gameObject.SetActive(true);
		}*/
		/*string message =
		   $"Message by {chatMessage.User.DisplayName} - " +
		   $"Bits: {chatMessage.Bits} - " +
		   $"Sub: {chatMessage.User.IsSub} - " +
		   $"Badges count: {chatMessage.User.Badges.Count} - " +
		   $"Badges: {string.Join("/", chatMessage.User.Badges.Select(badge => badge.Name))} - " +
		   $"Badge versions: {string.Join("/", chatMessage.User.Badges.Select(badge => badge.Version))} - " +
		   $"Is highlighted: {chatMessage.IsHighlighted} - " +
		   $"Message: {chatMessage.Message}";*/
		//TwitchChatClient.instance.SendWhisper(chatMessage.User.Username, "Thanks for your message!");
	}
}
