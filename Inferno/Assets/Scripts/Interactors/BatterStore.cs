using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterStore : Interactor {
    [SerializeField]
    private int cost = 1000;
    private bool on = true;

    public override void interact()
    {
        if (GameManager.Inst().money < cost || !on || !GameManager.Inst().fan)
            return;
        if (GameManager.Inst().hasItem(itemList.BATTERY))
        {
            GameManager.Inst().all_Items[itemList.BATTERY].amount++;
            GameManager.Inst().money -= cost;
            GameManager.Inst().spentMoney += cost;
        }
        else if (GameManager.Inst().itemList.Count < 3)
        {
            GameManager.Inst().all_Items[itemList.BATTERY].amount++;
            GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[itemList.BATTERY]);
            GameManager.Inst().money -= cost;
            GameManager.Inst().spentMoney += cost;
        }
        on = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
        GetComponent<AudioSource>().Play();
        UserInterfaceManager.Inst().updateInGameCanvas();
    }
}
