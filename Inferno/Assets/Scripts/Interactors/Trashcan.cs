using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Interactor {

    public override void interact()
    {
        if (GameManager.Inst().itemList.Contains(ItemManager.Inst().gameObject.GetComponent<MeltenIcecream>()))
        {
            GameManager.Inst().itemList.Remove(ItemManager.Inst().gameObject.GetComponent<MeltenIcecream>());
            ItemManager.Inst().gameObject.GetComponent<MeltenIcecream>().amount = 0;
        }
    }
}
