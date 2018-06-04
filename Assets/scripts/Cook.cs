using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using UnityEditor.Graphs;
using UnityEngine;

public class Cook : Photon.MonoBehaviour
{

	public GameObject marmitte;
	public AudioSource audio;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void Add_aliment(GameObject al)
	{
		if(marmitte.name == "Casserole")
			marmitte.GetComponent<Cooking_Pot>().Add_aliment(al);
		if (marmitte.name == "Stove")
		{
			marmitte.GetComponent<Stove>().Add_aliment(al);
			audio.mute = false;
		}
	}
	
	
	public bool Is_full()
	{
		if (marmitte.name == "Casserole")
			return marmitte.GetComponent<Cooking_Pot>().Is_full();
		return marmitte.GetComponent<Stove>().Is_full();
	}

	public void Take()
	{
		if (marmitte.name == "Casserole")
			marmitte.GetComponent<Cooking_Pot>().On_Cooking_Place = false;
		if (marmitte.name == "Stove")
		{
			marmitte.GetComponent<Stove>().On_Cooking_Place = false;
			audio.mute = true;
		}
	}
	
	public void Put()
	{
		if (marmitte.name == "Casserole")
			marmitte.GetComponent<Cooking_Pot>().On_Cooking_Place = true;
		if (marmitte.name == "Stove")
		{
			marmitte.GetComponent<Stove>().On_Cooking_Place = true;
			audio.mute = false;
		}
	}

}
