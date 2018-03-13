using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseManager : MonoBehaviour {

    public GameObject[] mouses;

    private UpgradeCtrl upgradeScript;

    private int defaultMouse = 1, curMouse;

    private bool wakeUp = false;

    private bool gunAble = false, rpgAble = false;

	// Use this for initialization
	void Awake () {
        curMouse = defaultMouse;

        mouses[curMouse].SetActive(true);

        upgradeScript = GameObject.Find("Main Camera/UpgradePage").GetComponent<UpgradeCtrl>();
    }
	
	// Update is called once per frame
	void Update () {
		if (DayCtrl.atNight)
        {
            if (wakeUp)
            {
                mouses[curMouse].SetActive(false);
                curMouse = defaultMouse;
                mouses[curMouse].SetActive(true);

                wakeUp = false;
            }
        }
        else
        {
            CheckKey();
            if (!wakeUp)
            {
                wakeUp = true;
                CheckWeaponAble();
            }
        }
	}

    void CheckKey()
    {
        if (Input.GetKeyDown("1"))
        {
            ChangeMouse(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            ChangeMouse(1);
        }
        else if (gunAble && Input.GetKeyDown("3"))
        {
            ChangeMouse(2);
        }
        else if (rpgAble && Input.GetKeyDown("4"))
        {
            ChangeMouse(3);
        }
    }

    void CheckWeaponAble()
    {
        gunAble = (upgradeScript.GetLv(3) > 0);
        rpgAble = (upgradeScript.GetLv(4) > 0);
    }

    public void ChangeMouse(int num)
    {
        if (!DayCtrl.atNight)
        {
            if (curMouse != num)
            {
                mouses[num].transform.position = mouses[curMouse].transform.position;
                mouses[curMouse].SetActive(false);
                curMouse = num;
                mouses[curMouse].SetActive(true);
            }
        }
    }
}
