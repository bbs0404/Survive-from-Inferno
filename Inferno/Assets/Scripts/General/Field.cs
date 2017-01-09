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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player)
        {
            InGameSystemManager.Inst().fields.Add(this);
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player && isPlayerIn)
        {
            InGameSystemManager.Inst().fields.Remove(this);
        }
    }
}
