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
    public Text discriptionText;
    public Item item;
    public Image itemImage;
    public String itemName;
    public String itemLabel;
    public Dictionary<itemList,string> itemDescription = new Dictionary<itemList, string>();
    private itemList itemType;
    public int[] cost;

    void Start()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text t in texts) {
            if (t.gameObject.name == "ItemName") nameText = t;
            else if (t.name == "ItemAmount") amountText = t;
            else if (t.name == "ItemPrice") priceText = t;
        }
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image i in images) {
            if (i.gameObject.name == "ItemImage") { itemImage = i; break; }
        }
        Money.text = GameManager.Inst().money.ToString();
        itemDescription.Add(itemList.BATTERY, "미니 선풍기의 배터리 양을 100 충전해 줍니다.");
        itemDescription.Add(itemList.BBONG, "10초간 체력감소 -50%, 1.4배의 속도로 움직입니다. 하지만 부작용으로 그 후에는 5초간 수분이 0으로 고정됩니다.");
        itemDescription.Add(itemList.HAPPINESSCIRCUIT, "10초간 수분에 의한 패널티(ex 이동속도 감소, 체력 감소량 증가)를 받지 않습니다.");
        itemDescription.Add(itemList.INVISIBLESOMETHING, "20초간 전도사를 피해다닐수 있습니다. 다만, 부작용으로 횡단보도에서 차사고의 확률이 2배 올라갑니다.");
        itemDescription.Add(itemList.WATERBOTTLE, "체력 +10, 수분 +50");
    }

    public void getItemInfo(GameObject itemSprite)
    {
        itemName = itemSprite.name;

        itemType = (itemList)Enum.Parse(typeof(itemList), itemName);
        item = GameManager.Inst().all_Items[itemType];
        cost = item.cost;
        itemLabel = item.label;

        itemImage.sprite = itemSprite.GetComponent<Image>().sprite;
        nameText.text = itemLabel;
        amountText.text = item.amount + " / " + cost.Length;
        if (item.amount < cost.Length)
            priceText.text = cost[item.amount].ToString();
        else
            priceText.text = "Sold Out";
        discriptionText.text = itemDescription[itemType];
    }

	public void buyOnClick()
    {
        if (item == null)
        {
            Debug.Log("Please Select Item");
            return;
        }
        GameObject.Find("Shop").GetComponent<Shop>().Buy(itemType);
        TextReload();
        UserInterfaceManager.Inst().updateShopCanvas();
    }
	
	public void TextReload ()
    {
        Money.text = GameManager.Inst().money.ToString();
        amountText.text = item.amount + " / " + cost.Length;
        if (item.amount < cost.Length)
            priceText.text = cost[item.amount].ToString();
        else
            priceText.text = "Sold Out";
    }

}
