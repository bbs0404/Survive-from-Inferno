using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoYouKnow : Interactor {

    private int life;
    private Color thisColor;

    private void Awake()
    {
        life = 3;
        thisColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    public override void interact()
    {
        if (life > 0 && --life == 0)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = 0.5f; i >= 0; i -= 0.05f)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(thisColor.r, thisColor.g, thisColor.b, i);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player)
        {
            //3초간 움직일 수 없음
        }
    }
}
