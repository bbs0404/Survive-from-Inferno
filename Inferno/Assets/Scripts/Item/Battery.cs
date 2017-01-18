using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item {

    public Battery()
    {
        type = itemList.BATTERY;
        amount = 0;
    }

	public override void use()
	{
        InGameSystemManager.Inst().battery = Mathf.Max(InGameSystemManager.Inst().battery, InGameSystemManager.Inst().batteryCapacity);

        ItemManager.Inst().GetComponent<Battery>().amount--;
        if (ItemManager.Inst().GetComponent<Battery>().amount == 0)
            GameManager.Inst().itemList.Remove(ItemManager.Inst().GetComponent<Battery>());
    }
}
