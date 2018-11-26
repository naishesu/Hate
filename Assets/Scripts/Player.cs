using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public string horizontalInput = "Horizontal";
	public string verticalInput = "Vertical";
	public bool hasMoved = false;
	
	void Update()
	{
		if(Input.GetButtonDown(horizontalInput) || Input.GetButtonDown(verticalInput))
		{
			Movement();
		}
	}

	public void Movement()
	{
		
		Vector3 movement = Vector3.zero;

		if (Input.GetButtonDown(horizontalInput))
		{
			float axis = Input.GetAxis(horizontalInput);
			//movement = (axis > 0) ? Vector3.right : Vector3.left;

			// if(axis > 0)
			// {
			// 	movement = Vector3.right;
			// }

			// else
			// {
			// 	movement = Vector3.left;
			// }

			movement = Vector3.right * Mathf.Sign(axis);
		}

		else if (Input.GetButtonDown(verticalInput))
		{
			float axis = Input.GetAxis(verticalInput);

			// if(axis > 0)
			// {
			// 	movement = Vector3.forward;
			// }

			// else if (axis < 0)
			// {
			// 	movement = Vector3.back;
			// }
			movement = Vector3.forward * Mathf.Sign(axis);
		}

		if (!CheckDirection(movement))
		{
			transform.position += movement;
			Debug.Log ("has moved true");
			hasMoved = true;
			// return movement != Vector3.zero;
		}

		// return movement == Vector3.zero;
	}


	//  void OnMove ()
	//   {
	// 	OnEnter(null);
	//   	Debug.Log("je reset le ground OnMove");
	//   }
		
	bool CheckDirection (Vector3 dir)
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, 1))
		{
			return true;
		}

		return false;
	}
}
