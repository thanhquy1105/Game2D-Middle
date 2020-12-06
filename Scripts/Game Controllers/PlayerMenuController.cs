using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour
{

	// score and coin text
	public Text scoreText, coinText;

	// keeping track which player is unlocked
	public bool[] players;

	// keeping track which weapon is unlocked
	public bool[] weapons;

	// reference to price tags in scene
	public Image[] priceTags;

	// reference to weapon icons in the scene
	public Image[] weaponIcons;

	// reference to weapon sprites to change the weapon icons in the scene
	public Sprite[] weaponArrows;

	// keeping track which weapon is selected on player
	public int selectedWeapon;

	// keeping track which player is selected
	public int selectedPlayer;

	// reference to buy player panel in scene
	public GameObject buyPlayerPanel;

	// reference to yes button from buy player panel
	public Button yesBtn;

	// reference to buy player text in the scene
	public Text buyPlayerText;

	public GameObject coinShop;

	// Use this for initialization
	void Start()
	{
		InitializePlayerMenuController();
	}

	void InitializePlayerMenuController()
	{

		scoreText.text = "" + GameController.instance.highScore;
		coinText.text = "" + GameController.instance.coins;

		players = GameController.instance.players;
		weapons = GameController.instance.weapons;

		selectedPlayer = GameController.instance.selectedPlayer;
		selectedWeapon = GameController.instance.selectedWeapon;

		for (int i = 0; i < weaponIcons.Length; i++)
		{
			weaponIcons[i].gameObject.SetActive(false);
		}

		for (int i = 1; i < players.Length; i++)
		{
			if (players[i] == true)
			{
				priceTags[i - 1].gameObject.SetActive(false);
			}
		}

		weaponIcons[selectedPlayer].gameObject.SetActive(true);
		weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

	}

	public void Player1Button()
	{

		if (selectedPlayer != 0)
		{

			selectedPlayer = 0;
			selectedWeapon = 0;

			weaponIcons[selectedPlayer].gameObject.SetActive(true);
			weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

			for (int i = 0; i < weaponIcons.Length; i++)
			{
				if (i == selectedPlayer)
					continue;

				weaponIcons[i].gameObject.SetActive(false);
			}

			GameController.instance.selectedPlayer = selectedPlayer;
			GameController.instance.selectedWeapon = selectedWeapon;
			GameController.instance.Save();

		}
		else
		{

			selectedWeapon++;

			if (selectedWeapon == weapons.Length)
				selectedWeapon = 0;

			bool foundWeapon = true;

			while (foundWeapon)
			{

				if (weapons[selectedWeapon] == true)
				{
					weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
					GameController.instance.selectedWeapon = selectedWeapon;
					GameController.instance.Save();
					foundWeapon = false;
				}
				else
				{
					selectedWeapon++;

					if (selectedWeapon == weapons.Length)
						selectedWeapon = 0;
				}

			}

		}

	}

	public void PiratePlayerButton()
	{

		if (players[1] == true)
		{

			if (selectedPlayer != 1)
			{

				selectedPlayer = 1;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Player?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(1));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // pirate player

	public void ZombiePlayerButton()
	{

		if (players[2] == true)
		{

			if (selectedPlayer != 2)
			{

				selectedPlayer = 2;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Player?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(2));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // Zombie player

	public void HomosapienPlayerButton()
	{

		if (players[3] == true)
		{

			if (selectedPlayer != 3)
			{

				selectedPlayer = 3;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Player?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(3));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // Homosapien player

	public void JokerPlayerButton()
	{

		if (players[4] == true)
		{

			if (selectedPlayer != 4)
			{

				selectedPlayer = 4;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Player?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(4));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // Joker player

	public void SpartanPlayerButton()
	{

		if (players[5] == true)
		{

			if (selectedPlayer != 5)
			{

				selectedPlayer = 5;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Player?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(4));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // Spartan player

	public void BuyPlayer(int index)
	{

		GameController.instance.players[index] = true;
		GameController.instance.coins -= 7000;
		GameController.instance.Save();
		InitializePlayerMenuController();

		buyPlayerPanel.SetActive(false);

	}

	public void CloseCoinShop()
	{
		coinShop.SetActive(false);
	}

	public void OpenCoinShop()
	{

		if (buyPlayerPanel.activeInHierarchy)
		{
			buyPlayerPanel.SetActive(false);
		}

		coinShop.SetActive(true);

	}

	public void DontBuyPlayerOrCoins()
	{
		buyPlayerPanel.SetActive(false);
	}

	public void GoToLevelMenu()
	{
		Application.LoadLevel("LevelMenu");
	}

	public void GoToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}


} // player menu controller
