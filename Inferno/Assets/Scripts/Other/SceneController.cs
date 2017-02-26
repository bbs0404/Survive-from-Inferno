using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public void ChangeScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void disableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
        if (UserInterfaceManager.Inst().InGameCanvas != null && canvas == UserInterfaceManager.Inst().InGameCanvas)
            InGameSystemManager.Inst().isPaused = true;
    }

    public void enableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
        if (UserInterfaceManager.Inst().InGameCanvas != null && canvas == UserInterfaceManager.Inst().InGameCanvas)
            InGameSystemManager.Inst().isPaused = false;
    }
}
