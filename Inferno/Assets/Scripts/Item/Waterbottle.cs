using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterbottle : Item {

    public float water = 50;
    public float health = 10;
     
    public Waterbottle()
    {
        type = itemList.WATERBOTTLE;
        amount = 0;
        int[] temp = { 300, 500, 1000 };
        cost = temp;
        label = "500ml 물병";
    }

    public Waterbottle(int num)
    {
        type = itemList.WATERBOTTLE;
        amount = num;
        int[] temp = { 300, 500, 1000 };
        cost = temp;
        label = "500ml 물병";
    }

	public override void use()
	{
        InGameSystemManager.Inst().water = Mathf.Min(InGameSystemManager.Inst().water+water,100);
        InGameSystemManager.Inst().health = Mathf.Min(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);

        GameManager.Inst().all_Items[itemList.WATERBOTTLE].amount--;
        if (GameManager.Inst().all_Items[itemList.WATERBOTTLE].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.WATERBOTTLE]);
	}
}
