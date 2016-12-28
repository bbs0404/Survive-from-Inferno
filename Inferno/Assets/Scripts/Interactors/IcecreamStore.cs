using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecreamStore : Interactor {
    [SerializeField]
    private int cost;
    public void interact()
    {
        GameManager.Inst().subMoney(cost);

    }
}
