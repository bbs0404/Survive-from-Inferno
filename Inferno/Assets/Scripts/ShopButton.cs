using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {
    public Text priceText;
    public Item item;
    public int[] cost;
	// Use this for initialization
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
            default: item = ItemManager.Inst().gameObject.GetComponent<BBong>(); break;
        }
        
        priceText.text = cost[item.amount] + "";
    }
	public void test()
    {
        Debug.Log(gameObject.name);
        priceText = GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        GameObject.Find("Shop").GetComponent<Shop>().Buy((itemList)Enum.Parse(typeof(itemList),gameObject.name));
        if (item.amount < cost.Length)
            priceTextReload();
        else
            soldOutMessage();
        //"ItemManager.Inst().GetComponent<Waterbottle>().amount";
	}
	
	// Update is called once per frame
	public void priceTextReload ()
    {
        priceText.text = cost[item.amount] + "";
    }

    public void soldOutMessage()
    {
        priceText.text = "Sold Out";
    }
}
