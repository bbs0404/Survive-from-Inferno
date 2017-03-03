using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecreamStore : Interactor {
    [SerializeField]
    private int cost = 300;
    private bool on = true;

    public override void interact()
    {
        if (GameManager.Inst().money < cost || !on)
            return;
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
        on = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
        UserInterfaceManager.Inst().updateInGameCanvas();
    }
}
