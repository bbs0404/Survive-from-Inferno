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
        else if (GameManager.Inst().money < Waterbottle.cost[Waterbottle.num]) Debug.Log("You don't have enough money");
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
            GameManager.Inst().money -= Waterbottle.cost[Waterbottle.num];
        }
    }

    public void buyBattery()
    {
        int num = 0;
        int[] cost =  { 500, 1000, 2500 } ;
        foreach(Item i in GameManager.Inst().itemList)
        {
            if (i.type == itemList.BATTERY) num++;
        }
        if (num >= 3) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            Battery battery = new Battery();
            for (int i = 0; i < 3; i++) {
                if (GameManager.Inst().itemList[i] == null) {
                    GameManager.Inst().itemList[i] = battery;
                }
            }
            GameManager.Inst().money -= cost[num];
        }
    }
    
    public void buyBBong()
    {
        if (BBong.num >= 1) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < BBong.cost) Debug.Log("You don't have enough money");
        else
        {
            BBong bbong = new BBong();
            for (int i = 0; i<3;i++)
            {
                if (GameManager.Inst().itemList[i] == null)
                {
                    GameManager.Inst().itemList[i] = bbong;
                }
            }
            BBong.num++;
            GameManager.Inst().money -= BBong.cost;
        }
    }

    public void buyInvisibleSomething()
    {
        if (InvisibleSomething.num >= 2) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < InvisibleSomething.cost[InvisibleSomething.num]) Debug.Log("You don't have enough money");
        else
        {
            InvisibleSomething invisible = new InvisibleSomething();
            for (int i = 0; i < 3; i++)
            {
                if (GameManager.Inst().itemList[i] == null)
                {
                    GameManager.Inst().itemList[i] = invisible;
                }
            }
            InvisibleSomething.num++;
            GameManager.Inst().money -= InvisibleSomething.cost[InvisibleSomething.num];
        }
    }

    public void buyHappinessCircuit()
    {
        if (HappinessCircuit.num >= 1) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < HappinessCircuit.cost) Debug.Log("You don't have enough money");
        else
        {
            HappinessCircuit happy = new HappinessCircuit();
            for (int i = 0; i < 3; i++)
            {
                if (GameManager.Inst().itemList[i] == null)
                {
                    GameManager.Inst().itemList[i] = happy;
                }
            }
            HappinessCircuit.num++;
            GameManager.Inst().money -= HappinessCircuit.cost;
        }
    }
}
