using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : Interactor {

    private bool isOpened;
    [SerializeField]
    private float openTimer = 5.0f;
    [SerializeField]
    private float closeTimer = 0;
    private Field thisField;
    private Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        isOpened = false;
        closeTimer = 0;
    }

    public override void interact()
    {
        Debug.Log(this.gameObject.name);
        if (!isOpened && closeTimer < 0)
        {
            (thisField = this.gameObject.AddComponent<Field>()).type = field.SHADOW;
            isOpened = true;
            animator.SetBool("Opening", true);
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
                if (InGameSystemManager.Inst().fields.Contains(thisField))
                    InGameSystemManager.Inst().fields.Remove(thisField);
                thisField.DestroyField();
                animator.SetBool("Opening", false);
                closeTimer = 5;
            }
        }
        else
        {
            if (closeTimer >= 0)
            {
                closeTimer -= Time.deltaTime;
                if (closeTimer < 0)
                    openTimer = 5f;
            }
        }
    }
}
