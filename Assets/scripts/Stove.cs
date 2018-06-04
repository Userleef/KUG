using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour
{
	public GameObject food_on;
	public bool have_food;
	public float TimerCook = 10;
	public bool On_Cooking_Place = true;
	public Canvas TimeBar;
	public Image timeBarGreen;
	public Image timeBarRed;
	public Material steak_cuit;
	public Material fish_cuit;
	public Material fish_crame;
	public bool Is_ready;
	public ParticleSystem ps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(TimerCook > -6 && have_food && On_Cooking_Place)
			TimerCook -= Time.deltaTime;
	}

	public void Add_aliment(GameObject al)
	{
		have_food = true;
		food_on = al;
		food_on.transform.rotation = new Quaternion(0,0,0,0);
		food_on.transform.parent = gameObject.transform;
		food_on.transform.position = gameObject.transform.position + Vector3.up * 0.1f;
		TimerCook = 10;
	}

	public bool Is_full()
	{
		return have_food;
	}
	
	private void OnGUI()
	{
		if (have_food)
		{
			if (timeBarRed.fillAmount < 1)
			{
				if (TimerCook > 0)
				{
					
					TimeBar.gameObject.SetActive(true);
					timeBarGreen.fillAmount = TimerCook / 10.0f;
					timeBarRed.fillAmount = 0;
				}
			}
			if (TimerCook < 0 && TimerCook > -5)
				timeBarRed.fillAmount = Math.Abs(TimerCook / 5.0f);
			else if (TimerCook < 0.1 && TimerCook > 0)
			{
				run_cook();
			}
			else if (TimerCook < -4.9 && TimerCook > -5.1)
			{
				BurnCook();
			}
		}
	}
	
	public void empty_aliment()
	{
		TimeBar.gameObject.SetActive(false);
		Is_ready = false;
		have_food = false;
		food_on = null;
		ps.playOnAwake = false;
		ps.Pause();
		ps.Clear();
	}
	
	public void run_cook()
	{
		Is_ready = true;
		if (food_on.tag == "F Steak")
			food_on.GetComponent<Renderer>().material = steak_cuit;
		else if (food_on.tag == "F Fish")
			food_on.GetComponent<poisson>().fish.GetComponent<Renderer>().material = fish_cuit;
	}

	public void BurnCook()
	{
		TimeBar.gameObject.SetActive(false);
		
		if (food_on.tag == "F Steak")
			food_on.GetComponent<Renderer>().material.color = Color.black;
		else if (food_on.tag == "F Fish")
			food_on.GetComponent<poisson>().fish.GetComponent<Renderer>().material = fish_crame;
		
		food_on.tag = "Burned";
		ps.playOnAwake = true;
		ps.Emit(100);
		ps.Play();
	}
}
