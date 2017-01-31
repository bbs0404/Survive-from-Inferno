using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {
    public Text priceText;
    public Item item;
    public int[] cost;

    void Start()
    {
        priceText = GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>();
        switch (gameObject.name.ToLower()) {
            case "waterbottle":
                cost = ItemManager.Inst().gameObject.GetComponent<Waterbottle>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<Waterbottle>();
                break;
            case "battery":
                cost = ItemManager.Inst().gameObject.GetComponent<Battery>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<Battery>();
                break;
            case "bbong":
                cost = ItemManager.Inst().gameObject.GetComponent<BBong>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<BBong>();
                break;
            case "invisiblesomething":
                cost = ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>();
                break;
            case "happinesscircuit":
                cost = ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>();
                break;
            default:  break;
        }
        
        priceText.text = cost[item.amount] + "";
    }
	public void OnClick()
    {
        Debug.Log(gameObject.name);
        priceText = GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        GameObject.Find("Shop").GetComponent<Shop>().Buy((itemList)Enum.Parse(typeof(itemList),gameObject.name));
        if (item.amount < cost.Length)
            priceTextReload();
        else
            soldOutMessage();
	}
	
	public void priceTextReload ()
    {
        priceText.text = cost[item.amount] + "";
    }

    public void soldOutMessage()
    {
        priceText.text = "Sold Out";
    }
}
