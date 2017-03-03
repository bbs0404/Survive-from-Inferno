using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doYouKnow : Interactor {

    private int life;
    private Color thisColor;
    private float timer;
    private bool istriggered;
    private bool faded;
    private Animator animator;
    private GameObject Do;

    private void Awake()
    {
        timer = 3f;
        life = 1;
        istriggered = false;
        faded = false;
        thisColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        animator = GetComponent<Animator>();
        Do = transform.FindChild("Do").gameObject;
        Do.SetActive(false);
    }

    private void Update()
    {
        if (!InGameSystemManager.Inst().isPaused && !InGameSystemManager.Inst().isInvisible)
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
            else if (Mathf.Abs(PlayerManager.Inst().player.transform.position.x - this.gameObject.transform.position.x) < 30 && !faded && !InGameSystemManager.Inst().isGameOver)
            {
                if (PlayerManager.Inst().player.transform.position.x - this.gameObject.transform.position.x < 0)
                {
                    this.gameObject.transform.position -= new Vector3(0.1f, 0);
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (PlayerManager.Inst().player.transform.position.x - this.gameObject.transform.position.x > 0)
                {
                    this.gameObject.transform.position += new Vector3(0.1f, 0);
                    animator.SetBool("Left", false);
                    animator.SetBool("Right", true);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            else
            {
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
            }
        }
    }

    public override void interact()
    {
        if (!istriggered && life > 0 && --life == 0)
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
            Do.GetComponent<SpriteRenderer>().color = new Color(thisColor.r, thisColor.g, thisColor.b, i);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InGameSystemManager.Inst().isInvisible)
            return;
        if (!faded && collision.gameObject == PlayerManager.Inst().player)
        {
            FindObjectOfType<PlayerController>().enabled = false;
            Do.SetActive(true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            PlayerManager.Inst().player.GetComponent<Animator>().SetBool("RUN_left", false);
            PlayerManager.Inst().player.GetComponent<Animator>().SetBool("RUN_right", false);
            istriggered = true;
            //3초간 움직일 수 없음
        }
    }
}
