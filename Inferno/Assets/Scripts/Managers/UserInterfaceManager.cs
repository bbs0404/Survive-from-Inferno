using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : SingletonBehaviour<UserInterfaceManager> {
    [SerializeField]
    private Canvas InGameCanvas = null;
    private Canvas PauseCanvas = null;
    private Canvas UpgradeCanvas = null;
    private Canvas[] CanvasList;
	void Awake () {
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
