using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Interactor {

    public override void interact()
    {
        if (GameManager.Inst().hasItem(itemList.MELTENICECREAM))
        {
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.MELTENICECREAM]);
            GameManager.Inst().all_Items[itemList.MELTENICECREAM].amount = 0;
        }
    }
}
