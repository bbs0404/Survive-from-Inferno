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

    public bool isPlayerIn = false;
    public field type;
    public GameObject fieldStateUI;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player)
        {
            InGameSystemManager.Inst().fields.Add(this);
            isPlayerIn = true;
            fieldStateUI = UserInterfaceManager.Inst().addFieldStateUI(type);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player && isPlayerIn)
        {
            InGameSystemManager.Inst().fields.Remove(this);
            isPlayerIn = false;
            if (fieldStateUI != null)
            {
                UserInterfaceManager.Inst().fieldState.Remove(fieldStateUI);
                Destroy(fieldStateUI);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerManager.Inst().player && !isPlayerIn)
        {
            InGameSystemManager.Inst().fields.Add(this);
            isPlayerIn = true;
            fieldStateUI = UserInterfaceManager.Inst().addFieldStateUI(type);
        }
    }

    public void DestroyField()
    {
        if (fieldStateUI != null)
            Destroy(fieldStateUI);
        Destroy(this);
    }
}
