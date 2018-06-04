using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
//using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Cooking_Pot : MonoBehaviour
{
	
	
	public List<string> aliment_inside;

	public bool On_Cooking_Place = true;
	
	public Transform slot1;
	public Transform slot2;
	public Transform slot3;

	public Material material_fond_casserole;
	
	public List<string> recette_Tomato3;
	public List<string> recette_Carot3;
	public List<string> recette_Oignon3;
	
	public List<string> recette_Carot2tomate;
	public List<string> recette_Tomates2carot;
	
	/*public List<string> recette_Carot2tomateVS2;
	public List<string> recette_Tomates2carotVS2;
	
	public List<string> recette_Carot2tomateVS3;
	public List<string> recette_Tomates2carotVS3;*/

	public ParticleSystem ps;
	public GameObject sauce;

	private Color Couleur_tomates;
	private Color Couleur_carotte;
	private Color Couleur_oignon;
	private Color Burn;
	private Color Couleur_inconnue = new Color();
	private Color CouleurCarot2tomato;
	private Color CouleurTomato2Carot;
	private float TimerCook = 10;

	public Canvas TimeBar;
	public Image timeBarGreen;
	public Image timeBarRed;

	public bool Is_Ready;
	
	// Use this for initializatio
	void Start ()
	{
		aliment_inside = new List<string>();
		
		//Couleur recette full tomates
		ColorUtility.TryParseHtmlString("#FF0000", out Couleur_tomates);
		recette_Tomato3 = new List<string>{"FC Tomato","FC Tomato","FC Tomato"};
		
		//Couleur recette full carottes
		ColorUtility.TryParseHtmlString("#DC7633", out Couleur_carotte);
		recette_Carot3 = new List<string>{"FC Carot","FC Carot","FC Carot"};
		
		//couleur recette full oignon
		ColorUtility.TryParseHtmlString("#A98AA5", out Couleur_oignon);
		recette_Oignon3 = new List<string>{"FC Oignon","FC Oignon","FC Oignon"};
		
		//couleur recette inconnue
		ColorUtility.TryParseHtmlString("#A98AA5", out Couleur_inconnue);
		
		//plat brulé
		ColorUtility.TryParseHtmlString("#135C10", out Burn);
		
		//couleur recette 2 carottes et 1 tomate
		ColorUtility.TryParseHtmlString("#FB5430", out CouleurCarot2tomato);
		recette_Carot2tomate = new List<string>{"FC Carot","FC Carot","FC Tomato"};
		
		//couleur recette 2 tomates et 1 carotte
		ColorUtility.TryParseHtmlString("#B62607", out CouleurTomato2Carot);
		recette_Tomates2carot = new List<string>{"FC Tomato","FC Tomato","FC Carot"};
	}
	
	// Update is called once per frame
	void Update () {
		if(TimerCook > -6 && aliment_inside.Count > 0 && On_Cooking_Place)
			TimerCook -= Time.deltaTime;
	}
	
	
	private GameObject DefineSlot(List<string> l)
	{
		if (l.Count == 1)
			return slot1.gameObject;
		if (l.Count == 2)
			return slot2.gameObject;
		return slot3.gameObject;
	}
	
	private Color DefineColor(GameObject al)
	{
		if(al.tag == "FC Tomato")
			return Couleur_tomates;
		if(al.tag == "FC Carot")
			return Couleur_carotte;
		if(al.tag == "FC Oignon")
			return Couleur_oignon;
		return Couleur_inconnue;
	}
	
	public void Add_aliment(GameObject al)
	{
		aliment_inside.Add(al.tag);
		al.SetActive(false);
		DefineSlot(aliment_inside).SetActive(true);
		DefineSlot(aliment_inside).GetComponent<Renderer>().material.color = DefineColor(al);

		//if (aliment_inside.Count == 0)
			TimerCook = 10;
		/*else
		{
			TimerCook += 5;
			if (TimerCook > 10)
				TimerCook = 10;
		}*/
	}
	
	public bool Is_full()
	{
		if (aliment_inside.Count < 3)
			return false;
		return true;
	}
	
	public void empty_aliment()
	{
		TimeBar.gameObject.SetActive(false);
		aliment_inside = new List<string>();
		slot1.gameObject.SetActive(false);
		slot2.gameObject.SetActive(false);
		slot3.gameObject.SetActive(false);
		sauce.GetComponent<Renderer>().material = material_fond_casserole;
		sauce.tag = "Untagged";
		Is_Ready = false;
		ps.playOnAwake = false;
		ps.Pause();
		ps.Clear();
	}
	
	public bool IsTheSame(List<string> L1, List<string> L2)
	{
		int L1Size = L1.Count;
		if (L1Size != L2.Count)
		{
			return false;
		}
		
		L1.Sort();
		L2.Sort();
		
		for (int i = 0; i < L1Size; i++)
		{
			if (L1[i] != L2[i])
			{
				return false;
			}
		}
		return true;
	}


	private void OnGUI()
	{
		if (aliment_inside.Count > 0)
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

	public void BurnCook()
	{
		slot1.gameObject.SetActive(false);
		slot2.gameObject.SetActive(false);
		slot3.gameObject.SetActive(false);
		sauce.GetComponent<Renderer>().material.color = Burn;
		sauce.tag = "Burned";
		TimeBar.gameObject.SetActive(false);
		ps.playOnAwake = true;
		ps.Emit(10);
		ps.Play();
	}

	public void run_cook()
	{
		Is_Ready = true;
		if (aliment_inside.Count == 3)
		{
			slot1.gameObject.SetActive(false);
			slot2.gameObject.SetActive(false);
			slot3.gameObject.SetActive(false);
			if (IsTheSame(aliment_inside, recette_Tomato3))
			{
				Debug.Log("Cuisiner une soupe à la tomate");
				sauce.GetComponent<Renderer>().material.color = Couleur_tomates;
			}
			else if (IsTheSame(aliment_inside, recette_Carot3))
			{
				Debug.Log("Cuisiner une soupe à la Carotte");
				sauce.GetComponent<Renderer>().material.color = Couleur_carotte;
			}
			else if (IsTheSame(aliment_inside, recette_Carot2tomate) /*|| IsTheSame(aliment_inside, recette_Carot2tomateVS2) ||
			         IsTheSame(aliment_inside, recette_Carot2tomateVS3)*/)
			{
				Debug.Log("Cuisiner une soupe avec 2 carottes et 1 tomate");
				sauce.GetComponent<Renderer>().material.color = CouleurCarot2tomato;
			}
			else if (IsTheSame(aliment_inside, recette_Tomates2carot) /*|| IsTheSame(aliment_inside, recette_Tomates2carotVS2) ||
			         IsTheSame(aliment_inside, recette_Tomates2carotVS3)*/)
			{
				Debug.Log("Cuisiner une soupe avec 2 tomates et 1 carotte");
				sauce.GetComponent<Renderer>().material.color = CouleurTomato2Carot;
			}
			else
			{
				Debug.Log("Cuisiner une recette inconnue");
				sauce.GetComponent<Renderer>().material.color = Couleur_inconnue;
			}
			
		}
	}
}
