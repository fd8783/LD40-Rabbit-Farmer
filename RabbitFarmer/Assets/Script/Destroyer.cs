using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float time;

	// Use this for initialization
	void Awake () {
        Destroy(gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
