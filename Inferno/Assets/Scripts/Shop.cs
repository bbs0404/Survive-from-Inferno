using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public void Buy(itemList item)
    {
        if (isListFull() == 0 ||isListContain(item) == 1) 
        {
            switch (item)
            {
                case itemList.WATERBOTTLE:  buyWaterBottle(); break;
                case itemList.BATTERY: buyBattery(); break;
                case itemList.BBONG: buyBBong(); break;
                case itemList.HAPPINESSCIRCUIT: buyHappinessCircuit(); break;
                case itemList.INVISIBLESOMETHING: buyInvisibleSomething(); break;
                default: break;
            }
        }
        else Debug.Log("Your item list is full");
    }

    public int isListFull() 
    {
        //if (GameManager.Inst().itemList.Count < 3) return 0;
        //else return 1;
        return 0;
    }
    public int isListContain(itemList item)
    {
        foreach(Item i in GameManager.Inst().itemList)
        {
            if (i.type == item) return 1;
        }
        return 0;
    }

    public void buyWaterBottle()
    {
        int num = GameManager.Inst().all_Items[itemList.WATERBOTTLE].amount;
        int[] cost = GameManager.Inst().all_Items[itemList.WATERBOTTLE].cost;

        if (num >= 3) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else
        {
            if (GameManager.Inst().itemList.Contains(GameManager.Inst().all_Items[itemList.WATERBOTTLE]));
            else
                GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[itemList.WATERBOTTLE]);

            GameManager.Inst().all_Items[itemList.WATERBOTTLE].amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyBattery() {
        int num = GameManager.Inst().all_Items[itemList.BATTERY].amount;
        int[] cost = GameManager.Inst().all_Items[itemList.BATTERY].cost;

        if (num >= 3) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(GameManager.Inst().all_Items[itemList.BATTERY]));
            else
                GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[itemList.BATTERY]);

            GameManager.Inst().all_Items[itemList.BATTERY].amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyBBong() {
        int num = GameManager.Inst().all_Items[itemList.BBONG].amount;
        int[] cost = GameManager.Inst().all_Items[itemList.BBONG].cost;

        if (num >= 1) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(GameManager.Inst().all_Items[itemList.BBONG]));
            else
                GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[itemList.BBONG]);

            GameManager.Inst().all_Items[itemList.BBONG].amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyInvisibleSomething() {
        int num = GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING].amount;
        int[] cost = GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING].cost;

        if (num >= 2) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING]));
            else
                GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING]);

            GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING].amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyHappinessCircuit() {
        int num = GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].amount;
        int[] cost = GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].cost;

        if (num >= 1) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT])) ;
            else
                GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT]);

            GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].amount++;
            GameManager.Inst().money -= cost[num];
        }
    }
}
