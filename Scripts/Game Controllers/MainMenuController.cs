using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

	[SerializeField]
	private Animator settingsButtonsAnim;

	private bool hidden;
	private bool canTouchSettingsButton;

	[SerializeField]
	private Button musicBtn;

	[SerializeField]
	private Sprite[] musicBtnSprites;

	[SerializeField]
	private Button fbBtn;

	[SerializeField]
	private Sprite[] fbSprites;

	[SerializeField]
	private GameObject infoPanel;

	[SerializeField]
	private Image infoImage;

	[SerializeField]
	private Sprite[] infoSprites;

	private int infoIndex;

	// Use this for initialization
	void Start()
	{
		canTouchSettingsButton = true;
		hidden = true;

		if (GameController.instance.isMusicOn)
		{
			MusicController.instance.PlayBgMusic();
			musicBtn.image.sprite = musicBtnSprites[0];
		}
		else
		{
			MusicController.instance.StopBgMusic();
			musicBtn.image.sprite = musicBtnSprites[1];
		}

		infoIndex = 0;
		infoImage.sprite = infoSprites[infoIndex];

	}

	public void SettingsButton()
	{
		StartCoroutine(DisableSettingsButtonWhilePlayingAnimation());
	} // sittings button

	IEnumerator DisableSettingsButtonWhilePlayingAnimation()
	{

		if (canTouchSettingsButton)
		{

			if (hidden)
			{
				canTouchSettingsButton = false;
				settingsButtonsAnim.Play("SlideIn");
				hidden = false;
				yield return new WaitForSeconds(1.2f);
				canTouchSettingsButton = true;

			}
			else
			{
				canTouchSettingsButton = false;
				settingsButtonsAnim.Play("SlideOut");
				hidden = true;
				yield return new WaitForSeconds(1.2f);
				canTouchSettingsButton = true;
			}

		}

	} // disable settings button while playing animation


	public void MusicButton()
	{
		if (GameController.instance.isMusicOn)
		{
			musicBtn.image.sprite = musicBtnSprites[1];
			MusicController.instance.StopBgMusic();
			GameController.instance.isMusicOn = false;
			GameController.instance.Save();
			Debug.Log(GameController.instance.isMusicOn);
		}
		else
		{
			musicBtn.image.sprite = musicBtnSprites[0];
			MusicController.instance.PlayBgMusic();
			GameController.instance.isMusicOn = true;
			GameController.instance.Save();
			Debug.Log(GameController.instance.isMusicOn);
		}
		
	} // music button


	public void OpenInfoPanel()
	{
		infoPanel.SetActive(true);
	} // open info panel

	public void CloseInfoPanel()
	{
		infoPanel.SetActive(false);
	} // close info panel

	public void NextInfo()
	{
		infoIndex++;

		if (infoIndex == infoSprites.Length)
		{
			infoIndex = 0;
		}

		infoImage.sprite = infoSprites[infoIndex];
	} // next info

	public void PlayButton()
	{
		MusicController.instance.PlayClickClip ();
		Application.LoadLevel("PlayerMenu");
	}

	public void ShopButton()
	{
		MusicController.instance.PlayClickClip();
		Application.LoadLevel("ShopMenu");
	}

} // main menu controller
