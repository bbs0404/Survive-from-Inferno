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
    NONE
}

public class Item : MonoBehaviour {

    public int amount;
    public itemList type;

    public Item()
    {
        type = itemList.NONE;
        amount = 0;
    }

	public virtual void use()
    {

    }
    
}
