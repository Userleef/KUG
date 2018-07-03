using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAUSE : MonoBehaviour
{
	public Canvas menuObject;
	private bool isActive;
	
	void Update () {
		if (isActive )
		{
			menuObject.gameObject.SetActive(true);
			Cursor.visible = true;
			Time.timeScale = 0;
		}
		else {
			menuObject.gameObject.SetActive(false);
			Time.timeScale = 1;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			RESUME_Button();
		}
	}

	public void RESUME_Button()
	{
		isActive = !isActive;
		Cursor.visible = false;
	}
}
