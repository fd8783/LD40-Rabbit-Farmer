using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallManager : MonoBehaviour {

    public GameObject[] walls;
    private int curWall = 0;

    private Transform mainCam;
    private SceenShake screenCtrl;

	// Use this for initialization
	void Awake () {
        mainCam = GameObject.Find("Main Camera").transform;
        screenCtrl = GameObject.Find("sceenCtrl").GetComponent<SceenShake>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpgradeWall(int level)
    {
        if (level == 2)
        {
            walls[1].SetActive(true);
            walls[0].SetActive(false);
            Camera.main.orthographicSize = 1f;
            Vector3 tempPos = mainCam.position;
            tempPos.x -= 0.1f;
            tempPos.y += 0.18f;
            mainCam.position = tempPos;
        }
        else if (level == 3)
        {
            walls[2].SetActive(true);
            walls[1].SetActive(false);
            Camera.main.orthographicSize = 1.3f;
            Vector3 tempPos = mainCam.position;
            tempPos.x -= 0.40f;
            tempPos.y += 0.25f;
            mainCam.position = tempPos;
        }
        screenCtrl.UpdateCamPos();
    }
}
