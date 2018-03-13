using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCtrl : MonoBehaviour {

    public static bool atNight = true;

    public static int curDay = 0;

    private TextMeshProUGUI dayText;

    private float startTime, oneDayTime = 40, remainTime;

    private Camera mainCam;

    private UpgradeCtrl upgradeScript;

    // Use this for initialization
    void Awake () {
        mainCam = Camera.main;
        dayText = transform.Find("Day").GetComponent<TextMeshProUGUI>();
        upgradeScript = GameObject.Find("Main Camera/UpgradePage").GetComponent<UpgradeCtrl>();
        //NewDay();
    }
	
	// Update is called once per frame
	void Update () {
        remainTime = oneDayTime - (Time.time - startTime);
        if (remainTime <= 0)
        {
            EndDay();
        }
        else
        {
            mainCam.backgroundColor = Color.HSVToRGB((float)216 / 359, (float)96 / 255, (30 + (225 * (remainTime / oneDayTime))) / 255);
        }
    }

    public void NewDay()
    {
        curDay++;
        dayText.text = "DAY " + curDay.ToString();
        startTime = Time.time;
        mainCam.backgroundColor = Color.HSVToRGB((float)216 / 359, (float)96 / 255, 1);
        atNight = false;
    }

    public void EndDay()
    {
        atNight = true;
        upgradeScript.Open();
    }
}
