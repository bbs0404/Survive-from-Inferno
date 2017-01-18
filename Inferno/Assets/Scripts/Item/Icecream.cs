using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : Item {

    public Icecream()
    {
        type = itemList.ICECREAM;
        amount = 0;
    }
    public static int cost = 300;
    public float health;
    public float water;
	public override void use()
    {
        InGameSystemManager.Inst().water = Mathf.Max(InGameSystemManager.Inst().water + water, 100f);
        InGameSystemManager.Inst().health = Mathf.Max(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);
        --amount;
    }
}
