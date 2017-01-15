using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasol : Interactor {

    private int count;

    private void Awake()
    {
        count = 3;
    }

    public override void interact()
    {
        if (count > 0 && --count == 0)
        {
            this.gameObject.AddComponent<Field>().type = field.SHADOW;
        }
    }
}
