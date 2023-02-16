using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tokki : MonoBehaviour
{
	[SerializeField] private Image tokkiImage;
	[SerializeField] private Image check;
	public TextMeshProUGUI username;
	public bool answer;

	public void SetSprite()
	{
		int randomId = Random.Range(1, 26);
		tokkiImage.sprite = Resources.Load<Sprite>($"BunnyPNG/bunny ({randomId})");
	}
	public void SetName(string username)
	{
		this.username.text = username;
	}
	public void SetChoice(bool answer)
	{
		this.answer = answer;
	}
	public void Check(bool hadAnswered)
	{
		check.gameObject.SetActive(hadAnswered);
	}
}
