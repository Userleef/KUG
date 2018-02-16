using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour {
    
    public GameObject Casserole;
    private bool is_inside = false;

    private void OnTriggerStay(Collider other)
    {
        is_inside = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Space) && is_inside)
        {
            Instantiate(Casserole, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        }

        is_inside = false;

    }
}
