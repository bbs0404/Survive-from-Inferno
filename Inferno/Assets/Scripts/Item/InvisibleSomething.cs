using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleSomething : Item {

    public int[] cost = { 1000, 2000 };

    public InvisibleSomething()
    {
        type = itemList.INVISIBLESOMETHING;
        amount = 0;
    }

    public InvisibleSomething(int num)
    {
        type = itemList.INVISIBLESOMETHING;
        amount = num;
    }

    public override void use()
	{
        GameManager.Inst().all_items[itemList.INVISIBLESOMETHING].amount--;
        if (GameManager.Inst().all_items[itemList.INVISIBLESOMETHING].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_items[itemList.INVISIBLESOMETHING]); 
    }
}
