using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windCtrl : MonoBehaviour {

    public GameObject[] winds;
    public float speed;

    public float leftPt, rightPt;
    private Vector2 spawnPos;

    private Transform tempWind;
    private int windsCount;

	// Use this for initialization
	void Awake () {
        windsCount = winds.Length;
        spawnPos = transform.position;
        spawnPos.x = rightPt;
        SpawnNew();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void SpawnNew()
    {
        tempWind = Instantiate(winds[Random.Range(0, windsCount)].transform, spawnPos, Quaternion.identity);
        tempWind.parent = transform;
        tempWind.GetComponent<wind>().Setting(speed, -1, (Mathf.Abs(leftPt) + Mathf.Abs(rightPt)), (Mathf.Abs(leftPt) + Mathf.Abs(rightPt))*1.5f);
    }
}
