using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : SingletonBehaviour<UserInterfaceManager>
{
    [SerializeField]
    public Canvas InGameCanvas = null;
    [SerializeField]
    private Canvas PauseCanvas = null;
    [SerializeField]
    private Canvas UpgradeCanvas = null;
    [SerializeField]
    private Canvas GameOverCanvas = null;
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
    public Canvas shopCanvas;
   

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
            else if (item.gameObject.name == "GameOverCanvas")
                GameOverCanvas = item;
            else if (item.gameObject.name == "ShopCanvas")
                shopCanvas = item;
        }
        setStatic();
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            disableCanvas(shopCanvas);
            enableCanvas(UpgradeCanvas);
        }
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
        if (InGameSystemManager.Inst().isGameOver)
        {
            disableCanvas(PauseCanvas);
            disableCanvas(InGameCanvas);
            enableCanvas(GameOverCanvas);
            GameOverCanvas.transform.GetChild(0).transform.FindChild("MoneyText").GetComponent<Text>().text = (InGameSystemManager.Inst().distance * 2).ToString();
        }
        else if (isPaused)
        {
            enableCanvas(PauseCanvas);
            disableCanvas(GameOverCanvas);
            disableCanvas(InGameCanvas);
        }
        else if (!isPaused && !InGameSystemManager.Inst().isGameOver)
        { 
            enableCanvas(InGameCanvas);
            disableCanvas(GameOverCanvas);
            disableCanvas(PauseCanvas);
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

            InGameCanvas.transform.FindChild("HP").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().health, 100);
            InGameCanvas.transform.FindChild("Water").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().water, 100);
            InGameCanvas.transform.FindChild("Stamina").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().stamina * 2, 20);
            InGameCanvas.transform.FindChild("Battery").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().battery, 20);
        }
        InGameCanvas.transform.FindChild("Emergency").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f - InGameSystemManager.Inst().health / (InGameSystemManager.Inst().maxHealth * 2));
    }
    public GameObject addFieldStateUI(field type)
    {
        if (InGameCanvas == null)
            return null;
        //if (type == field.SHADOW
        GameObject ui;
        fieldState.Add(ui = Instantiate(fieldStatePrefab));
        ui.transform.SetParent(InGameCanvas.transform);
        ui.GetComponent<RectTransform>().anchoredPosition = new Vector3(600 + fieldState.IndexOf(ui) * 100, ui.GetComponent<RectTransform>().position.y);
        //ui.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        //ui.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        ui.GetComponent<Image>().sprite = spriteOfFieldState[(int)type];
        return ui;
    }

    public void timeFog()
    {
        if (InGameSystemManager.Inst().time == DayNight.Day)
        {
            StartCoroutine(fogFadeOut());
        }
        else
        {
            StartCoroutine(fogFadeIn());
        }
    }

    IEnumerator fogFadeIn()
    {
        for (float i = 0; i<0.5f; i += 0.01f)
        {
            InGameCanvas.transform.FindChild("Fog").GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
    }

    IEnumerator fogFadeOut()
    {
        for (float i = 0.5f; i >= 0; i -= 0.01f)
        {
            InGameCanvas.transform.FindChild("Fog").GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
    }

    public void useItem(int num)
    {
        if (GameManager.Inst().itemList.Count > num)
        {
            GameManager.Inst().itemList[num].use();
            updateInGameCanvas();
        }
    }

    public void useFan()
    {
        InGameSystemManager.Inst().isFan = !InGameSystemManager.Inst().isFan;
    }

    public void updateUpgradeCostText(int cost)
    {
        UpgradeCanvas.transform.FindChild("upgradeMoney").GetComponent<Text>().text = cost.ToString();
    }

    public void Upgrade_Shop()
    {
        UpgradeCanvas.gameObject.SetActive(!UpgradeCanvas.gameObject.activeSelf);
        shopCanvas.gameObject.SetActive(!shopCanvas.gameObject.activeSelf);
    }
}