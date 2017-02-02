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

    public BBong(int num)
    {
        type = itemList.BBONG;
        amount = num;
    }

    public override void use()
	{
        GameManager.Inst().speedLevel += 10;
        GameManager.Inst().hitResistLevel += 50;
        // Invoke("off", 5);
        Debug.Log("수분고정은 나중에");

        GameManager.Inst().all_Items[itemList.BBONG].amount--;
        if (GameManager.Inst().all_Items[itemList.BBONG].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.BBONG]);
    } 
    public void off() {
        GameManager.Inst().speedLevel -= 10;
        GameManager.Inst().hitResistLevel -= 50;
    }
}
