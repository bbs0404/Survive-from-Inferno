using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : SingletonBehaviour<UserInterfaceManager>
{
    [SerializeField]
    private Canvas InGameCanvas = null;
    [SerializeField]
    private Canvas PauseCanvas = null;
    [SerializeField]
    private Canvas UpgradeCanvas = null;
    [SerializeField]
    private Canvas[] CanvasList;
    [SerializeField]
    private Sprite[] spriteOfItems;
    [SerializeField]
    private Sprite[] spriteOfFieldState;
    public List<GameObject> fieldState;
    public GameObject fieldStatePrefab;
    public bool isPaused = false;
    private int level;


    void Awake()
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
        setStatic();
    }



    //private void OnLevelWasLoaded(int level)
    //{
    //    CanvasList = (Canvas[])GameObject.FindObjectsOfType(typeof(Canvas));
    //    foreach (var item in CanvasList)
    //    {
    //        if (item.gameObject.name == "InGameCanvas")
    //            InGameCanvas = item;
    //        else if (item.gameObject.name == "PauseCanvas")
    //            PauseCanvas = item;
    //        else if (item.gameObject.name == "UpgradeCanvas")
    //            UpgradeCanvas = item;
    //    }
    //    this.level = level;
    //}

    // Update is called once per frame
    void Update()
    {
        if (InGameCanvas != null && InGameCanvas.isActiveAndEnabled)
        {
            GameObject.Find("DistanceText").GetComponent<Text>().text = "목적지까지의 거리 : " + (1000 - InGameSystemManager.Inst().distance).ToString(string.Format("F1")) + "m";
        }
    }

    public void disableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
        if (InGameCanvas != null && canvas == InGameCanvas)
            isPaused = true;
    }

    public void enableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
        if (InGameCanvas != null && canvas == InGameCanvas)
            isPaused = false;
    }

    public void updateInGameCanvas()
    {
        int i;
        GameObject itemButton;
        for (i = 0; i < GameManager.Inst().itemList.Count; ++i)
        {
            (itemButton = GameObject.Find("ItemButton" + i.ToString())).GetComponent<Image>().sprite =
                spriteOfItems[(int)GameManager.Inst().itemList[i].type];
            itemButton.GetComponentInChildren<Text>().text = GameManager.Inst().itemList[i].amount.ToString();
        }
        for (; i < 3; ++i)
        {
            (itemButton = GameObject.Find("ItemButton" + i.ToString())).GetComponent<Image>().sprite =
               spriteOfItems[8];
            itemButton.GetComponentInChildren<Text>().text = "";
        }


    }
    public GameObject addFieldStateUI(field type)
    {
        if (InGameCanvas == null)
            return null;
        //if (type == field.SHADOW
        GameObject ui;
        fieldState.Add(ui = Instantiate(fieldStatePrefab));
        ui.transform.parent = InGameCanvas.transform;
        ui.GetComponent<RectTransform>().anchoredPosition = new Vector3(400 + fieldState.IndexOf(ui) * 50, ui.GetComponent<RectTransform>().position.y);
        //ui.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        //ui.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        ui.GetComponent<Image>().sprite = spriteOfFieldState[(int)type];
        return ui;
    }

}