using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecreamStore : Interactor {
    [SerializeField]
    private int cost = 300;
    public override void interact()
    {
        for (int i=0; i<3; ++i)
        {
            if (GameManager.Inst().itemList[i].type == itemList.NONE)
            {
                GameManager.Inst().itemList[i] = new Icecream();
                GameManager.Inst().all_Items.Add(GameManager.Inst().itemList[i]);
                ++GameManager.Inst().itemList[i].amount;
                GameManager.Inst().money -= cost;
                break;
            }
            if (GameManager.Inst().itemList[i].type == itemList.ICECREAM)
            {
                ++GameManager.Inst().itemList[i].amount;
                GameManager.Inst().money -= cost;
                break;
            }
        }
    }
}
