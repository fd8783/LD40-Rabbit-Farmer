using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {

    public Transform blood;

    private List<string> RabbitTags = new List<string> { "Rabbit", "RestingRabbit", "SickRabbit", "DeadRabbit", "Fragments" };

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (RabbitTags.Contains(col.tag))
        {
            Instantiate(blood, col.transform.position, Quaternion.identity);
            Destroy(col.gameObject);

        }
    }
}
