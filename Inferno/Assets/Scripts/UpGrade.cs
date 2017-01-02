using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum upgrades {
    SPEED,
    FAN
}

public class UpGrade : MonoBehaviour {
    public GameManager gameManager;
    
	// Use this for initialization
	void Start () {
        gameManager = GameManager.Inst();
    }
	
	// Update is called once per frame
	void Update () {
	}


    public void hitResistLevelUpgrade()
    {
        double needcost = 100 * Mathf.Pow(5, (gameManager.hitResistLevel - 1));
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
    }
    public void acde(upgrades c) {
        switch(c) {
            case upgrades.FAN:
                
                break;
            default:
                break;
        }
    }
    public void waterConsumeLevelUpgrade()
    {
        double needcost = 100 * Mathf.Pow(5, gameManager.waterConsumeLevel - 1);
        if(gameManager.waterConsumeLevel<5)
        {
            if(gameManager.money>=needcost)
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

    public void buyfan()
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

    public void fanBetteryLevelUpgrade()
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

    public void buysolar()
    {
        double needcost = 1000;
        if (gameManager.fan)
        {
            if (gameManager.fanCharger)
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
}
