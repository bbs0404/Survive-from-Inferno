using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGrade : MonoBehaviour {
    public GameManager gameManager;
    public Text hitResistLevel, waterConsumeLevel, speedLevel, fanPerformLevel, fanBatteryLevel, fanChargerPerformLevel, fanEnergyConsumeLevel, Money,upgradeMoney;

    public enum Upgrades { HitResist, WaterConsume, Speed, Fan, FanPerform, FanBattery, FanCharger, FanChargerPerform, FanEnergyConsume };

    public int upgradeCase = 0;
    public int needcost;

    // Use this for initialization
    void Start() {
        gameManager = GameManager.Inst();

        hitResistLevel = GameObject.Find("hitResistLevel").GetComponent<Text>();
        waterConsumeLevel = GameObject.Find("waterConsumeLevel").GetComponent<Text>();
        speedLevel = GameObject.Find("speedLevel").GetComponent<Text>();
        fanPerformLevel = GameObject.Find("fanPerformLevel").GetComponent<Text>();
        fanBatteryLevel = GameObject.Find("fanBatteryLevel").GetComponent<Text>();
        fanChargerPerformLevel = GameObject.Find("fanChargerPerformLevel").GetComponent<Text>();
        fanEnergyConsumeLevel = GameObject.Find("fanEnergyConsumeLevel").GetComponent<Text>();

        Money = GameObject.Find("Money").GetComponent<Text>();
        upgradeMoney = GameObject.Find("upgradeMoney").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        waterConsumeLevel.text = string.Format("{0:D2}", gameManager.waterConsumeLevel) + " / 05";
        speedLevel.text = string.Format("{0:D2}", gameManager.speedLevel) + " / 03";
        fanPerformLevel.text = string.Format("{0:D2}",gameManager.fanPerformLevel) + " / 10";
        fanBatteryLevel.text = string.Format("{0:D2}", gameManager.fanBatteryLevel) + " / 04";
        fanChargerPerformLevel.text = string.Format("{0:D2}", gameManager.fanChargerLevel) + " / 04";
        fanEnergyConsumeLevel.text = string.Format("{0:D2}", gameManager.fanEnergyConsumeLevel) + " / 03";

        Money.text = "￦" + gameManager.money;

    }

    public void upgradeClick()
    {
        switch (upgradeCase) {
            case (int)Upgrades.HitResist:
                needcost = costCalculate(upgradeCase);
                UserInterfaceManager.Inst().updateUpgradeCostText(needcost);
                hitResistLevelUpgrade();
                break;
            case (int)Upgrades.WaterConsume:
                waterConsumeLevelUpgrade();
                break;
            case (int)Upgrades.Speed:
                speedLevelUpgrade();
                break;
            case (int)Upgrades.Fan:
                buyFan();
                break;
            case (int)Upgrades.FanPerform:
                fanPerfomLevelUpgrade();
                break;
            case (int)Upgrades.FanBattery:
                fanBatteryLevelUpgrade();
                break;
            case (int)Upgrades.FanCharger:
                buyFanCharger();
                break;
            case (int)Upgrades.FanChargerPerform:
                fanChargerLevelUpgrade();
                break;
            case (int)Upgrades.FanEnergyConsume:
                fanEnergyConsumeLevelUpgrade();
                break;
            default:
                break;
        }
    }

    public void hitResistLevelUpgrade()
    { 
        if (gameManager.hitResistLevel < 5)
        {
            if (gameManager.money >= needcost)
            {
                gameManager.hitResistLevel++;
                gameManager.money -= (int)needcost;
            }
            else
            {
                Debug.Log("Not enough money.");//돈 부족
            }
        }
        else
        {
            Debug.Log("Level Complete.");//이미 만렙
        }
        hitResistLevel.text = string.Format("{0:D2}", gameManager.hitResistLevel) + " / 05";
    }

    public void waterConsumeLevelUpgrade()
    {
        double needcost = 100 * Mathf.Pow(5, gameManager.waterConsumeLevel - 1);
        if (gameManager.waterConsumeLevel < 5)
        {
            if (gameManager.money >= needcost)
            {
                gameManager.waterConsumeLevel++;
                gameManager.money -= (int)needcost;
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }
        else
        {
            Debug.Log("Level Complete.");
        }
    }

    public void speedLevelUpgrade()
    {
        double needcost = 500 * Mathf.Pow(10, gameManager.speedLevel - 1);
        if (gameManager.speedLevel < 3)
        {
            if (gameManager.money >= needcost)
            {
                gameManager.speedLevel++;
                gameManager.money -= (int)needcost;
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }
        else
        {
            Debug.Log("Level Complete.");
        }
    }

    public void buyFan()
    {
        double needcost = 500;
        if (gameManager.fan == false)
        {
            if (gameManager.money >= needcost)
            {
                gameManager.fan = true;
                gameManager.money -= (int)needcost;
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }
        else
        {
            Debug.Log("You already have fan.");
        }
    }

    public void fanPerfomLevelUpgrade()
    {
        double needcost = 50 * Mathf.Pow(2, gameManager.fanPerformLevel - 1);
        if (gameManager.fan)
        {
            if (gameManager.fanPerformLevel < 10)
            {
                if (gameManager.money >= needcost)
                {
                    gameManager.fanPerformLevel++;
                    gameManager.money -= (int)needcost;
                }
                else
                {
                    Debug.Log("Not enough money.");
                }
            }
            else
            {
                Debug.Log("Level Complete.");
            }
        }
        else
        {
            Debug.Log("You don't have fan.");
        }
    }

    public void fanBatteryLevelUpgrade()
    {
        double needcost = 500 * Mathf.Pow(gameManager.fanBatteryLevel, 2) + 1;
        if (gameManager.fan)
        {
            if (gameManager.fanBatteryLevel < 4)
            {
                if (gameManager.money >= needcost)
                {
                    gameManager.fanBatteryLevel++;
                    gameManager.money -= (int)needcost;
                }
                else
                {
                    Debug.Log("Not enough money.");
                }
            }
            else
            {
                Debug.Log("Level Complete.");
            }
        }
        else
        {
            Debug.Log("You don't have fan.");
        }
    }

    public void buyFanCharger()
    {
        double needcost = 1000;
        if (gameManager.fan)
        {
            if (!gameManager.fanCharger)
            {
                if (gameManager.money >= needcost)
                {
                    gameManager.fanCharger = true;
                    gameManager.money -= (int)needcost;
                }
                else
                {
                    Debug.Log("Not enough money.");
                }
            }
            else
            {
                Debug.Log("You already have fanCharger.");
            }
        }
        else
        {
            Debug.Log("You don't have fan.");
        }
    }

    public void fanChargerLevelUpgrade()
    {
        double needcost = 1000 * Mathf.Pow(3, gameManager.fanChargerLevel - 1);
        if (gameManager.fan)
        {
            if (gameManager.fanCharger)
            {
                if (gameManager.fanChargerLevel < 4)
                {
                    if (gameManager.money >= needcost)
                    {
                        gameManager.fanChargerLevel++;
                        gameManager.money -= (int)needcost;
                    }
                    else
                    {
                        Debug.Log("Not enough money.");
                    }
                }
                else
                {
                    Debug.Log("Level Complete.");
                }
            }
            else
            {
                Debug.Log("You don't have fancharger.");
            }
        }
        else
        {
            Debug.Log("You don't have fan.");
        }
    }

    public void fanEnergyConsumeLevelUpgrade()
    {
        double needcost = 3000 * Mathf.Pow(2, gameManager.fanEnergyConsumeLevel - 1);
        if (gameManager.fan)
        {
            if (gameManager.fanEnergyConsumeLevel < 3)
            {
                if (gameManager.money >= needcost)
                {
                    gameManager.fanEnergyConsumeLevel++;
                    gameManager.money -= (int)needcost;
                }
                else
                {
                    Debug.Log("Not enough money.");
                }
            }
            else
            {
                Debug.Log("Level Complete.");
            }
        }
        else
        {
            Debug.Log("You don't have fan.");
        }
    }
    
    public void buttonClick(int type)
    {
        upgradeCase = type;
        needcost = costCalculate(type);
        UserInterfaceManager.Inst().updateUpgradeCostText(needcost);
    }

    public int costCalculate(int type)
    {
        switch (type)
        {
            case 0:
                return needcost = (int)(100 * Mathf.Pow(5, gameManager.hitResistLevel));
            default:
                Debug.LogError("coding jotgachi hane");
                return -1;
        }
    }

    public void hitResistButtonClick() {
        upgradeCase = (int)Upgrades.HitResist;
    }
    public void waterConsumeButtonClick() {
        upgradeCase = (int)Upgrades.WaterConsume;
    }
    public void speedButtonClick() {
        upgradeCase = (int)Upgrades.Speed;
    }
    public void fanButtonClick() {
        upgradeCase = (int)Upgrades.Fan;
    }
    public void fanPerformButtonClick() {
        upgradeCase = (int)Upgrades.FanPerform;
    }
    public void fanBatteryButtonClick() {
        upgradeCase = (int)Upgrades.FanBattery;
    }
    public void fanChargerButtonClick() {
        upgradeCase = (int)Upgrades.FanCharger;
    }
    public void fanChargerPerformButtonClick() {
        upgradeCase = (int)Upgrades.FanChargerPerform;
    }
    public void fanEnergyConsumeButtonClick() {
        upgradeCase = (int)Upgrades.FanEnergyConsume;
    }
}

public class UpgradeClass : SingletonBehaviour<UpgradeClass> {
    public GameManager gameManager = GameManager.Inst();
    public UpGrade upgrade;
    public double needcost;
    public int option;
    public int id;
    public int Max_Level;
    
}

public class HitResist : UpgradeClass {

    public HitResist() {
        this.needcost = 100 * Mathf.Pow(5, (gameManager.hitResistLevel - 1));
        this.id = 0;
        this.Max_Level = 5;
    }
    

}