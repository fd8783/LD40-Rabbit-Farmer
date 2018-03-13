using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackGroundSetting : MonoBehaviour{

    public static BackGroundSetting Instance;

    public static bool firsttime = true;

    private Transform mainUI;
    private TextMeshProUGUI fundsText;

    public static int cumulatedFunds = 1200, curFunds=1200;

    // Use this for initialization
    void Awake () {
        /*if (Instance = null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);*/

        mainUI = GameObject.Find("Main Camera/UI").transform;
        fundsText = mainUI.Find("Funds").GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update () {


        fundsText.text = "Funds: $" + curFunds.ToString();

	}

    public void GameStart()
    {
        DayCtrl.curDay = 0;
        DayCtrl.atNight = true;
        
    }


    public void GameOver()
    {
        curFunds = cumulatedFunds /3;
        cumulatedFunds = curFunds;
    }

    static public void FundsEarned(int amount)
    {
        curFunds += amount;
        cumulatedFunds += amount;
    }

    static public void FundsComsumed(int amount)
    {
        curFunds -= amount;
    }

}
