using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item {

    public Battery()
    {
        type = itemList.BATTERY;
    }
    public static int[] cost = { 500, 1000, 2500 };
	public void use()
	{
        InGameSystemManager.Inst().battery = Mathf.Max(InGameSystemManager.Inst().battery, InGameSystemManager.Inst().batteryCapacity);
    }
}
