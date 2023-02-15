using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tokki : MonoBehaviour
{
	[SerializeField] private Image tokkiImage;
	[SerializeField] private TextMeshProUGUI username;

	public void SetSprite()
	{
		int randomId = Random.Range(1, 26);
		tokkiImage.sprite = Resources.Load<Sprite>($"BunnyPNG/bunny ({randomId})");
	}
	public void SetName(string username)
	{
		this.username.text = username;
	}
}
