using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_aliment : MonoBehaviour {

    public GameObject Grab_object;
    private bool is_inside;

    private void OnTriggerStay(Collider other)
    {
        is_inside = true;
    }
    
    // Use this for initialization
    void Start () {
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.C) && is_inside)
        {
            Instantiate(Grab_object, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        }

        is_inside = false;
    }
}
