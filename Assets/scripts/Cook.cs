using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cook : Photon.MonoBehaviour
{
	public List<string> aliment_inside;
	public Vector3[] Slots = new Vector3[3];
	public GameObject sauce;

	public List<string> recette_Tomato3;
	public List<string> recette_Carot3;
	
    // Use this for initialization
    void Start ()
    {
	    aliment_inside = new List<string>();
	    Slots[0] = new Vector3(0.3f, 1.8f, 0f);
	    Slots[1] = new Vector3(0f, 1.8f, 0.3f);
	    Slots[2] = new Vector3(-0.3f, 1.8f, 0f);
	    
	    recette_Tomato3 = new List<string>{"F Tomato","F Tomato","F Tomato"};
	    recette_Carot3 = new List<string>{"F Carot","F Carot","F Carot"};
    }
	
	// Update is called once per frame
	void Update ()
    {

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
				if (L[i][2] > L[i - 1][2])
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

	public void Add_aliment(GameObject al)
	{
		aliment_inside.Add(al.tag);
	}
	
	public void empty_aliment()
	{
		aliment_inside = new List<string>();
	}
	
	public bool Is_full()
	{
		if (aliment_inside.Count < 3)
			return false;
		return true;
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
