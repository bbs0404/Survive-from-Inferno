﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour {
    public Text priceText;
    public Text nameText;
    public Text amountText;
    public Text Money;
    public Item item;
    public Image itemImage;
    public String itemName;
    public String itemLabel;
    public int[] cost;

    void Start()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text t in texts) {
            if (t.gameObject.name == "ItemName") nameText = t;
            else if (t.name == "ItemAmount") amountText = t;
            else if (t.name == "ItemPrice") priceText = t;
            else if (t.name == "Money") Money = t;
        }
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image i in images) {
            if (i.gameObject.name == "ItemImage") { itemImage = i; break; }
        }
        Money.text = GameManager.Inst().money + "원";
    }

    public void getItemInfo(GameObject itemSprite)
    {
        itemName = itemSprite.name;
        switch (itemName.ToLower()) {
            case "waterbottle":
                cost = ItemManager.Inst().gameObject.GetComponent<Waterbottle>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<Waterbottle>();
                itemLabel = "500ml 물병";
                break;
            case "battery":
                cost = ItemManager.Inst().gameObject.GetComponent<Battery>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<Battery>();
                itemLabel = "배터리";
                break;
            case "bbong":
                cost = ItemManager.Inst().gameObject.GetComponent<BBong>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<BBong>();
                itemLabel = "간다 간다 뿅간다!";
                break;
            case "invisiblesomething":
                cost = ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<InvisibleSomething>();
                itemLabel = "보이지않는 무언가";
                break;
            case "happinesscircuit":
                cost = ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>().cost;
                item = ItemManager.Inst().gameObject.GetComponent<HappinessCircuit>();
                itemLabel = "행복회로";
                break;
            default:  break;
        }
        itemImage.sprite = itemSprite.GetComponent<Image>().sprite;
        nameText.text = itemLabel;
        amountText.text = item.amount + " / " + cost.Length;
        if (item.amount < cost.Length)
            priceText.text = cost[item.amount] + "원";
        else
            priceText.text = "Sold Out";
    }

	public void buyOnClick()
    {
        if (item == null)
        {
            Debug.Log("Please Select Item");
            return;
        }
        GameObject.Find("Shop").GetComponent<Shop>().Buy((itemList)Enum.Parse(typeof(itemList),itemName));
        TextReload();
	}
	
	public void TextReload ()
    {
        Money.text = GameManager.Inst().money + "원";
        amountText.text = item.amount + " / " + cost.Length;
        if (item.amount < cost.Length)
            priceText.text = cost[item.amount] + "원";
        else
            priceText.text = "Sold Out";
    }

}
