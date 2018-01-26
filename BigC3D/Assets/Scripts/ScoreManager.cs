﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;

	public int score;
	public int lives;
	public Button shootButton;
	//public int mpAmt;
	public int points = 20;
	public GameObject mpText;
	public Text mp;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () 
	{
		lives = 3;
		score = 0;
		PlayerPrefs.SetInt ("Score", 0);
		//mpAmt = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lives <= 0)
		{
			PlayerPrefs.SetInt ("Score", score);

			GameObject.Find ("EnemySpawner").GetComponent<EnemySpawner> ().StopSpawning ();
			//GameObject.Find ("EnemySpawner").GetComponent<EnemySpawner> ().enabled = false;
			GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
			shootButton.GetComponent<Button> ().interactable = false;

			UIManager.instance.GameOver ();
			lives = 0;
		}

		if(UIManager.instance.mpCnt == 0)
		{
			mp.text = " ";

		}
		else if(UIManager.instance.mpCnt >= 10 && UIManager.instance.mpCnt < 30)
		{
			mp.text = "x2";
		}
		else if ( UIManager.instance.mpCnt >= 30)
		{
			mp.text = "x3";
		}
	

		UIManager.instance.highScoreText.text ="High Score: " + PlayerPrefs.GetInt ("HighScore").ToString ();
	}

	public void SetPlayerScores()
	{
		if(PlayerPrefs.HasKey("HighScore"))
		{
			if(score > PlayerPrefs.GetInt("HighScore"))
			{
				PlayerPrefs.SetInt ("HighScore", score);
			}
		}
		else
		{
			PlayerPrefs.SetInt ("HighScore", score);
		}
	}
	public void EnemyKill()
	{
		if(UIManager.instance.mpCnt == 0)
		{
			score += points;
		}
		if(UIManager.instance.mpCnt >= 10 && UIManager.instance.mpCnt < 30)
		{
			score += (points * 2);
		}
		else if ( UIManager.instance.mpCnt >= 30)
		{
			score += (points * 3);
		}


	}
	public void LoseLife()
	{
		lives = lives - 1;
	}
}
