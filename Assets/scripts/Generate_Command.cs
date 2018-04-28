using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate_Command : MonoBehaviour
{
	public Camera MainCamera;
		
	public bool free = true;
	public RawImage Slot_ingredient1;
	public RawImage Slot_ingredient2;
	public RawImage Slot_ingredient3;

	public float Timer_Command;
	public float Intervalle;
	public Image timeBarGreen;

	public List<string> recette_commande = new List<string>();
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer_Command > 0)
			Timer_Command -= Time.deltaTime;
	}

	public void Validate_Commande()
	{
		MainCamera.GetComponent<Command_Gestion>().Score += 10;
		MainCamera.GetComponent<Command_Gestion>().Update_Score();
		kill_command();
	}

	private void kill_command()
	{
		recette_commande = new List<string>();
		timeBarGreen.fillAmount = 1;
		gameObject.SetActive(false);
		free = true;
	}

	private void OnGUI()
	{
		if (Timer_Command > 0)
		{
			timeBarGreen.fillAmount = Timer_Command / Intervalle;
		}
		else
		{
			if (MainCamera.GetComponent<Command_Gestion>().Score >= 5)
			{
				MainCamera.GetComponent<Command_Gestion>().Score -= 5;
				MainCamera.GetComponent<Command_Gestion>().Update_Score();
			}
			kill_command();
		}
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

	public void generate_Command(List<Texture> Ingr)
	{
		Timer_Command = Intervalle;
		int place = Random.Range(0,Ingr.Count);
		Slot_ingredient1.GetComponent<RawImage>().texture = Ingr[place];
		recette_commande.Add(Ingr[place].name);
		
		place = Random.Range(0,Ingr.Count);
		Slot_ingredient2.GetComponent<RawImage>().texture = Ingr[place];
		recette_commande.Add(Ingr[place].name);
		
		place = Random.Range(0,Ingr.Count);
		Slot_ingredient3.GetComponent<RawImage>().texture = Ingr[place];
		recette_commande.Add(Ingr[place].name);

		recette_commande = Sort(recette_commande);
	}
}
