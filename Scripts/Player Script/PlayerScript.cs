using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Specialized;
using System.Security.Cryptography;

public class PlayerScript : MonoBehaviour
{

	public static PlayerScript instance;

	private float speed = 8.0f;
	private float maxVelocity = 4.0f;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private GameObject[] arrows;

	private float height;

	private bool canWalk;

	[SerializeField]
	private AnimationClip clip;

	[SerializeField]
	private AudioClip shootClip;

	private bool shootOnce, shootTwice;

	void Awake()
    {
		if (instance == null)
		{
			instance = this;
		}

		float cameraHeight = Camera.main.orthographicSize;
		height = - cameraHeight - 0.8f;
		canWalk = true;
		shootOnce = true;
		
		shootTwice = true;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		ShootTheArrow();
	}

	void FixedUpdate()
	{
		PlayerWalkKeyboard();

		//MoveThePlayer();

	}

	public void PlayerShootOnce(bool shootOnce)
	{
		this.shootOnce = shootOnce;
		//Debug.Log("Once:" + shootOnce);
		
		//shootFirstArrow = false;
	}

	public void PlayerShootTwice(bool shootTwice)
	{

		//if (doubleArrows || doubleStickyArrows)
		//{
		this.shootTwice = shootTwice;
		//}
		Debug.Log("Twice:" + shootTwice);
		//shootSecondArrow = false;
	}

	public void ShootTheArrow()
	{
		if (Input.GetMouseButtonDown(0))
        {
			if (shootOnce)
            {
				shootOnce = false;
				StartCoroutine(PlayerTheShootAnimation());
				Instantiate(arrows[2], new Vector3(transform.position.x, height, 0), Quaternion.identity);
				
			}
			else if (shootTwice)
            {
				shootTwice = false;
				StartCoroutine(PlayerTheShootAnimation());
				Instantiate(arrows[3], new Vector3(transform.position.x, height, 0), Quaternion.identity);
				
			}
			
		}
		

	}

	IEnumerator PlayerTheShootAnimation()
	{

		canWalk = false;
		//shootBtn.interactable = false;
		animator.Play("PlayerShoot");
		AudioSource.PlayClipAtPoint (shootClip, transform.position);
		yield return new WaitForSeconds(clip.length);
		animator.SetBool("Shoot", false);
		//shootBtn.interactable = true;
		canWalk = true;
	}


	void PlayerWalkKeyboard()
	{

		float force = 0.0f;
		float velocity = Mathf.Abs(myRigidBody.velocity.x);

		float h = Input.GetAxis("Horizontal");


		if (canWalk)
		{

			if (h > 0)
			{
				// moving right

				if (velocity < maxVelocity)
				{
					force = speed;
				}

				Vector3 scale = transform.localScale;
				scale.x = 1.0f;
				transform.localScale = scale;

				animator.SetBool("Walk", true);

			}
			else if (h < 0)
			{
				// moving left

				if (velocity < maxVelocity)
				{
					force = -speed;
				}

				Vector3 scale = transform.localScale;
				scale.x = -1.0f;
				transform.localScale = scale;

				animator.SetBool("Walk", true);

			}
			else if (h == 0)
			{
				animator.SetBool("Walk", false);
			}

			myRigidBody.AddForce(new Vector2(force, 0));

		}

	}



} // class



























