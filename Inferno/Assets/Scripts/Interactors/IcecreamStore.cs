using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecreamStore : Interactor {
    [SerializeField]
    private int cost = 300;
    public override void interact()
    {
        if(GameManager.Inst().hasItem(itemList.ICECREAM))
        {
            GameManager.Inst().all_Items[itemList.ICECREAM].amount++;
            GameManager.Inst().money -= cost;
        }
        else if(GameManager.Inst().itemList.Count < 3)
        {
            GameManager.Inst().all_Items[itemList.ICECREAM].amount++;
            GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[itemList.ICECREAM]);
            GameManager.Inst().money -= cost;
        }
        UserInterfaceManager.Inst().updateInGameCanvas();
    }
}
