using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessCircuit : Item {

    public HappinessCircuit()
    {
        type = itemList.HAPPINESSCIRCUIT;
        amount = 0;
    }

    public override void use()
	{
        ItemManager.Inst().GetComponent<HappinessCircuit>().amount--;
        if (ItemManager.Inst().GetComponent<HappinessCircuit>().amount == 0)
            GameManager.Inst().itemList.Remove(ItemManager.Inst().GetComponent<HappinessCircuit>());
    }
}
