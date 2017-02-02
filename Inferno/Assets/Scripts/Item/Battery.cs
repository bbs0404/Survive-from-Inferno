using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item {

    public int[] cost = { 500, 1000, 2500 };

    public Battery()
    {
        type = itemList.BATTERY;
        amount = 0;
    }

    public Battery(int num)
    {
        type = itemList.BATTERY;
        amount = num;
    }

	public override void use()
	{
        InGameSystemManager.Inst().battery = Mathf.Max(InGameSystemManager.Inst().battery, InGameSystemManager.Inst().batteryCapacity);

        GameManager.Inst().all_Items[itemList.BATTERY].amount--;
        if (GameManager.Inst().all_Items[itemList.BATTERY].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.BATTERY]);
    }
}
