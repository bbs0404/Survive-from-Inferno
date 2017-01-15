using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltenIcecream : Item {

    public MeltenIcecream()
    {
        GameManager.Inst().speedLevel -= 10;
    }
	public void use()
	{
        Debug.Log("Icecream Melten");
	}
    
}
