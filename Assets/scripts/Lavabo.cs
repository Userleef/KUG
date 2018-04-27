using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lavabo : MonoBehaviour {

	private float TimerWash = -1;
	public Canvas TimeBar;
	public Image timeBarGreen;

	private GameObject player;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(TimerWash > 0)
			TimerWash -= Time.deltaTime;
	}
	
	
	private void OnTriggerStay(Collider Col)
	{
		if (Col.tag == "Target")
		{
			player = Col.gameObject;
		}
	}
	
	private void OnTriggerExit(Collider Col)
	{
		if (Col.tag == "Target")
		{
			TimerWash = -1;
			TimeBar.gameObject.SetActive(false);
		}
	}
	
	private void OnGUI()
	{
		if (TimerWash > 0.1)
		{
			TimeBar.gameObject.SetActive(true);
			timeBarGreen.fillAmount = (TimerWash / 10.0f) * 3;
		}
		else if(TimerWash < 0.1 && TimerWash >= 0)
		{
			TimeBar.gameObject.SetActive(false);
			Clean();
		}
	}

	public void Run_Wash()
	{
		TimerWash = 3;
	}

	public void Clean()
	{
		player.GetComponent<Target>().player.GetComponent<Controller>().grab_object.GetComponent<Assiette>().Is_clean = true;
		player.GetComponent<Target>().player.GetComponent<Controller>().grab_object.GetComponent<Assiette>().sauce_assiette.GetComponent<MeshRenderer>().material = null;
		player.GetComponent<Target>().player.GetComponent<Controller>().grab_object.GetComponent<Assiette>().sauce_assiette.SetActive(false);
	}
}
