using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasol : Interactor {

    private int count;
    [SerializeField]
    private Animator animator;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        count = 1;
    }

    public override void interact()
    {
        if (count > 0 && --count == 0)
        {
            this.gameObject.AddComponent<Field>().type = field.SHADOW;
            animator.SetBool("interact", true);
        }
    }
}
