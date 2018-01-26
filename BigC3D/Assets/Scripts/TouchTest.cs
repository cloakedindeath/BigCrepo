﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchTest : MonoBehaviour 
{
	// Update is called once per frame
	public float speed = 0.1F;
	public GameObject player; 

	public Rigidbody projectile;
	public Rigidbody[] projectiles;
	public float bulletSpeed = 20;
	public int ammoType;

	void Awake()
	{
		ammoType = 0;
	}

	void Update() 
	{
		Debug.Log (ammoType);
		if(UIManager.instance.gameOver == false)
		{
			////////////////////////////////////////////////////
			//Check that player position is within the screen//
			//////////////////////////////////////////////////
			if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
			{
				//////////////////////////////////////////
				//Touch movement with Y-axis restricted//
				////////////////////////////////////////
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
				{
					Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
					transform.Translate (-touchDeltaPosition.x * speed, 0 * speed, 0);
				}
				//////////////////////////////////////////////////
				//Guarantees that the player wont go off screen//
				////////////////////////////////////////////////
				Vector2 p = player.transform.position;
				player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
			}
		}

	}

	public void PickAmmoType()
	{
		if(ammoType == 0)
		{
			projectile = projectiles [0];
			ammoType++;
		}
		else if(ammoType == 1)
		{
			projectile = projectiles [1];
			ammoType++;
		}
		else if(ammoType == 2)
		{
			projectile = projectiles [2];
			ammoType = 0;
		}
	}

	public void Shoot2()
	{
		Debug.Log ("Shooting");
		Rigidbody instantiatedProjectile = Instantiate (projectile,
			player.transform.position, 
			Quaternion.identity)
			as Rigidbody;
		instantiatedProjectile.velocity = transform.TransformDirection (new Vector3 (0, 0, -bulletSpeed));
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy_Waffle" ||
			col.gameObject.tag == "Enemy_Chicken" || col.gameObject.tag == "Enemy_KoolAid")
		{
			EnemySpawner.instance.count--;
			Destroy (col.gameObject);
			ScoreManager.instance.LoseLife ();
		}
	}
}
