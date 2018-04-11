using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
	public float timeRemaining;
	public GUIStyle guiStyle = new GUIStyle();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(timeRemaining > 0)
			timeRemaining -= Time.deltaTime;
	}

	private void OnGUI()
	{
		guiStyle.fontSize = 30;
		if (timeRemaining > 0)
		{
			string zerro = "";
			if ((timeRemaining % 60) < 10)
				zerro = "0";
			GUI.Label(new Rect(10, 10, 200, 100), (int)timeRemaining / 60 + " : " + zerro + (int) timeRemaining % 60, guiStyle);
		}
		else
		{
			GUI.Label(new Rect(240, 100, 100, 100), "Time's up", guiStyle);
			Time.timeScale = 0;
		}
	}
}
