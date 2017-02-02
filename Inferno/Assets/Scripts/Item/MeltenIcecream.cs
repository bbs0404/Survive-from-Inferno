using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltenIcecream : Item {

    public MeltenIcecream()
    {
        type = itemList.MELTENICECREAM;
        amount = 0;
    }

    public MeltenIcecream(int num)
    {
        type = itemList.MELTENICECREAM;
        amount = num;
    }

    public override void use()
	{
        Debug.Log("Icecream Melten");
	}
    
}
