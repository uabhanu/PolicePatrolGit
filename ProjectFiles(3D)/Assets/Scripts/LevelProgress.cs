﻿using UnityEngine;
using System.Collections;

public class LevelProgress : MonoBehaviour 
{
	public bool levelProgress;
	public Enemy enemyScript;
	public GameObject enemyObj , iguiObj , truckLeftObj , truckRightObj;
	public InGameUI iguiScript;
	public Truck truckScript;
	
	void Start () 
	{
		DontDestroyOnLoad(this.gameObject);
		StartCoroutine("GameTimer");
	}

	IEnumerator GameTimer()
	{
		yield return new WaitForSeconds(0.4f);
		
		if(iguiObj == null)
		{
			iguiObj = GameObject.FindGameObjectWithTag("IGUI");
		}

		if(truckLeftObj == null)
		{
			truckLeftObj = GameObject.FindGameObjectWithTag("Left");
		}

		if(truckRightObj == null)
		{
			truckRightObj = GameObject.FindGameObjectWithTag("Right");
		}
		
		if(iguiObj != null && truckLeftObj != null && truckRightObj != null)
		{
			iguiScript = iguiObj.GetComponent<InGameUI>();
			
			if(iguiScript != null)
			{
				if(iguiScript.timeValue > 0) //This part works only if game started from Level Selection Screen which is correct
				{
					if(iguiScript.truckLeftScoreValue == 5 && truckLeftObj == null && iguiScript.truckRightScoreValue == 5 && truckRightObj == null)
					{
						Debug.Log("Both Trucks 5/5");
						iguiScript.Active("LoseCard");
						iguiScript.Active("LoseCardText");
						iguiScript.Inactive("PauseButton");
						iguiScript.Active("RetryButton");
						Time.timeScale = 0;
					}
				}
				
				else if(iguiScript.timeValue == 0)
				{
					Debug.Log("Back up arrived in Time");
					iguiScript.Active("WinCard");
					iguiScript.Active("WinCardText");
					iguiScript.Inactive("PauseButton");
					iguiScript.Active("ContinueButton");
					Time.timeScale = 0;
					levelProgress = true;
				}
			}
		}
		
		StartCoroutine("GameTimer");
	}

	void Update () 
	{
	
	}
}
