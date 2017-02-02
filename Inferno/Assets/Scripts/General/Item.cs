using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemList
{
    WATERBOTTLE,
    BATTERY,
    ICECREAM,
    MELTENICECREAM,
    BBONG,
    HAPPINESSCIRCUIT,
    INVISIBLESOMETHING,
    FAN,
    NONE
}

[System.Serializable]
public class Item {

    public int amount;
    public itemList type;
    public int[] cost;

    public Item()
    {
        type = itemList.NONE;
        amount = 0;
    }

	public virtual void use()
    {

    }
    
}
