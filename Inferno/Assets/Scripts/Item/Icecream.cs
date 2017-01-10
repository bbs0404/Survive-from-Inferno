﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecream : Item {
    public int water;
    public int health;
	public void use()
    {
        InGameSystemManager.Inst().water = Mathf.Max(InGameSystemManger.Inst().water + water, 100f);
        InGameSystemManager.Inst().health = Mathf.Max(InGameSystemManager.Inst().health + health, InGameSystemManager.Inst().maxHealth);
    }
}
