using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : Item {

    public float health;
    public float water;

    public Icecream()
    {
        type = itemList.ICECREAM;
        amount = 0;
    }

    public Icecream(int num)
    {
        type = itemList.ICECREAM;
        amount = num;
    }

    private void Awake()
    {
        type = itemList.ICECREAM;
        amount = 0;
    }

    public override void use()
    {
        InGameSystemManager.Inst().water = Mathf.Max(InGameSystemManager.Inst().water + water, 100f);
        InGameSystemManager.Inst().health = Mathf.Max(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);

        GameManager.Inst().all_items[itemList.ICECREAM].amount--;
        if (GameManager.Inst().all_items[itemList.ICECREAM].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_items[itemList.ICECREAM]);

    }
}
