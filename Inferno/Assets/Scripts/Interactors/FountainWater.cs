using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainWater : Interactor {

    private bool on = true;

    public override void interact()
    {
        if (on)
        {
            StartCoroutine(drinkWater());
            on = false;
        }
    }
    IEnumerator drinkWater()
    {
        float amount = (InGameSystemManager.Inst().maxWater - InGameSystemManager.Inst().water) / 120f;
        for (int i=0; i<120; ++i)
        {
            InGameSystemManager.Inst().water += amount;
            yield return new WaitForSeconds(1 / 60f);
        }
    }
}
