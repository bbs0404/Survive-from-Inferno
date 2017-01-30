using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBong : Item {

    public int[] cost = { 3000 };

    public BBong()
    {
        type = itemList.BBONG;
        amount = 0;
    }

	public override void use()
	{
        GameManager.Inst().speedLevel += 10;
        GameManager.Inst().hitResistLevel += 50;
         Invoke("off", 5);
        Debug.Log("수분고정은 나중에");

        ItemManager.Inst().GetComponent<BBong>().amount--;
        if (ItemManager.Inst().GetComponent<BBong>().amount == 0)
            GameManager.Inst().itemList.Remove(ItemManager.Inst().GetComponent<BBong>());
    } 
    public void off() {
        GameManager.Inst().speedLevel -= 10;
        GameManager.Inst().hitResistLevel -= 50;
    }
}
