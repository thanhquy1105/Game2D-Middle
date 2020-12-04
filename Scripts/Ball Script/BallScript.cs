﻿using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{

	private float forceX, forceY;

	[SerializeField]
	private bool moveLeft, moveRight;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private GameObject originalBall;

	private GameObject ball1, ball2;

	private BallScript ball1Script, ball2Script;

	[SerializeField]
	private AudioClip[] popSounds;


	void Awake()
	{

		/*if (this.gameObject.tag == "SmallestBall")
		{
			GameplayController.smallBallsCount++;
		}*/

		SetBallSpeed();
		InstantiateBalls();
	}

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		MoveBall();
	}

	void InstantiateBalls()
	{

		if (this.gameObject.tag != "SmallestBall")
		{
			ball1 = Instantiate(originalBall);
			ball2 = Instantiate(originalBall);

			ball1Script = ball1.GetComponent<BallScript>();
			ball2Script = ball2.GetComponent<BallScript>();

			ball1.SetActive(false);
			ball2.SetActive(false);
		}
	}

	public void SetMoveLeft(bool moveLeft)
	{
		this.moveLeft = moveLeft;
		this.moveRight = !moveLeft;
	}

	public void SetMoveRight(bool moveRight)
	{
		this.moveRight = moveRight;
		this.moveLeft = !moveRight;
	}

	void InitializeBallsAndTurnOffCurrentBall()
	{

		Vector3 position = transform.position;

		ball1.transform.position = position;
		ball1Script.SetMoveLeft(true);

		ball2.transform.position = position;
		ball2Script.SetMoveRight(true);

		ball1.SetActive(true);
		ball2.SetActive(true);

		if (gameObject.tag != "SmallestBall")
		{
			if (transform.position.y > 1 && transform.position.y <= 1.3f)
			{
				ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
				ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
			}
			else if (transform.position.y > 1.3f)
			{
				ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
				ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
			}
			else if (transform.position.y < 1)
			{
				ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
				ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
			}
		}

		AudioSource.PlayClipAtPoint (popSounds[Random.Range(0, popSounds.Length)], transform.position);
		//InitializeCollectableItems(transform.position);
		//GiveScoreAndCoins(this.gameObject.tag);
		gameObject.SetActive(false);

	}

	void MoveBall()
	{
		if (moveLeft)
		{
			Vector3 temp = transform.position;
			temp.x -= (forceX * Time.deltaTime);
			transform.position = temp;
		}

		if (moveRight)
		{
			Vector3 temp = transform.position;
			temp.x += (forceX * Time.deltaTime);
			transform.position = temp;
		}
	} // move the ball


	void SetBallSpeed()
	{

		forceX = 2.5f;

		switch (this.gameObject.tag)
		{

			case "LargestBall":
				forceY = 11.5f;
				break;

			case "LargeBall":
				forceY = 10.5f;
				break;

			case "MediumBall":
				forceY = 9f;
				break;

			case "SmallBall":
				forceY = 8f;
				break;

			case "SmallestBall":
				forceY = 7f;
				break;

		}

	} // set ball speed

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "FirstArrow" || target.tag == "SecondArrow" || target.tag == "FirstStickyArrow" || target.tag == "SecondStickyArrow")
		{
			if (gameObject.tag != "SmallestBall")
			{
				InitializeBallsAndTurnOffCurrentBall();
			}
			else
			{
				AudioSource.PlayClipAtPoint (popSounds[Random.Range(0, popSounds.Length)], transform.position);
				//GameplayController.instance.CountSmallBalls();
				gameObject.SetActive(false);
			}
		} // if the ball hits the arrow

		if (target.tag == "UnbreakableBrickTop" || target.tag == "BrokenBrickTop" || target.tag == "UnbreakableBrickTopVertical")
		{
			myRigidBody.velocity = new Vector2(0, 5);

		}
		else if (target.tag == "UnbreakableBrickBottom" || target.tag == "BrokenBrickBottom"
				 || target.tag == "UnbreakableBrickBottomVertical")
		{
			myRigidBody.velocity = new Vector2(0, -2);

		}
		else if (target.tag == "UnbreakableBrickLeft" || target.tag == "BrokenBrickLeft"
				 || target.tag == "UnbreakableBrickLeftVertical")
		{
			moveLeft = true;
			moveRight = false;

		}
		else if (target.tag == "UnbreakableBrickRight" || target.tag == "BrokenBrickRight"
				 || target.tag == "UnbreakableBrickRightVertical")
		{
			moveRight = true;
			moveLeft = false;
		}

		if (target.tag == "BottomBrick")
		{
			myRigidBody.velocity = new Vector2(0, forceY);
		} // if its the bottom brick

		if (target.tag == "LeftBrick")
		{
			moveLeft = false;
			moveRight = true;
		} // if its left brick

		if (target.tag == "RightBrick")
		{
			moveLeft = true;
			moveRight = false;
		} // if its right brick

		

	} // on trigger enter








} // class
