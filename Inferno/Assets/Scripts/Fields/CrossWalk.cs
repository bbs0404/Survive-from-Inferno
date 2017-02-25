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
    }

    private void Update()
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
    }

    private void cross()
    {
        if (!green)
        {
            if (Random.Range(0, 100) >= 95)
            {
                InGameSystemManager.Inst().playerDead();
            }
            else
            {
                GameManager.Inst().money -= 300;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player)
        {
            cross();
        }
    }
}
