using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_aliment : MonoBehaviour {

    public GameObject Food_inside;
	
	private bool have_food = false;
	private GameObject food_on;
    
    // Use this for initialization
    void Start () {
	}

	// Update is called once per frame
	void Update () {

    }

	public bool HaveFood
	{
		get { return have_food; }
		set { have_food = value; }
	}

	public GameObject FoodOn
	{
		get { return food_on; }
		set { food_on = value; }
	}
}
