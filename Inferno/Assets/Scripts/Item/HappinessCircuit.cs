using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessCircuit : Item {

    public int[] cost = { 2000 };

    public HappinessCircuit()
    {
        type = itemList.HAPPINESSCIRCUIT;
        amount = 0;
    }

    public HappinessCircuit(int num)
    {
        type = itemList.HAPPINESSCIRCUIT;
        amount = num;
    }

    public override void use()
	{
        GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].amount--;
        if (GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT]);
    }
}
