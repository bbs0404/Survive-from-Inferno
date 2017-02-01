using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
    
    public virtual void interact()
    {

    }

    private void OnMouseDown()
    {
        if (UserInterfaceManager.Inst().isPaused || InGameSystemManager.Inst().isGameOver)
            return;
        Debug.Log(this.gameObject.name);
        interact();
    }
}
