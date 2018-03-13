using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCtrl : MonoBehaviour {

    private BackGroundSetting bgscript;
    private DayCtrl dayController;

    //private int cageLv = 1, meatGrinderLv = 1, grabHandLv = 1, gunLv = 0, rpgLv = 0;
    private int[] levelSave = { 1, 1, 1, 0, 0 };

    //private int[] cagePrice = { 300, 1000, 3000 },
    //              meatGrinderPrice = { 300, 1000, 3000 },
    //              grabHandPrice = { 300, 1000, 3000 },
    //              gunPrice = { 300, 800, 2500 },
    //              rpgPrice = { 300, 800, 2500 };  

    private int[,] priceSave = new int[,] {  { 300, 800, 1500,0 },
                                             { 300, 800, 1500,0 },
                                             { 300, 800, 1500,0 },
                                             { 300, 800, 1500,0 },
                                             { 300, 800, 1500,0 }  };

    private int selectedItem = -1;

    private TextMeshProUGUI funds, description, itemName;
    private TextMeshProUGUI cageText, meatGrinderText, grabHandText, gunText, rpgText;

    public GameObject wallObj, grinderObj ,handObj, gunObj, rpgObj;

    private float[] grinderScaleVaule = { 0, 1.5f, 1.5f, 1.5f };

    private float[] handScaleValue = {0, 2.5f, 3f, 3f };

    private float[] gunFireRate = { 0, 0.5f, 0.3f, 0.15f };
    
    private float[] rpgFireRate = { 0, 2f, 1.5f, 1f };

    // Use this for initialization
    void Awake()
    {
        bgscript = GameObject.Find("BackGroundSetting").GetComponent<BackGroundSetting>();
        dayController = GameObject.Find("Main Camera/UI").GetComponent<DayCtrl>();

        funds = transform.Find("Panel/Funds").GetComponent<TextMeshProUGUI>();
        description = transform.Find("Panel/Description/Describe").GetComponent<TextMeshProUGUI>();
        itemName = transform.Find("Panel/Description/Name").GetComponent<TextMeshProUGUI>();

        cageText = transform.Find("Panel/Cage").GetComponent<TextMeshProUGUI>();
        meatGrinderText = transform.Find("Panel/meatGrinder").GetComponent<TextMeshProUGUI>();
        grabHandText = transform.Find("Panel/GrabHand").GetComponent<TextMeshProUGUI>();
        gunText = transform.Find("Panel/Gun").GetComponent<TextMeshProUGUI>();
        rpgText = transform.Find("Panel/RPG").GetComponent<TextMeshProUGUI>();

       // gunScript = GameObject.Find("Gun").GetComponent<gun>();
       // rpgScript = GameObject.Find("RPG").GetComponent<RPG>();

        for (int i =0; i< 5; i++)
        {
            UpdatePrice(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        funds.text = "Funds: $" + BackGroundSetting.curFunds.ToString();
        
    }


    void UpdatePrice(int num)
    {
        switch (num)
        {
            case 0:
                cageText.text = "LV. " + levelSave[num].ToString() + "\n$" + priceSave[num,levelSave[num]].ToString();
                break;
            case 1:
                meatGrinderText.text = "LV. " + levelSave[num].ToString() + "\n$" + priceSave[num, levelSave[num]].ToString();
                break;
            case 2:
                grabHandText.text = "LV. " + levelSave[num].ToString() + "\n$" + priceSave[num, levelSave[num]].ToString();
                break;
            case 3:
                gunText.text = "LV. " + levelSave[num].ToString() + "\n$" + priceSave[num, levelSave[num]].ToString();
                break;
            case 4:
                rpgText.text = "LV. " + levelSave[num].ToString() + "\n$" + priceSave[num, levelSave[num]].ToString();
                break;
            default:
                break;
        }
    }

    public void Selected(int num)
    {
        selectedItem = num;

        switch (selectedItem)
        {
            case -1:
                itemName.text = "";
                description.text = "";
                break;
            case 0:
                itemName.text = "Cage";
                description.text = "Upgrade to get larger space";
                break;
            case 1:
                itemName.text = "Meat Grinder";
                description.text = "Upgrade to increase the size and grinding speed";
                break;
            case 2:
                itemName.text = "Grab Hand";
                description.text = "Upgrade to increase the range you can grab (green sphere)";
                break;
            case 3:
                itemName.text = "Gun >1bullet = $10";
                description.text = "Upgrade to increase fire rate";
                break;
            case 4:
                itemName.text = "RPG >1rocket = $level*100"; 
                description.text = "Upgrade explosion range and fire rate";
                break;
            default:
                itemName.text = "";
                description.text = "";
                break;
        }
    }

    public void Continue()
    {
        selectedItem = -1;
        gameObject.SetActive(false);
        dayController.NewDay();
    }

    public void BuyButton()
    {
        if (selectedItem != -1)
        {
            Buy(selectedItem);
        }
    }

    public void Buy(int num)
    {
        int tempPrice = priceSave[num, levelSave[num]];
        if (tempPrice != 0 && BackGroundSetting.curFunds >= tempPrice)
        {
            BackGroundSetting.FundsComsumed(tempPrice);
            levelSave[num]++;
            UpdatePrice(num);

            switch (num)
            {
                case 0:
                    wallObj.GetComponent<wallManager>().UpgradeWall(levelSave[num]);
                    break;
                case 1:
                    grinderObj.GetComponent<meatGrinder>().UpgradeGrinder(grinderScaleVaule[levelSave[num]], 2f);
                    break;
                case 2:
                    handObj.GetComponent<mouseCtrl>().UpgradeGrabHand(handScaleValue[levelSave[num]]);
                    break;
                case 3:
                    gunObj.GetComponent<gun>().UpdateGunLevel(gunFireRate[levelSave[num]], 10);
                    break;
                case 4:
                    rpgObj.GetComponent<RPG>().UpdateRPGLevel(rpgFireRate[levelSave[num]], levelSave[num]);
                    break;
                default:
                    break;
            }
        }
    }

    public int GetLv(int num) //0 = cage, 1 = meat grinder, 2 = grab hand, 3 = gun, 4 = rpg
    {
        return levelSave[num];
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }


}
