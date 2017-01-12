using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item {

    public static int[] amount = { 500, 1000, 2500 };
    public static int num = 0;
	public void use()
	{
        InGameSystemManager.Inst().battery = Mathf.Max(InGameSystemManager.Inst().battery, InGameSystemManager.Inst().batteryCapacity);
    }
}
