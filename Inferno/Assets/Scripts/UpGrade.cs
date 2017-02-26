using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpGrade : MonoBehaviour
{
    public GameManager gameManager;
    public Text hitResistLevel, waterConsumeLevel, speedLevel, fanPerformLevel, fanBatteryLevel, fanChargerPerformLevel, fanEnergyConsumeLevel, Money, upgradeMoney, upgradediscription, upgradename;
    int[] needcost = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    enum Upgrades { HitResist, WaterConsume, Speed, BuyFan, FanPerform, FanBattery, BuyFanCharger, FanChargerPerform, FanEnergyConsume };
    public int upgradeCase = 0;
    public Sprite[] ItemList;
    public Image itemImage;
    
    public string[,] itemd = new string[9, 2];

    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.Inst();
        {
            needcost[(int)Upgrades.HitResist] = 100 * (int)Mathf.Pow(5, (gameManager.hitResistLevel));
            needcost[(int)Upgrades.WaterConsume] = 100 * (int)Mathf.Pow(5, gameManager.waterConsumeLevel);
            needcost[(int)Upgrades.Speed] = 500 * (int)Mathf.Pow(10, gameManager.speedLevel);
            needcost[(int)Upgrades.BuyFan] = 1000;
            needcost[(int)Upgrades.FanPerform] = 50 * (int)Mathf.Pow(2, gameManager.fanPerformLevel);
            needcost[(int)Upgrades.FanBattery] = 500 * (int)Mathf.Pow(gameManager.fanBatteryLevel + 1, 2) + 1;
            needcost[(int)Upgrades.BuyFanCharger] = 1500;
            needcost[(int)Upgrades.FanChargerPerform] = 1000 * (int)Mathf.Pow(3, gameManager.fanChargerLevel);
            needcost[(int)Upgrades.FanEnergyConsume] = 3000 * (int)Mathf.Pow(2, gameManager.fanEnergyConsumeLevel);
        }
        GameObject.Find("Money").GetComponent<Text>().text = gameManager.money.ToString();

        itemd[0, 0] = "열저항 업그레이드";itemd[0, 1] = "땡볕에 있을 때 체력 감소가 1레벨당 10%씩 줄어듭니다. (덧셈연산)";
        itemd[1, 0] = "수분 소모율 업그레이드"; itemd[1, 1] = "수분의 소모율이 1레벨당 10%씩 줄어듭니다. (덧셈연산)";
        itemd[2, 0] = "스피드 업그레이드";itemd[2, 1] = "캐릭터의 이동속도가 10%씩 상승합니다. (곱연산)";
        itemd[3, 0] = "미니선풍기 구매";itemd[3, 1] = "미니선풍기를 구매합니다.";
        itemd[4, 0] = "미니 선풍기 성능 업그레이드";itemd[4, 1] = "미니 선풍기가 감소시키는 열의 양이 10% 증가합니다. (덧셈연산)";
        itemd[5, 0] = "미니 선풍기 배터리 업그레이드";itemd[5, 1] = "미니 선풍기의 총 배터리의 양이 100 증가합니다.";
        itemd[6, 0] = "미니 선풍기 태양 충전기 구매";itemd[6, 1] = "미니 선풍기의 배터리를 햇볕 아래에서 충전시킬 수 있는 태양 충전기를 구매.";
        itemd[7, 0] = "태양 충전기 성능 업그레이드";itemd[7, 1] = "미니 선풍기가 햇볕 아래에서 충전되는 초당 배터리의 양이 5 증가합니다.";
        itemd[8, 0] = "선풍기 에너지 효율 업그레이드";itemd[8, 1] = "미니 선풍기의 초당 배터리 소모량을 10% 감소시킵니다. (곱연산)";
    }
    public void upgradeClick()
    {
        switch (upgradeCase)
        {
            case (int)Upgrades.HitResist:
                hitResistLevelUpgrade();
                break;
            case (int)Upgrades.WaterConsume:
                waterConsumeLevelUpgrade();
                break;
            case (int)Upgrades.Speed:
                speedLevelUpgrade();
                break;
            case (int)Upgrades.BuyFan:
                buyFan();
                break;
            case (int)Upgrades.FanPerform:
                fanPerfomLevelUpgrade();
                break;
            case (int)Upgrades.FanBattery:
                fanBatteryLevelUpgrade();
                break;
            case (int)Upgrades.BuyFanCharger:
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
        Money.text = gameManager.money.ToString();
        upgradeMoney.text = (int)needcost[upgradeCase] != -1 ? ((int)needcost[upgradeCase]).ToString() : "Level Complete";
    }
    public void hitResistLevelUpgrade()
    {
        if (gameManager.hitResistLevel < 5)
        {
            if (gameManager.money >= (int)needcost[(int)Upgrades.HitResist])
            {
                gameManager.hitResistLevel++;
                gameManager.money -= (int)needcost[(int)Upgrades.HitResist];
                needcost[(int)Upgrades.HitResist] = gameManager.hitResistLevel < 5 ? 100 * (int)Mathf.Pow(5, (gameManager.hitResistLevel)) : -1;
                hitResistLevel.text = string.Format("{0:D2}", gameManager.hitResistLevel) + " / 05";
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
    }
    public void waterConsumeLevelUpgrade()
    {
        if (gameManager.waterConsumeLevel < 5)
        {
            if (gameManager.money >= (int)needcost[(int)Upgrades.WaterConsume])
            {
                gameManager.waterConsumeLevel++;
                gameManager.money -= (int)needcost[(int)Upgrades.WaterConsume];
                needcost[(int)Upgrades.WaterConsume] = gameManager.waterConsumeLevel < 5 ? 100 * (int)Mathf.Pow(5, gameManager.waterConsumeLevel) : -1;
                waterConsumeLevel.text = string.Format("{0:D2}", gameManager.waterConsumeLevel) + " / 05";
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
        if (gameManager.speedLevel < 3)
        {
            if (gameManager.money >= (int)needcost[(int)Upgrades.Speed])
            {
                gameManager.speedLevel++;
                gameManager.money -= (int)needcost[(int)Upgrades.Speed];
                needcost[(int)Upgrades.Speed] = gameManager.speedLevel < 3 ? 500 * (int)Mathf.Pow(10, gameManager.speedLevel) : -1;
                speedLevel.text = string.Format("{0:D2}", gameManager.speedLevel) + " / 03";
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
        if (gameManager.fan == false)
        {
            if (gameManager.money >= (int)needcost[(int)Upgrades.BuyFan])
            {
                gameManager.fan = true;
                gameManager.money -= (int)needcost[(int)Upgrades.BuyFan];
                needcost[(int)Upgrades.BuyFan] = -1;
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
        if (gameManager.fan)
        {
            if (gameManager.fanPerformLevel < 10)
            {
                if (gameManager.money >= (int)needcost[(int)Upgrades.FanPerform])
                {
                    gameManager.fanPerformLevel++;
                    gameManager.money -= (int)needcost[(int)Upgrades.FanPerform];
                    needcost[(int)Upgrades.FanPerform] = gameManager.fanPerformLevel < 10 ? 50 * (int)Mathf.Pow(2, gameManager.fanPerformLevel) : -1;
                    fanPerformLevel.text = string.Format("{0:D2}", gameManager.fanPerformLevel) + " / 10";
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
        if (gameManager.fan)
        {
            if (gameManager.fanBatteryLevel < 4)
            {
                if (gameManager.money >= (int)needcost[(int)Upgrades.FanBattery])
                {
                    gameManager.fanBatteryLevel++;
                    gameManager.money -= (int)needcost[(int)Upgrades.FanBattery];
                    needcost[(int)Upgrades.FanBattery] = gameManager.fanBatteryLevel < 4 ? 500 * (int)Mathf.Pow(gameManager.fanBatteryLevel + 1, 2) + 1 : -1;
                    fanBatteryLevel.text = string.Format("{0:D2}", gameManager.fanBatteryLevel) + " / 04";
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
        if (gameManager.fan)
        {
            if (!gameManager.fanCharger)
            {
                if (gameManager.money >= (int)needcost[(int)Upgrades.BuyFanCharger])
                {
                    gameManager.fanCharger = true;
                    gameManager.money -= (int)needcost[(int)Upgrades.BuyFanCharger];
                    needcost[(int)Upgrades.BuyFanCharger] = -1;
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
        if (gameManager.fan)
        {
            if (gameManager.fanCharger)
            {
                if (gameManager.fanChargerLevel < 4)
                {
                    if (gameManager.money >= (int)needcost[(int)Upgrades.FanChargerPerform])
                    {
                        gameManager.fanChargerLevel++;
                        gameManager.money -= (int)needcost[(int)Upgrades.FanChargerPerform];
                        needcost[(int)Upgrades.FanChargerPerform] = gameManager.fanChargerLevel < 4 ? 1000 * (int)Mathf.Pow(3, gameManager.fanChargerLevel) : -1;
                        fanChargerPerformLevel.text = string.Format("{0:D2}", gameManager.fanChargerLevel) + " / 04";
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
        if (gameManager.fan)
        {
            if (gameManager.fanEnergyConsumeLevel < 3)
            {
                if (gameManager.money >= (int)needcost[(int)Upgrades.FanEnergyConsume])
                {
                    gameManager.fanEnergyConsumeLevel++;
                    gameManager.money -= (int)needcost[(int)Upgrades.FanEnergyConsume];
                    needcost[(int)Upgrades.FanEnergyConsume] = gameManager.fanEnergyConsumeLevel < 3 ? 3000 * (int)Mathf.Pow(2, gameManager.fanEnergyConsumeLevel) : -1;
                    fanEnergyConsumeLevel.text = string.Format("{0:D2}", gameManager.fanEnergyConsumeLevel) + " / 03";
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
    public void buttonClick(int upCase)
    {
        upgradeCase = upCase;
        upgradeMoney.text = (int)needcost[upgradeCase] != -1 ? ((int)needcost[upgradeCase]).ToString() : "Level Complete";

        itemImage.sprite = ItemList[upCase];
        upgradename.text = itemd[upCase, 0];
        upgradediscription.text = itemd[upCase, 1];
    }
}
