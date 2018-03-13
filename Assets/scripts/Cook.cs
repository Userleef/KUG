using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Graphs;
using UnityEngine;

public class Cook : Photon.MonoBehaviour
{

	public GameObject marmitte;
	
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
		marmitte.GetComponent<Cooking_Pot>().Add_aliment(al);
	}
	
	
	public bool Is_full()
	{
		return marmitte.GetComponent<Cooking_Pot>().Is_full();
	}

	public void run_cook()
	{
		marmitte.GetComponent<Cooking_Pot>().run_cook();
	}
}
