using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class anyKeyToMenu : MonoBehaviour {
    private BackGroundSetting bgscript;

    // Use this for initialization
    void Awake () {
        bgscript = GameObject.Find("BackGroundSetting").GetComponent<BackGroundSetting>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            bgscript.GameOver();
            SceneManager.LoadScene(2);
        }
	}
}
