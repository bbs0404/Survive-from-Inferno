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
                cost = GameManager.Inst().all_Items[itemList.WATERBOTTLE].cost;
                item = GameManager.Inst().all_Items[itemList.WATERBOTTLE];
                break;
            case "battery":
                cost = GameManager.Inst().all_Items[itemList.BATTERY].cost;
                item = GameManager.Inst().all_Items[itemList.BATTERY];
                break;
            case "bbong":
                cost = GameManager.Inst().all_Items[itemList.BBONG].cost;
                item = GameManager.Inst().all_Items[itemList.BBONG];
                break;
            case "invisiblesomething":
                cost = GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING].cost;
                item = GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING];
                break;
            case "happinesscircuit":
                cost = GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].cost;
                item = GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT];
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
