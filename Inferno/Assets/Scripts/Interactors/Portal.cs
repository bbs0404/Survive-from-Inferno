using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactor {

    [SerializeField]
    private GameObject exit;

    public override void interact()
    {
        PlayerManager.Inst().player.transform.position = exit.transform.position;
    }
}
