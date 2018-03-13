using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour {

    private float startX;

    private float speed, spawnNewDis, destroyDis;
    private int dir;

    private Vector2 targetPos;

    private windCtrl controller;

    private bool spawnedNew = false;

	// Use this for initialization
	void Start () {
        startX = transform.position.x;
        controller = transform.parent.GetComponent<windCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawnedNew)
        {
            if (Mathf.Abs(startX - transform.position.x) >= spawnNewDis)
            {
                controller.SpawnNew();
                spawnedNew = true;
            }
        }
        else
        {
            if (Mathf.Abs(startX - transform.position.x) >= destroyDis)
            {
                Destroy(gameObject);
            }
        }
        targetPos = transform.position;
        targetPos.x += dir * speed;
        transform.position = targetPos;
	}

    public void Setting(float speed, int dir, float spawnNewDis, float destroyDis)
    {
        this.speed = speed;
        this.dir = dir;
        this.spawnNewDis = spawnNewDis;
        this.destroyDis = destroyDis;
    }
}
