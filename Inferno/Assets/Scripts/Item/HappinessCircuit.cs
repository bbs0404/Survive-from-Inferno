﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessCircuit : Item {

    public HappinessCircuit()
    {
        type = itemList.HAPPINESSCIRCUIT;
        amount = 0;
        int[] temp = { 2000 };
        cost = temp;
        label = "행복회로";
    }

    public HappinessCircuit(int num)
    {
        type = itemList.HAPPINESSCIRCUIT;
        amount = num;
        int[] temp = { 2000 };
        cost = temp;
        label = "행복회로";
    }

    public override void use()
	{
        GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].amount--;
        if (GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.HAPPINESSCIRCUIT]);
    }
}
