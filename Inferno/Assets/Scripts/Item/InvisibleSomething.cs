using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleSomething : Item {

    public InvisibleSomething()
    {
        type = itemList.INVISIBLESOMETHING;
        amount = 0;
    }

    public override void use()
	{
        ItemManager.Inst().GetComponent<InvisibleSomething>().amount--;
        if (ItemManager.Inst().GetComponent<InvisibleSomething>().amount == 0)
            GameManager.Inst().itemList.Remove(ItemManager.Inst().GetComponent<InvisibleSomething>());
    }
}
