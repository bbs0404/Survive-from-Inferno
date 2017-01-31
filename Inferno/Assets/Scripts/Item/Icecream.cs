﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : Item {

    public float health;
    public float water;
    private void Awake()
    {
        type = itemList.ICECREAM;
        amount = 0;
    }

    public override void use()
    {
        InGameSystemManager.Inst().water = Mathf.Max(InGameSystemManager.Inst().water + water, 100f);
        InGameSystemManager.Inst().health = Mathf.Max(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);

        ItemManager.Inst().GetComponent<Icecream>().amount--;
        if (ItemManager.Inst().GetComponent<Icecream>().amount == 0)
            GameManager.Inst().itemList.Remove(ItemManager.Inst().GetComponent<Icecream>());

    }
}
