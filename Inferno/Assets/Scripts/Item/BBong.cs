using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBong : Item {

    public static int amount = 3000;
    public static int num = 0;
	public void use()
	{
        GameManager.Inst().speedLevel += 10;
        GameManager.Inst().hitResistLevel += 50;
        Invoke("off", 5);
        Debug.Log("수분고정은 나중에");
    } 
    public void off() {
        GameManager.Inst().speedLevel -= 10;
        GameManager.Inst().hitResistLevel -= 50;
    }
}
