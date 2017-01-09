using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Field {

    private float existTime;
    private Color thisColor;
    private bool isExist = false;
    private float speed;

    private void Awake()
    {
        existTime = (Random.Range(0, 5) + Random.Range(0, 10)) / 2;
        speed = Random.Range(0.5f, 1);
        thisColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        thisColor = new Color(thisColor.r, thisColor.g, thisColor.b, 0);
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        
        StartCoroutine(FadeIn());
    }
    private void Update()
    {
        if (isExist)
            existTime -= Time.deltaTime;
        this.transform.position += new Vector3(speed * 0.1f,0);
    }

    private void OnDestroy()
    {
        if (InGameSystemManager.Inst().fields.Contains(this))
            InGameSystemManager.Inst().fields.Remove(this);
    }

    IEnumerator FadeIn()
    {
        for (float i=0; i<=0.5; i += 0.01f)
        {
            thisColor = new Color(thisColor.r, thisColor.g, thisColor.b, i);
            yield return null;
        }
        isExist = true;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    IEnumerator FadeOut()
    {
        for (float i=0.5f; i>=0; i -= 0.01f)
        {
            thisColor = new Color(thisColor.r, thisColor.g, thisColor.b, i);
            yield return null;
        }
        isExist = false;
        Destroy(this.gameObject);
    }
}
