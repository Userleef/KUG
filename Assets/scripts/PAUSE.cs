using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAUSE : MonoBehaviour
{
	public GameObject menuObject;
	private bool isActive;
	
	void Update () {
		if (isActive )
		{
			menuObject.SetActive(true);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			Time.timeScale = 0;
		}
		else {
			menuObject.SetActive(false);
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
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
	}
}
