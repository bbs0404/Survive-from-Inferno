using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainWater : Interactor {

    private bool on = true;
    [SerializeField]
    private Animator animator;

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
        float amount = (InGameSystemManager.Inst().maxWater - InGameSystemManager.Inst().water) / 60f;
        animator.SetBool("Work", true);
        for (int i=0; i<60; ++i)
        {
            InGameSystemManager.Inst().water += amount;
            yield return new WaitForSeconds(1 / 60f);
        }
        GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
        animator.SetBool("Work", false);
    }
}
