using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escapeCheck : MonoBehaviour {

    private List<string> deadRabbitTags = new List<string> { "DeadRabbit","Fragments" };
    private List<string> liveRabbitTags = new List<string> { "Rabbit", "RestingRabbit", "SickRabbit" };

    private stageCtrl stageController;

    // Use this for initialization
    void Awake () {
        stageController = GameObject.Find("stageManager").GetComponent<stageCtrl>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (liveRabbitTags.Contains(col.tag))
        {
            stageController.Escaped();
            Destroy(col.gameObject);
        }
        else if (deadRabbitTags.Contains(col.tag))
        {
            Destroy(col.gameObject);
        }
    }
}
