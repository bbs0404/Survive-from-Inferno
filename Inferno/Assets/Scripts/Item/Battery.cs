using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item {

    public Battery()
    {
        type = itemList.BATTERY;
        amount = 0;
        int[] temp = { 500, 1000, 2500 };
        cost = temp;
        label = "배터리";
    }

    public Battery(int num)
    {
        type = itemList.BATTERY;
        amount = num;
        int[] temp = { 500, 1000, 2500 };
        cost = temp;
        label = "배터리";
    }

	public override void use()
	{
        if (!InGameSystemManager.Inst().isFan)
            return;
        InGameSystemManager.Inst().battery = Mathf.Min(InGameSystemManager.Inst().battery + 100, InGameSystemManager.Inst().batteryCapacity);

        GameManager.Inst().all_Items[itemList.BATTERY].amount--;
        if (GameManager.Inst().all_Items[itemList.BATTERY].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.BATTERY]);
    }
}