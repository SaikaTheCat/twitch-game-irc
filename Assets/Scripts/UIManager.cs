using TMPro;
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
	[SerializeField] private TextMeshProUGUI roundText;
	[SerializeField] private AudioClip startClickAudio;
	[SerializeField] private AudioClip quizBGAudio;
	[SerializeField] private AudioClip pewAudio;
	[SerializeField] private AudioClip winAudio;
	[SerializeField] private AudioClip defeatedAudio;
	[SerializeField] private Canvas gameCanvas;
	[SerializeField] private Canvas winCanvas;
	[SerializeField] private Canvas defeatedCanvas;
	[SerializeField] private TextMeshProUGUI winnerText;
	[SerializeField] private Button quitButton;

	public AudioClip StartClickAudio { get => startClickAudio; }
	public AudioClip PewAudio { get => pewAudio; }

	public void SetRound(int round)
	{
		roundText.text = $"Round {round}";
	}
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
	public void SetTokkiKiller(bool isKillingTime)
	{
		inGameBigTokki.SetActive(!isKillingTime);
		killerBigTokki.SetActive(isKillingTime);
	}
	public void SetNoSurvivors()
	{
		AudioSource audioSource = FindObjectOfType<AudioSource>();
		audioSource.clip = defeatedAudio;
		audioSource.Play();
		gameCanvas.gameObject.SetActive(false);
		defeatedCanvas.gameObject.SetActive(true);
	}
	public void SetWinner(string winner)
	{
		AudioSource audioSource = FindObjectOfType<AudioSource>();
		audioSource.loop = false;
		audioSource.clip = winAudio;
		audioSource.Play();
		gameCanvas.gameObject.SetActive(false);
		winCanvas.gameObject.SetActive(true);
		winnerText.text = $"EL GANADOR ES:\n{winner}";
	}
	public void QuitGame()
	{
		Application.Quit();
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
		roundText.gameObject.SetActive(true);
		AudioSource audioSource = FindObjectOfType<AudioSource>();
		audioSource.clip = quizBGAudio;
		audioSource.Play();
	}
}
