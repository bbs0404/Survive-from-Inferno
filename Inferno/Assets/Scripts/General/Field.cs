using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum field
{
    SUN,
    SHADOW,
    ASPHALT,
    OUTDOORFAN,
    CROSSWALK,
    FOUNTAIN
}
public class Field : MonoBehaviour {

    private bool isPlayerIn = false;
    public field type;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.Inst().player)
        {
            InGameSystemManager.Inst().fields.Add(this);
            isPlayerIn = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.Inst().player && isPlayerIn)
        {
            InGameSystemManager.Inst().fields.Remove(this);
        }
    }
}
