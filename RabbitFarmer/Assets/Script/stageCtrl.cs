using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class stageCtrl : MonoBehaviour {

    //level setting, now 7 day first//
    private int[] escapeLimit = { 10, 10, 10, 10, 10, 10, 10 };
    private int[] targetGold = { 400, 800, 1600, 3200, 6400, 9600, 12800};
    static private float[] growSpeed = { 1.0015f, 1.0015f, 1.0015f, 1.002f, 1.002f, 1.002f, 1.0025f};
    static private float[] sexRestTime = { 2f, 1.93f, 1.85f, 1.75f, 1.7f, 1.6f, 1.5f};
    //******************************//

    private int curDay;
    private bool dayStarted = false;

    private Transform mainUI;
    private TextMeshProUGUI escapeLimitText, targetGoldText;

    private int escapeCount;

    // Use this for initialization
    void Awake ()
    {
        mainUI = GameObject.Find("Main Camera/UI").transform;
        escapeLimitText = mainUI.Find("Escape/Limit").GetComponent<TextMeshProUGUI>();
        targetGoldText = mainUI.Find("TargetGold").GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!DayCtrl.atNight)
        {
            if (!dayStarted)
            {
                curDay = DayCtrl.curDay;
                dayStarted = true;
            }

            escapeLimitText.text = escapeCount.ToString() + "\n  / \n    " + escapeLimit[curDay - 1].ToString();
            targetGoldText.text = "TODAY'S TARGET\n$" + targetGold[curDay - 1].ToString();
        }
        else
        {
            if (dayStarted)
            {
                CheckLose();
                dayStarted = false;
            }
        }
	}

    public void Escaped()
    {
        escapeCount++;
    }

    static public float GetGrowSpeed()
    {
        return growSpeed[DayCtrl.curDay-1];
    }

    static public float GetSexRestTime()
    {
        return sexRestTime[DayCtrl.curDay-1];
    }

    public void CheckLose()
    {
        Debug.Log("11");
        if (escapeCount > escapeLimit[curDay - 1])
        {
            Debug.Log("111");
            SceneManager.LoadScene(6);
        }
        else if (BackGroundSetting.curFunds < targetGold[curDay - 1])
        {
            Debug.Log("1111");
            SceneManager.LoadScene(7);
        }
        else  if (curDay >= 7)
        {
            Debug.Log("11111");
            SceneManager.LoadScene(5); //win!
        }
    }
}
