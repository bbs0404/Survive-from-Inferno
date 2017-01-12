using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : Item {
    public static int cost = 300;
    public float health;
    public float water;
	public void use()
    {
        InGameSystemManager.Inst().water = Mathf.Max(InGameSystemManager.Inst().water + water, 100f);
        InGameSystemManager.Inst().health = Mathf.Max(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);
    }
}
