using System;
using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Config;
using TwitchChatConnect.Data;
using TwitchChatConnect.Manager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Tokki sagwa;
	[SerializeField] private Tokki momoe;
	private void Start()
	{
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

	private void ShowReward(TwitchChatReward chatReward)
	{
		throw new NotImplementedException();
	}

	private void ShowCommand(TwitchChatCommand chatCommand)
	{
		throw new NotImplementedException();
	}

	private void ShowMessage(TwitchChatMessage chatMessage)
	{
		if(chatMessage.User.DisplayName == "Sagwacito")
		{
			sagwa.gameObject.SetActive(true);
		}
		else if(chatMessage.User.DisplayName=="mrmoemoekyun")
		{
			momoe.gameObject.SetActive(true);
		}
		/*string message =
		   $"Message by {chatMessage.User.DisplayName} - " +
		   $"Bits: {chatMessage.Bits} - " +
		   $"Sub: {chatMessage.User.IsSub} - " +
		   $"Badges count: {chatMessage.User.Badges.Count} - " +
		   $"Badges: {string.Join("/", chatMessage.User.Badges.Select(badge => badge.Name))} - " +
		   $"Badge versions: {string.Join("/", chatMessage.User.Badges.Select(badge => badge.Version))} - " +
		   $"Is highlighted: {chatMessage.IsHighlighted} - " +
		   $"Message: {chatMessage.Message}";*/
		TwitchChatClient.instance.SendWhisper(chatMessage.User.Username, "Thanks for your message!");
	}
}
