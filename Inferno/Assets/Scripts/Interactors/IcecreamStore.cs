using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecreamStore : Interactor {
    [SerializeField]
    private int cost = 300;
    public override void interact()
    {
        //Debug.Log(GameManager.Inst().itemList.Count);
        if(GameManager.Inst().itemList.Contains(ItemManager.Inst().gameObject.GetComponent<Icecream>()))
        {
            ++ItemManager.Inst().gameObject.GetComponent<Icecream>().amount;
            GameManager.Inst().money -= cost;
        }
        else if(GameManager.Inst().itemList.Count < 3)
        {
            ++ItemManager.Inst().gameObject.GetComponent<Icecream>().amount;
            GameManager.Inst().itemList.Add(ItemManager.Inst().gameObject.GetComponent<Icecream>());
            GameManager.Inst().money -= cost;
        }
        UserInterfaceManager.Inst().updateInGameCanvas();
    }
}
