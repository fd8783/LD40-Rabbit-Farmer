using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuCtrl : MonoBehaviour {

    private BackGroundSetting bgscript;


    // Use this for initialization
    void Awake()
    {
        bgscript = GameObject.Find("BackGroundSetting").GetComponent<BackGroundSetting>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButton()
    {
        if (BackGroundSetting.firsttime)
        {
            SceneManager.LoadScene(4);
            BackGroundSetting.firsttime = false;
        }
        else
        {
            SceneManager.LoadScene(3);
            bgscript.GameStart();
        }
    }

    /*public void UpGradeButton()
    {
        SceneManager.LoadScene(4);
    }*/

    public void GuideButton()
    {
        SceneManager.LoadScene(4);
        BackGroundSetting.firsttime = false;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    
    public void RedoButton()
    {
        transform.Find("UI/RedoCan").gameObject.SetActive(true);
    }

    public void GiveUp()
    {
        SceneManager.LoadScene(7);
    }

    public void DontGiveUp()
    {
        transform.Find("UI/RedoCan").gameObject.SetActive(false);
    }
}
