using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
	public float timeRemaining;
	public GUIStyle guiStyle = new GUIStyle();
	public RawImage Game_over_display;
	
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
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			Game_over_display.gameObject.SetActive(true);
			Game_over_display.GetComponent<Display_Star>().display_star();
			Time.timeScale = 0;
		}
	}
}
