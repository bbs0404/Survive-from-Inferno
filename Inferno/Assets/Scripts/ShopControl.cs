using System.Collections;
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
                cost = GameManager.Inst().all_Items[itemList.WATERBOTTLE].cost;
                item = GameManager.Inst().all_Items[itemList.WATERBOTTLE];
                itemLabel = "500ml 물병";
                break;
            case "battery":
                cost = GameManager.Inst().all_Items[itemList.BATTERY].cost;
                item = GameManager.Inst().all_Items[itemList.BATTERY];
                itemLabel = "배터리";
                break;
            case "bbong":
                cost = GameManager.Inst().all_Items[itemList.BBONG].cost;
                item = GameManager.Inst().all_Items[itemList.BBONG];
                itemLabel = "간다 간다 뿅간다!";
                break;
            case "invisiblesomething":
                cost = GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING].cost;
                item = GameManager.Inst().all_Items[itemList.INVISIBLESOMETHING];
                itemLabel = "보이지않는 무언가";
                break;
            case "happinesscircuit":
                cost = GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].cost;
                item = GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT];
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
