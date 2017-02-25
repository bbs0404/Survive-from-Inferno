using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLight : MonoBehaviour {
    [SerializeField]
    private bool isGreen;

    public void updateSignal(bool green)
    {
        if (isGreen)
        {
            if (green)
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            else
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.3f, 0);
        }
        else
        {
            if (!green)
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            else
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0, 0);
        }
    }
}
