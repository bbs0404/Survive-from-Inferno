using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : SingletonBehaviour<UserInterfaceManager> {
    [SerializeField]
    private Canvas InGameCanvas = null;
    [SerializeField]
    private Canvas PauseCanvas = null;
    [SerializeField]
    private Canvas UpgradeCanvas = null;
    [SerializeField]
    private Canvas[] CanvasList;
    private int level;

	void Awake () {
        setStatic();
    }

    

    private void OnLevelWasLoaded(int level)
    {
        CanvasList = (Canvas[])GameObject.FindObjectsOfType(typeof(Canvas));
        foreach (var item in CanvasList)
        {
            if (item.gameObject.name == "InGameCanvas")
                InGameCanvas = item;
            else if (item.gameObject.name == "PauseCanvas")
                PauseCanvas = item;
            else if (item.gameObject.name == "UpgradeCanvas")
                UpgradeCanvas = item;
        }
        this.level = level;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void disableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
    }

    public void enableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
    }
}
