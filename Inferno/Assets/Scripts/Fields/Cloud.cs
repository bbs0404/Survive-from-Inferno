using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Field {

    [SerializeField]
    private float existTime;
    [SerializeField]
    private Color thisColor;
    private bool isExist = false;
    private float speed;
    private bool isFadeOut = false;

    private void Awake()
    {
        existTime = (Random.Range(10, 20) + Random.Range(5, 10)) / 2;
        speed = Random.Range(-1f, 1);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(thisColor.r, thisColor.g, thisColor.b, 0);
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        
        StartCoroutine(FadeIn());
    }
    private void Update()
    {
        if (isExist)
        {
            existTime -= Time.deltaTime;
            if (existTime < 0 && !isFadeOut)
            {
                StartCoroutine(FadeOut());
                isFadeOut = true;
            }
        }
        this.transform.position += new Vector3(speed * 0.05f,0);
    }

    private void OnDestroy()
    {
        //if (InGameSystemManager.Inst().fields.Contains(this))
        //    InGameSystemManager.Inst().fields.Remove(this);
    }

    IEnumerator FadeIn()
    {
        for (float i=0; i<=0.5; i += 0.05f)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(thisColor.r, thisColor.g, thisColor.b, i);
            yield return null;
        }
        isExist = true;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    IEnumerator FadeOut()
    {
        for (float i=0.5f; i>=0; i -= 0.05f)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(thisColor.r, thisColor.g, thisColor.b, i);
            yield return null;
        }
        isExist = false;
        Destroy(this.gameObject);
    }
}
