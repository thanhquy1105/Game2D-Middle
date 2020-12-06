using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

	// score and coin text in scene
	public Text scoreText, coinText;

	// keeping track which level is unlocked
	public bool[] levels;

	// reference to level buttons in scene
	public Button[] levelButtons;

	// reference to level texts in scene
	public Text[] levelText;

	// reference to lock images in scene
	public Image[] lockIcons;

	public GameObject coinShopPanel;

	// Use this for initialization
	void Start()
	{
		InitializeLevelMenu();
	}

	void InitializeLevelMenu()
	{

		scoreText.text = "" + GameController.instance.highScore;
		coinText.text = "" + GameController.instance.coins;

		levels = GameController.instance.levels;

		for (int i = 1; i < levels.Length; i++)
		{

			if (levels[i])
			{
				lockIcons[i - 1].gameObject.SetActive(false);
			}
			else
			{
				levelButtons[i - 1].interactable = false;
				levelText[i - 1].gameObject.SetActive(false);
			}

		}


	}

	public void LoadLevel()
	{

		if (GameController.instance.isMusicOn)
		{
			MusicController.instance.GameIsLoadedTurnOfMusic();
		}

		string level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

		switch (level)
		{

			case "Level0":
				GameController.instance.currentLevel = 0;
				break;

			case "Level1":
				GameController.instance.currentLevel = 1;
				break;

			case "Level2":
				GameController.instance.currentLevel = 2;
				break;

			case "Level3":
				GameController.instance.currentLevel = 3;
				break;

			case "Level4":
				GameController.instance.currentLevel = 4;
				break;

			case "Level5":
				GameController.instance.currentLevel = 5;
				break;

			case "Level6":
				GameController.instance.currentLevel = 6;
				break;

			case "Level7":
				GameController.instance.currentLevel = 7;
				break;

			case "Level8":
				GameController.instance.currentLevel = 8;
				break;

			case "Level9":
				GameController.instance.currentLevel = 9;
				break;

			case "Level10":
				GameController.instance.currentLevel = 10;
				break;

			case "Level11":
				GameController.instance.currentLevel = 1;
				break;

			case "Level12":
				GameController.instance.currentLevel = 12;
				break;

			case "Level13":
				GameController.instance.currentLevel = 13;
				break;

			case "Level14":
				GameController.instance.currentLevel = 14;
				break;

			case "Level15":
				GameController.instance.currentLevel = 15;
				break;

			case "Level16":
				GameController.instance.currentLevel = 16;
				break;

			case "Level17":
				GameController.instance.currentLevel = 17;
				break;

			case "Level18":
				GameController.instance.currentLevel = 18;
				break;

			case "Level19":
				GameController.instance.currentLevel = 19;
				break;

		} // switch
		Debug.Log("1");
		LoadingScreen.instance.PlayLoadingScreen();
		GameController.instance.isGameStartedFromLevelMenu = true;
		//		Application.LoadLevel (level);
		Application.LoadLevel("Level Setup Scene");

	}

	public void OpenCoinShop()
	{
		coinShopPanel.SetActive(true);
	}

	public void CloseCoinShop()
	{
		coinShopPanel.SetActive(false);
	}

	public void GoToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void GoBackButton()
	{
		Application.LoadLevel("PlayerMenu");
	}



} // level controller
