using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterbottle : Item {

    public Waterbottle()
    {
        type = itemList.WATERBOTTLE;
        amount = 0;
    }
    public float water;
    public float health; 
	public override void use()
	{
        InGameSystemManager.Inst().water = Mathf.Max(InGameSystemManager.Inst().water+water,100);
        InGameSystemManager.Inst().health = Mathf.Max(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);

        ItemManager.Inst().GetComponent< Waterbottle >().amount--;
        if (ItemManager.Inst().GetComponent<Waterbottle>().amount == 0)
            GameManager.Inst().itemList.Remove(ItemManager.Inst().GetComponent<Waterbottle>());
	}
}
