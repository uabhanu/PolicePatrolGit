﻿using GooglePlayGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;

public class LevelSelection : MonoBehaviour 
{
	public bool levelProgress;
	public GameObject iguiObj , PF_LevelProgress , progressObj;
	public GUITexture[] buttons;
	public InGameUI iguiScript;
	public LevelProgress progressScript;
	public Texture[] buttonTextures;

	void Start () 
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;

		PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) =>
      	{
			if(success)
			{
				Debug.Log("You've successfully logged in");
			}
			else
			{
				Debug.Log("Login failed for some reason");				
			}
		});
	
		progressObj = GameObject.FindGameObjectWithTag("Progress");

		if(progressObj == null)
		{
			progressObj = Instantiate(PF_LevelProgress , Vector3.zero , Quaternion.identity) as GameObject;
		}		

		if(progressObj != null)
		{
			progressScript = progressObj.GetComponent<LevelProgress>();
		}

		Unlock("Level1");

		#region Selection
		if(progressScript != null)
		{
			if(buttons[1].GetComponent<GUITexture>().texture == buttonTextures[1] && progressScript.levelProgress == 1 && progressScript.levelNo >= 1)
			{
				Unlock("Level2");
			}

			if(buttons[1].GetComponent<GUITexture>().texture == buttonTextures[1] && progressScript.levelProgress == 1 && progressScript.levelNo >= 2)
			{
				Unlock("Level3");
			}

			if(buttons[1].GetComponent<GUITexture>().texture == buttonTextures[1] && progressScript.levelProgress == 1 && progressScript.levelNo >= 3)
			{
				Unlock("Level4");
			}
		}
		#endregion
	}

	#region Lock Method
	public void Lock(string name)
	{
		switch(name)
		{
			case "Level1" :
				buttons[0].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level2" :
				buttons[1].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level3" :
				buttons[2].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level4" :
				buttons[3].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level5" :
				buttons[4].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level6" :
				buttons[5].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level7" :
				buttons[6].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level8" :
				buttons[7].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level9" :
				buttons[8].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level10" :
				buttons[9].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level11" :
				buttons[10].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level12" :
				buttons[11].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level13" :
				buttons[12].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level14" :
				buttons[13].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level15" :
				buttons[14].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level16" :
				buttons[15].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level17" :
				buttons[16].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level18" :
				buttons[17].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level19" :
				buttons[18].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level20" :
				buttons[19].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level21" :
				buttons[20].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;

			case "Level22" :
				buttons[21].GetComponent<GUITexture>().texture = buttonTextures[1];
			break;
		}
	}
	#endregion

	#region Unlock Method
	public void Unlock(string name)
	{
		switch(name)
		{
			case "Level1" :
				buttons[0].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level2" :
				buttons[1].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level3" :
				buttons[2].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level4" :
				buttons[3].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level5" :
				buttons[4].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level6" :
				buttons[5].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level7" :
				buttons[6].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level8" :
				buttons[7].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level9" :
				buttons[8].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level10" :
				buttons[9].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level11" :
				buttons[10].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level12" :
				buttons[11].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level13" :
				buttons[12].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level14" :
				buttons[13].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level15" :
				buttons[14].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level16" :
				buttons[15].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level17" :
				buttons[16].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level18" :
				buttons[17].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level19" :
				buttons[18].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level20" :
				buttons[19].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level21" :
				buttons[20].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
			
			case "Level22" :
				buttons[21].GetComponent<GUITexture>().texture = buttonTextures[0];
			break;
		}
	}
	#endregion

	void Update () 
	{

	}	
}