using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterbottle : Item {

    public static int[] cost = {300, 500, 1000};
    public static int num = 0;
    public float water;
    public float health; 
	public void use()
	{
        InGameSystemManager.Inst().water = Mathf.Max(InGameSystemManager.Inst().water+water,100);
        InGameSystemManager.Inst().health = Mathf.Max(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);
	}
}
