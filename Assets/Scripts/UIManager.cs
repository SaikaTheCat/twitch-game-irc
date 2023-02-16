using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject startingBigTokki;
	[SerializeField] private GameObject inGameBigTokki;
	[SerializeField] private GameObject killerBigTokki;
	[SerializeField] private Button startButton;
	[SerializeField] private Button yesButton;
	[SerializeField] private Button noButton;
	[SerializeField] private GameObject joinInstruction;
	[SerializeField] private GameObject gameInstruction;
	[SerializeField] private AudioClip startClickAudio;
	[SerializeField] private AudioClip quizBGAudio;
	[SerializeField] private AudioClip pewAudio;

	public AudioClip StartClickAudio { get => startClickAudio; }
	public AudioClip PewAudio { get => pewAudio; }

	public void SetStartButtonAction(UnityAction unityAction)
	{
		startButton.onClick.AddListener(unityAction);
	}
	public void SetYesButtonAction(UnityAction unityAction)
	{
		yesButton.onClick.AddListener(unityAction);
	}
	public void SetNoButtonAction(UnityAction unityAction)
	{
		noButton.onClick.AddListener(unityAction);
	}
	public void StartGame()
	{
		startingBigTokki.SetActive(false);
		inGameBigTokki.SetActive(true);
		startButton.gameObject.SetActive(false);
		yesButton.gameObject.SetActive(true);
		noButton.gameObject.SetActive(true);
		joinInstruction.SetActive(false);
		gameInstruction.SetActive(true);
		AudioSource audioSource = FindObjectOfType<AudioSource>();
		audioSource.clip = quizBGAudio;
		audioSource.Play();
	}
}
