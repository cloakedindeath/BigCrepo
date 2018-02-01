﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentTimer : MonoBehaviour 
{
	public static PersistentTimer instance;

	public Text timer;
	int minutes = 1;
	int seconds = 0;
	float milliseconds = 0;

	public int curHealth;
	public int maxHealth = 3;

	//[Range(1,59)]
	public int defaultStartMinutes = 1;
	public bool allowTimerRestart = false;

	public int savedSeconds;
	private bool resetTimer = false;

	void Start()
	{
		curHealth = maxHealth;
	}

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		//DontDestroyOnLoad(this);
		minutes = defaultStartMinutes;

		if(PlayerPrefs.HasKey("TimeOnExit"))
		{
			milliseconds = PlayerPrefs.GetFloat ("TimeOnExit");

			minutes = (int)milliseconds / 60;
			milliseconds -= (minutes * 60);

			seconds = (int)milliseconds;
			milliseconds -= seconds;

			//PlayerPrefs.DeleteKey ("TimeOnExit");
		}
	}

	public void Update()
	{
		Debug.Log(PlayerPrefs.GetFloat("TimeOnExit"));
		Debug.Log(savedSeconds);
		//PlayerPrefs.SetInt ("TimeOnExit", savedSeconds);
		if(PlayerPrefs.GetInt("lives") == 0)
		{
			//count down in seconds

			milliseconds += Time.deltaTime;

			if(resetTimer)
			{
				ResetTimer ();
			}

			if(milliseconds >= 1.0f)
			{
				milliseconds -= 1.0f;

				if((seconds > 0) || (minutes > 0))
				{
					seconds--;

					if(seconds < 0)
					{
						seconds = 59;
						minutes--;
					}
				}
				else
				{
					//add code to flag and stop endless loop
					PlayerPrefs.SetInt("lives", 3);
					if(PlayerPrefs.GetInt("lives") == 3)
					{
						allowTimerRestart = true;
						//PlayerPrefs.DeleteKey ("TimeOnExit");
					}
					else
					{
						allowTimerRestart = false;
					}
					//allowTimerRestart = true;
					resetTimer = allowTimerRestart;
				}
			}

			if(seconds != savedSeconds)
			{
				//Show Current Time
				timer.text = string.Format("Time:{0}:{1:D2}", minutes,seconds);

				savedSeconds = seconds;
			}
		}

	}

	void ResetTimer()
	{
		minutes = defaultStartMinutes;
		seconds = 0;
		savedSeconds = 0;
		milliseconds = 1.0f - Time.deltaTime;
		resetTimer = false;
	}

	private void OnApplicationQuit()
	{
		int numSeconds = ((minutes * 60) + seconds);

		if(numSeconds > 0)
		{
			milliseconds += numSeconds;
			//PlayerPrefs.SetFloat ("TimeOnExit", milliseconds);
			PlayerPrefs.SetFloat ("TimeOnExit", savedSeconds);
		}
	}
}