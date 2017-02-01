using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public void Buy(itemList item)
    {
        if (isListFull() == 0 || isListContain(item) == 1) 
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
        if (GameManager.Inst().itemList.Count < 3) return 0;
        else return 1;
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
        int num = ItemManager.Inst().gameObject.GetComponent<Waterbottle>().amount;
        int[] cost = ItemManager.Inst().gameObject.GetComponent<Waterbottle>().cost;

        if (num >= 3) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else
        {
            if (GameManager.Inst().itemList.Contains(ItemManager.Inst().gameObject.GetComponent<Waterbottle>())) ;
            else
                GameManager.Inst().itemList.Add(ItemManager.Inst().gameObject.GetComponent<Waterbottle>());

            ItemManager.Inst().gameObject.GetComponent<Waterbottle>().amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyBattery() {
        int num = ItemManager.Inst().gameObject.GetComponent<Battery>().amount;
        int[] cost = ItemManager.Inst().gameObject.GetComponent<Battery>().cost;

        if (num >= 3) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(ItemManager.Inst().gameObject.GetComponent<Battery>())) ;
            else
                GameManager.Inst().itemList.Add(ItemManager.Inst().gameObject.GetComponent<Battery>());

            ItemManager.Inst().gameObject.GetComponent<Battery>().amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyBBong() {
        int num = ItemManager.Inst().gameObject.GetComponent<BBong>().amount;
        int[] cost = ItemManager.Inst().gameObject.GetComponent<BBong>().cost;

        if (num >= 1) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(ItemManager.Inst().gameObject.GetComponent<BBong>())) ;
            else
                GameManager.Inst().itemList.Add(ItemManager.Inst().gameObject.GetComponent<BBong>());

            ItemManager.Inst().gameObject.GetComponent<BBong>().amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyInvisibleSomething() {
        int num = ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>().amount;
        int[] cost = ItemManager.Inst().gameObject.GetComponent<BBong>().cost;

        if (num >= 2) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>())) ;
            else
                GameManager.Inst().itemList.Add(ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>());

            ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>().amount++;
            GameManager.Inst().money -= cost[num];
        }
    }

    public void buyHappinessCircuit() {
        int num = ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>().amount;
        int[] cost = ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>().cost;

        if (num >= 1) Debug.Log("You can't have this item more than now");
        else if (GameManager.Inst().money < cost[num]) Debug.Log("You don't have enough money");
        else {
            if (GameManager.Inst().itemList.Contains(ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>())) ;
            else
                GameManager.Inst().itemList.Add(ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>());

            ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>().amount++;
            GameManager.Inst().money -= cost[num];
        }
    }
}
