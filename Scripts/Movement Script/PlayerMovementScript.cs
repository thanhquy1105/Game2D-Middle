﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	public void OnPointerDown(PointerEventData data)
	{
		if (this.gameObject.tag == "MoveLeftButton")
		{
			PlayerScript.instance.MoveThePlayerLeft();
		}
		else if (this.gameObject.tag == "MoveRightButton")
		{
			PlayerScript.instance.MoveThePlayerRight();
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		PlayerScript.instance.StopMoving();
	}

} // player movement script
