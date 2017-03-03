using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWalk : Field {

    [SerializeField]
    private float redTime;
    [SerializeField]
    private float greenTime;
    [SerializeField]
    private float timer;
    private bool green;
    [SerializeField]
    private SignalLight[] signals;
    private AudioSource crashSFX;

    private void Awake()
    {
        if (Random.value > 0.5f)
        {
            green = true;
            timer = greenTime;
        }
        else
        {
            green = false;
            timer = redTime;
        }
        foreach (var item in signals)
        {
            item.updateSignal(green);
        }
        type = field.CROSSWALK;
        crashSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!InGameSystemManager.Inst().isPaused)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                green = !green;
                if (green)
                    timer = greenTime;
                else
                {
                    timer = redTime;
                    if (isPlayerIn)
                        cross();
                }
                foreach (var item in signals)
                {
                    item.updateSignal(green);
                }
            }
            if (green && timer < greenTime / 2 && timer % 1 > 0.5f)
            {
                foreach (var item in signals)
                {
                    if (item.GetComponent<SpriteRenderer>().color == new Color(0, 1, 0))
                        item.blink();
                }
            }
            else if (green && timer < greenTime / 2)
            {
                foreach (var item in signals)
                {
                    if (item.GetComponent<SpriteRenderer>().color == new Color(0, 0.3f, 0))
                        item.blink();
                }
            }
        }
    }

    private void cross()
    {
        if (!green)
        {
            if (Random.Range(0, 100) >= 95)
            {
                InGameSystemManager.Inst().playerDeadByCar();
                crashSFX.Play();
            }
            else
            {
                GameManager.Inst().money -= 300;
                if (GameManager.Inst().money < 0)
                    GameManager.Inst().money = 0;
                Debug.Log(GameManager.Inst().money.ToString());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player)
        {
            InGameSystemManager.Inst().fields.Add(this);
            isPlayerIn = true;
            cross();
        }
    }
}
