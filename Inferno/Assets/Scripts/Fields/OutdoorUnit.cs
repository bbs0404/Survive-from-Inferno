using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorUnit : Field {

    private float timer;
    private void Awake()
    {
        timer = Random.Range(5f, 10f); ;
    }

    void Update () {
        timer -= Gametime.deltaTime;
        if (timer < 0)
        {
            bool foo;
            if (foo = this.gameObject.GetComponent<Animator>().GetBool("Start"))
            {
                timer = 5.0f;
                if (InGameSystemManager.Inst().fields.Contains(this))
                    InGameSystemManager.Inst().fields.Remove(this);
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            }
            else
            {
                timer = 8.0f;
                this.gameObject.AddComponent<BoxCollider2D>();
            }
            this.gameObject.GetComponent<Animator>().SetBool("Start", !foo);
        }
	}
}
