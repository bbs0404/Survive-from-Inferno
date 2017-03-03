using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : Item {

    public float health = 30;
    public float water = 30;

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
        InGameSystemManager.Inst().water = Mathf.Min(InGameSystemManager.Inst().water + water, 100f);
        InGameSystemManager.Inst().health = Mathf.Min(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);

        GameManager.Inst().all_Items[itemList.ICECREAM].amount--;
        if (GameManager.Inst().all_Items[itemList.ICECREAM].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.ICECREAM]);

    }
}
