using System;
using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Is_food_on_cutting_place : Photon.MonoBehaviour
{

	public bool have_food = false;
	public GameObject food_on;

	public AudioSource audio;
	public GameObject Cutting_Tomato;
	public GameObject Cutting_Carot;

	private float TimerCut = -1;
	public Canvas TimeBar;
	public Image timeBarGreen;

	private bool can_cut;
	private bool push_key;
	public bool is_cutting;


	private void OnTriggerStay(Collider Col)
	{
		if(Col.tag == "Target")
			can_cut = true;
	}
	
	private void OnTriggerExit(Collider Col)
	{
		if (Col.tag == "Target")
		{
			push_key = false;
			can_cut = false;
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TimerCut > 0 && can_cut && push_key)
		{
			TimerCut -= Time.deltaTime;
			audio.mute = false;
		}
		else
		{
			audio.mute = true;
		}
	}
	
	private void OnGUI()
	{
		if (TimerCut > 0.1)
		{
			TimeBar.gameObject.SetActive(true);
			timeBarGreen.fillAmount = (TimerCut / 10.0f) * 3;
		}
		else if(TimerCut < 0.1 && TimerCut >= 0 && food_on.tag[1] != 'C')
		{
			TimeBar.gameObject.SetActive(false);
			CutAliment();
			is_cutting = false;
		}
	}

	public void Run_Cut()
	{
		push_key = true;
		
		if (TimerCut < 0)
		{
			is_cutting = true;
			TimerCut = 3f;
		}


	}

	public void CutAliment()
	{
		GameObject Aliment_a_couper = food_on;
		if (food_on.tag == "F Tomato")
		{
			Aliment_a_couper = Cutting_Tomato;
		}
		if (food_on.tag == "F Carot")
		{
			Aliment_a_couper = Cutting_Carot;
		}
		
		food_on.SetActive(false);
		
		Transform cutting_aliment = PhotonView.Instantiate(Aliment_a_couper.transform, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
		
		food_on = cutting_aliment.gameObject;
		
		cutting_aliment.transform.parent = gameObject.GetComponent<Transform>();
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,-90,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.right * 0.3f;
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,0,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.back * 0.3f;
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,90,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.left * 0.3f;
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,180,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.forward * 0.3f;
		
	}
	
	
}
