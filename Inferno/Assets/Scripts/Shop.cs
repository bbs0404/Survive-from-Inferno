using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public void Buy(itemList item)
    {
        if (isListFull() == 0) 
        {
            switch (item)
            {
                case itemList.WATERBOTTLE:  buyWaterBottle(); break;
                case itemList.BATTERY: buyBattery(); break;
            }
        }
        else Debug.Log("Your item list is full");
    }

    public int isListFull() 
    {
        foreach (Item i in GameManager.Inst().itemList) {
            if (i == null) {
                return 0;
            }
        }
        return 1;
    }

    public void buyWaterBottle()
    {
        if (Waterbottle.num >= 3) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < Waterbottle.amount[Waterbottle.num]) Debug.Log("You don't have enough money");
        else
        {
            Waterbottle water = new Waterbottle();
            for (int i = 0; i<3 ;i++)
            {
                if (GameManager.Inst().itemList[i] == null)
                {
                    GameManager.Inst().itemList[i] = water;
                }
            }
            Waterbottle.num++;
            GameManager.Inst().money -= Waterbottle.amount[Waterbottle.num];
        }
    }

    public void buyBattery() {
        if (Battery.num >= 3) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < Battery.amount[Battery.num]) Debug.Log("You don't have enough money");
        else {
            Battery battery = new Battery();
            for (int i = 0; i < 3; i++) {
                if (GameManager.Inst().itemList[i] == null) {
                    GameManager.Inst().itemList[i] = battery;
                }
            }
            Battery.num++;
            GameManager.Inst().money -= Battery.amount[Battery.num];
        }
    }

}
