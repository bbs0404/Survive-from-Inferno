using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : Interactor {

    private bool isOpened = false;
    private float openTimer = 5.0f;
    private float closeTimer = 5.0f;
    private Field thisField;

    public override void interact()
    {
        if (!isOpened && closeTimer < 0)
        {
            (thisField = this.gameObject.AddComponent<Field>()).type = field.SHADOW;
            isOpened = true;
        }
    }

    private void Update()
    {
        if (isOpened)
        {
            openTimer -= Time.deltaTime;
            if (openTimer < 0)
            {
                isOpened = false;
                Destroy(thisField);
            }
        }
        else
        {
            if (closeTimer >= 0)
            {
                closeTimer -= Time.deltaTime;
            }
        }
    }
}
