﻿using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{

	public static LoadingScreen instance;

	[SerializeField]
	GameObject bgImage, logoImage, text, fadePanel;

	[SerializeField]
	private Animator fadeAnim;

	// Use this for initialization
	void Awake()
	{
		MakeSingleton();
		Hide();
	}

	void MakeSingleton()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void PlayLoadingScreen()
	{
		StartCoroutine(ShowLoadingScreen());
	}

	public void PlayFadeInAnimation()
	{
		StartCoroutine(FadeIn());
	}

	IEnumerator FadeIn()
	{
		fadeAnim.Play("FadeIn");
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.4f));

		if (GamePlayController.instance != null)
		{
			GamePlayController.instance.setHasLevelBegan(true);
		}

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.9f));
		fadePanel.SetActive(false);
	}

	public void FadeOut()
	{
		fadePanel.SetActive(true);
		fadeAnim.Play("FadeOut");
	}

	IEnumerator ShowLoadingScreen()
	{
		Show();
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
		Hide();

		if (GamePlayController.instance != null)
		{
			GamePlayController.instance.setHasLevelBegan(true);
		}
	}

	void Show()
	{
		bgImage.SetActive(true);
		logoImage.SetActive(true);
		text.SetActive(true);
	}

	void Hide()
	{
		bgImage.SetActive(false);
		logoImage.SetActive(false);
		text.SetActive(false);
	}

} // loading screen
