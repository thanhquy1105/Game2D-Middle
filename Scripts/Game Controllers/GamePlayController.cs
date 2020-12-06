using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{

	// gameplay controller instance
	public static GamePlayController instance;

	// BG bricks that will be created when the game beggins
	[SerializeField]
	private GameObject[] topAndBottomBricks, leftBricks, rightBricks;

	// pause, level finished and player died panel, and bg panel 
	public GameObject panelBG, levelFinishedPanel, playerDiedPanel, pausePanel;

	// reference to all bricks that will be created in game
	private GameObject topBrick, bottomBrick, leftBrick, rightBrick;

	// coordinates that will be used to position BG bricks
	private Vector3 coordinates;

	// reference to our players because gameplay controller will initialize a player the user selects
	[SerializeField]
	private GameObject[] players;

	// level time in game
	public float levelTime;

	// reference to text's in game, lives, score, level timer, show score at the end of level, countdown, and watch video text
	public Text liveText, scoreText, levelTimerText, showScoreAtTheEndOfLevelText, countDownAndBeginLevelText, watchVideoText;

	// countdown before level begins
	private float countDownBeforeLevelBegins = 3.0f;

	// small balls count - when all small balls are killed level ends
	public static int smallBallsCount = 0;

	// player lives, score and coins
	public int playerLives, playerScore, coins;

	// boolean variables to know when the game is paused, has the level began, is level in progress and when to countdown level
	private bool isGamePaused, hasLevelBegan, countdownLevel;

	public bool levelInProgress;

	// items that will be created at the end of the level so that the user can collect them
	[SerializeField]
	private GameObject[] endOfLevelRewards;

	[SerializeField]
	private Button pauseBtn;

	void Awake()
	{
		CreateInstance();
		InitializeBricksAndPlayer();
	}

	// Use this for initialization
	void Start()
	{
		InitializeGameplayController();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateGameplayController();
	}

	// creating an instance of our class
	void CreateInstance()
	{
		if (instance == null)
			instance = this;
	}

	void InitializeBricksAndPlayer()
	{

		coordinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

		int index = Random.Range(0, topAndBottomBricks.Length);

		topBrick = Instantiate(topAndBottomBricks[index]);
		bottomBrick = Instantiate(topAndBottomBricks[index]);
		leftBrick = Instantiate(leftBricks[index], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
		rightBrick = Instantiate(rightBricks[index], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;

		topBrick.tag = "TopBrick";

		topBrick.transform.position = new Vector3(-coordinates.x + 8, coordinates.y + 0.17f, 0);
		bottomBrick.transform.position = new Vector3(-coordinates.x + 8, -coordinates.y - 0.17f, 0);
		leftBrick.transform.position = new Vector3(-coordinates.x - 0.17f, coordinates.y + 5, 0);
		rightBrick.transform.position = new Vector3(coordinates.x + 0.17f, coordinates.y + 5, 0);

		Instantiate(players[GameController.instance.selectedPlayer]);

	}

	void InitializeGameplayController()
	{

		if (GameController.instance.isGameStartedFromLevelMenu)
		{
			playerScore = 0;
			playerLives = 2;
			GameController.instance.currentScore = playerScore;
			GameController.instance.currentLives = playerLives;
			GameController.instance.isGameStartedFromLevelMenu = false;
		}
		else
		{
			playerScore = GameController.instance.currentScore;
			playerLives = GameController.instance.currentLives;
		}

		levelTimerText.text = levelTime.ToString("F0");
		scoreText.text = "Score x" + playerScore;
		liveText.text = "x" + playerLives;

		Time.timeScale = 0;
		countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");

	}

	void UpdateGameplayController()
	{

		scoreText.text = "Score x" + playerScore;

		if (hasLevelBegan)
		{
			CountdownAndBeginLevel();
		}

		if (countdownLevel)
		{
			LevelCountdownTimer();
		}

	}

	public void setHasLevelBegan(bool hasLevelBegan)
	{
		this.hasLevelBegan = hasLevelBegan;
	}

	void CountdownAndBeginLevel()
	{
		countDownBeforeLevelBegins -= (0.19f * 0.15f);
		countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");
		if (countDownBeforeLevelBegins <= 0)
		{
			Time.timeScale = 1;
			hasLevelBegan = false;
			levelInProgress = true;
			countdownLevel = true;
			countDownAndBeginLevelText.gameObject.SetActive(false);
		}
	}

	void LevelCountdownTimer()
	{

		if (Time.timeScale == 1)
		{

			levelTime -= Time.deltaTime;
			levelTimerText.text = levelTime.ToString("F0");

			if (levelTime <= 0)
			{

				playerLives--;
				GameController.instance.currentLives = playerLives;
				GameController.instance.currentScore = playerScore;

				if (playerLives < 0)
				{
					StartCoroutine(PromptTheUserToWatchAVideo());
				}
				else
				{
					StartCoroutine(PlayerDiedRestartLevel());
				}

			}

		}

	}

	IEnumerator PlayerDiedRestartLevel()
	{

		levelInProgress = false;

		coins = 0;
		smallBallsCount = 0;

		Time.timeScale = 0;

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.FadeOut();
		}

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));

		Application.LoadLevel(Application.loadedLevelName);

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayFadeInAnimation();
		}

	}

	public void PlayerDied()
	{

		countdownLevel = false;
		pauseBtn.interactable = false;
		levelInProgress = false;

		smallBallsCount = 0;

		playerLives--;
		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;

		if (playerLives < 0)
		{
			StartCoroutine(PromptTheUserToWatchAVideo());
		}
		else
		{
			StartCoroutine(PlayerDiedRestartLevel());
		}

	}

	IEnumerator PromptTheUserToWatchAVideo()
	{
		levelInProgress = false;
		countdownLevel = false;
		pauseBtn.interactable = false;

		Time.timeScale = 0;

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.7f));

		playerDiedPanel.SetActive(true);
	}

	IEnumerator LevelCompleted()
	{

		countdownLevel = false;
		pauseBtn.interactable = false;

		int unlockedLevel = GameController.instance.currentLevel;
		unlockedLevel++;

		if (!(unlockedLevel >= GameController.instance.levels.Length))
		{
			GameController.instance.levels[unlockedLevel] = true;
		}

		Instantiate(endOfLevelRewards[GameController.instance.currentLevel],
					 new Vector3(0, Camera.main.orthographicSize, 0), Quaternion.identity);

		if (GameController.instance.doubleCoins)
		{
			coins *= 2;
		}

		GameController.instance.coins = coins;
		GameController.instance.Save();

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(4f));
		levelInProgress = false;
		PlayerScript.instance.StopMoving();
		Time.timeScale = 0;

		levelFinishedPanel.SetActive(true);
		showScoreAtTheEndOfLevelText.text = "" + playerScore;


	}

	public void CountSmallBalls()
	{
		smallBallsCount--;

		if (smallBallsCount == 0)
		{
			StartCoroutine(LevelCompleted());
		}
	}

	public void GoToMapButton()
	{

		GameController.instance.currentScore = playerScore;

		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}

		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}

		Application.LoadLevel("LevelMenu");

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayLoadingScreen();
		}

	}

	public void RestartCurrentLevelButton()
	{

		smallBallsCount = 0;
		coins = 0;

		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;

		Application.LoadLevel(Application.loadedLevelName);

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayLoadingScreen();
		}

	}


	public void NextLevel()
	{

		GameController.instance.currentScore = playerScore;
		GameController.instance.currentLives = playerLives;

		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}

		int nextLevel = GameController.instance.currentLevel;
		nextLevel++;

		if (!(nextLevel >= GameController.instance.levels.Length))
		{
			GameController.instance.currentLevel = nextLevel;

			Application.LoadLevel("Level" + nextLevel);

			if (LoadingScreen.instance != null)
			{
				LoadingScreen.instance.PlayLoadingScreen();
			}

		}


	}

	public void PauseGame()
	{

		if (!hasLevelBegan)
		{

			if (levelInProgress)
			{

				if (!isGamePaused)
				{

					countdownLevel = false;
					levelInProgress = false;
					isGamePaused = true;

					panelBG.SetActive(true);
					pausePanel.SetActive(true);

					Time.timeScale = 0;
				}

			}

		}

	}

	public void ResumeGame()
	{

		countdownLevel = true;
		levelInProgress = true;
		isGamePaused = false;
		panelBG.SetActive(false);
		pausePanel.SetActive(false);

		Time.timeScale = 1;

	}

	IEnumerator GivePlayerLivesRewardAfterWatchingVideo()
	{

		watchVideoText.text = "Thank ";

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(2f));

		coins = 0;
		playerLives = 2;
		smallBallsCount = 0;

		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;

		Time.timeScale = 0;

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.FadeOut();
		}

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));

		Application.LoadLevel(Application.loadedLevelName);

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayFadeInAnimation();
		}

	}

	public void DontWatchVideoInsteadQuit()
	{

		GameController.instance.currentScore = playerScore;

		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}

		Time.timeScale = 1;

		Application.LoadLevel("LevelMenu");
		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayLoadingScreen();
		}

	}


} // gameplay controller
