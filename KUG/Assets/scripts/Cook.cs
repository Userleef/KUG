using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cook : Photon.MonoBehaviour
{
	public List<GameObject> aliment_inside;
	public Vector3[] Slots = new Vector3[3];
	public GameObject sauce;

	public List<string> recette_1;
	
    // Use this for initialization
    void Start ()
    {
	    aliment_inside = new List<GameObject>();
	    Slots[0] = new Vector3(0.3f, 1.8f, 0f);
	    Slots[1] = new Vector3(0f, 1.8f, 0.3f);
	    Slots[2] = new Vector3(-0.3f, 1.8f, 0f);
	    
	    recette_1 = new List<string>();
	    recette_1.Add("F Tomato");
	    recette_1.Add("F Tomato");
	    recette_1.Add("F Tomato");
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

	public void Add_aliment(GameObject al)
	{
		aliment_inside.Add(al);
	}
	
	public void empty_aliment()
	{
		aliment_inside = new List<GameObject>();
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
			/*foreach (GameObject go in aliment_inside)
			{
				go.transform.position = new Vector3(-50f, -50f, -50f);
			}*/
			sauce.GetComponent<Renderer>().material.color = Color.red;
			//empty_aliment();
		}
	}
}
