using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doYouKnow : Interactor {

    private int life;
    private Color thisColor;
    private float timer;
    private bool istriggered;
    private bool faded;

    private void Awake()
    {
        timer = 3f;
        life = 3;
        istriggered = false;
        faded = false;
        thisColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (istriggered)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                istriggered = false;
                FindObjectOfType<PlayerController>().enabled = true;
                StartCoroutine(FadeOut());
            }
        }
        else if (Mathf.Abs(PlayerManager.Inst().player.transform.position.x - this.gameObject.transform.position.x) < 30 && !faded)
        {
            if (PlayerManager.Inst().player.transform.position.x - this.gameObject.transform.position.x < 0)
                this.gameObject.transform.position -= new Vector3(0.1f, 0);
            else if (PlayerManager.Inst().player.transform.position.x - this.gameObject.transform.position.x > 0)
                this.gameObject.transform.position += new Vector3(0.1f, 0);
        }

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
        faded = true;
        for (float i = 1f; i >= 0; i -= 0.01f)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(thisColor.r, thisColor.g, thisColor.b, i);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!faded && collision.gameObject == PlayerManager.Inst().player)
        {
            FindObjectOfType<PlayerController>().enabled = false;
            istriggered = true;
            //3초간 움직일 수 없음
        }
    }
}
