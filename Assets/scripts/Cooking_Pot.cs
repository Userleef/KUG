using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;

public class Cooking_Pot : MonoBehaviour
{
	
	public List<string> aliment_inside;
	
	public Transform slot1;
	public Transform slot2;
	public Transform slot3;

	public Material material_fond_casserole;
	
	public List<string> recette_Tomato3;
	public List<string> recette_Carot3;

	public GameObject sauce;
	
	// Use this for initializatio
	void Start ()
	{
		aliment_inside = new List<string>();
	    
		recette_Tomato3 = new List<string>{"FC Tomato","FC Tomato","FC Tomato"};
		recette_Carot3 = new List<string>{"FC Carot","FC Carot","FC Carot"};
	}
	
	// Update is called once per frame
	void Update () {
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
			return UnityEngine.Color.red;
		if(al.tag == "FC Carot")
			return UnityEngine.Color.yellow;
		
		return UnityEngine.Color.white;
	}
	
	public void Add_aliment(GameObject al)
	{
		aliment_inside.Add(al.tag);
		al.SetActive(false);
		DefineSlot(aliment_inside).GetComponent<Renderer>().material.color = DefineColor(al);
	}
	
	public bool Is_full()
	{
		if (aliment_inside.Count < 3)
			return false;
		return true;
	}
	
	public void empty_aliment()
	{
		aliment_inside = new List<string>();
		slot1.GetComponent<Renderer>().material.color = Color.white;
		slot2.GetComponent<Renderer>().material.color = Color.white;
		slot3.GetComponent<Renderer>().material.color = Color.white;
		sauce.GetComponent<Renderer>().material = material_fond_casserole;
	}
	
	public bool IsTheSame(List<string> L1, List<string> L2)
	{
		int L1Size = L1.Count;
		if (L1Size != L2.Count)
		{
			return false;
		}
		
		L1 = Sort(L1);
		L2 = Sort(L2);
		
		for (int i = 0; i < L1Size; i++)
		{
			if (L1[i] != L2[i])
			{
				return false;
			}
		}
		return true;
	}

	public List<string> Sort(List<string> L)
	{
		bool sorted = false;

		while (!sorted)
		{
			sorted = true;
			for (int i = 1; i < L.Count; i++)
			{
				if (L[i][3] > L[i - 1][3])
				{
					sorted = false;
					string cur = L[i];
					L[i] = L[i - 1];
					L[i - 1] = cur;
				}
			}
		}

		return L;
	}

	
	public void run_cook()
	{
		if (aliment_inside.Count == 3)
		{
			if (IsTheSame(aliment_inside,recette_Tomato3))
			{
				Debug.Log("Cuisiner une soupe à la tomate");
				sauce.GetComponent<Renderer>().material.color = Color.red;
			}
			else if (IsTheSame(aliment_inside,recette_Carot3))
			{
				Debug.Log("Cuisiner une soupe à la Carotte");
				sauce.GetComponent<Renderer>().material.color = Color.yellow;
			}
		}
	}
	
}
