using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : SingletonBehaviour<UserInterfaceManager>
{
    [SerializeField]
    public Canvas TitleCanvas = null;
    [SerializeField]
    public Canvas InGameCanvas = null;
    [SerializeField]
    private Canvas PauseCanvas = null;
    [SerializeField]
    private Canvas UpgradeCanvas = null;
    [SerializeField]
    private Canvas GameOverCanvas = null;
    [SerializeField]
    private Canvas ClearCanvas = null;
    [SerializeField]
    private Canvas[] CanvasList;
    [SerializeField]
    private Sprite[] spriteOfItems;
    public List<GameObject> fieldState;

    private int level;
    public Canvas shopCanvas;

    private void OnEnable()
    {
        setStatic();
        SceneManager.sceneLoaded += AssignSprites;
        SceneManager.sceneLoaded += AssignCanvases;
    }

    private void AssignCanvases(Scene arg0, LoadSceneMode arg1)
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
            else if (item.gameObject.name == "TitleCanvas")
                TitleCanvas = item;
            else if (item.gameObject.name == "ClearCanvas")
                ClearCanvas = item;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            updateShopCanvas();
            disableCanvas(shopCanvas);
            enableCanvas(UpgradeCanvas);
            updateUpgradeCanvas();
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            enableCanvas(InGameCanvas);
            disableCanvas(PauseCanvas);
            disableCanvas(GameOverCanvas);
            disableCanvas(ClearCanvas);
        }
    }

    private void AssignSprites(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 2) // InGameScene
        {
            spriteOfItems = GameObject.FindObjectOfType<DataForIngame>().spriteOfItems;
        }
        if (arg0.buildIndex == 1)
        {
            spriteOfItems = GameObject.FindObjectOfType<DataForIngame>().spriteOfItems;
        }
    }

    void Update()
    {
        if (InGameCanvas != null && InGameCanvas.isActiveAndEnabled)
        {
            GameObject.Find("DistanceText").GetComponent<Text>().text = "목적지까지의 거리 : " + (1000 - InGameSystemManager.Inst().distance).ToString(string.Format("F1")) + "m";
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void disableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
        if (InGameCanvas != null && canvas == InGameCanvas)
            InGameSystemManager.Inst().isPaused = true;
    }

    public void enableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
        if (InGameCanvas != null && canvas == InGameCanvas)
            InGameSystemManager.Inst().isPaused = false;
    }

    public void updateInGameCanvas()
    {
        if (InGameSystemManager.Inst().isClear)
        {
            disableCanvas(PauseCanvas);
            disableCanvas(InGameCanvas);
            disableCanvas(GameOverCanvas);
            enableCanvas(ClearCanvas);
            ClearCanvas.transform.GetChild(0).transform.FindChild("SpentMoneyText").GetComponent<Text>().text = GameManager.Inst().spentMoney.ToString();
        }
        else if (InGameSystemManager.Inst().isGameOver)
        {
            disableCanvas(PauseCanvas);
            disableCanvas(InGameCanvas);
            enableCanvas(GameOverCanvas);
            if (InGameSystemManager.Inst().deadByCar)
                GameOverCanvas.transform.GetChild(0).transform.FindChild("MoneyText").GetComponent<Text>().text = Mathf.Min(-1000,GameManager.Inst().money * (-1)).ToString();
            else
                GameOverCanvas.transform.GetChild(0).transform.FindChild("MoneyText").GetComponent<Text>().text = ((int)InGameSystemManager.Inst().distance * 3).ToString();
        }
        else if (InGameSystemManager.Inst().isPaused)
        {
            enableCanvas(PauseCanvas);
            disableCanvas(InGameCanvas);
        }
        else if (!InGameSystemManager.Inst().isPaused && !InGameSystemManager.Inst().isGameOver && !InGameSystemManager.Inst().isClear)
        { 
            enableCanvas(InGameCanvas);
            disableCanvas(PauseCanvas);
            disableCanvas(GameOverCanvas);
            disableCanvas(ClearCanvas);
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

            InGameCanvas.transform.FindChild("HP Bar").FindChild("Mask").FindChild("HP").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().health * 7.5f, 60);
            InGameCanvas.transform.FindChild("HP Bar").FindChild("HealthText").GetComponent<Text>().text = ((int)InGameSystemManager.Inst().health).ToString();
            InGameCanvas.transform.FindChild("Water Bar").FindChild("Mask").FindChild("Water").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().water * 5, 85);
            InGameCanvas.transform.FindChild("HP Bar").FindChild("Stamina").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().stamina * 3, 7.5f);
            InGameCanvas.transform.FindChild("Battery").GetComponent<RectTransform>().sizeDelta = new Vector2(100 * InGameSystemManager.Inst().battery /InGameSystemManager.Inst().batteryCapacity , 20);
            InGameCanvas.transform.FindChild("Money").GetComponent<Text>().text = GameManager.Inst().money.ToString();
            if (InGameSystemManager.Inst().isBbong)
                InGameCanvas.transform.FindChild("Bbong").GetComponent<Image>().color = new Color(1, 1, 1);
            else
                InGameCanvas.transform.FindChild("Bbong").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            if (InGameSystemManager.Inst().isHappy)
                InGameCanvas.transform.FindChild("Happiness Circuit").GetComponent<Image>().color = new Color(1, 1, 1);
            else
                InGameCanvas.transform.FindChild("Happiness Circuit").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            if (InGameSystemManager.Inst().isInvisible)
                InGameCanvas.transform.FindChild("Invisible Something").GetComponent<Image>().color = new Color(1, 1, 1);
            else
                InGameCanvas.transform.FindChild("Invisible Something").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            if (!GameManager.Inst().fan)
                InGameCanvas.transform.FindChild("FanButton").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        InGameCanvas.transform.FindChild("Emergency").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f - InGameSystemManager.Inst().health / (InGameSystemManager.Inst().maxHealth * 2));
        InGameCanvas.transform.FindChild("DayNight").GetComponent<Animator>().SetBool("Night", InGameSystemManager.Inst().time == DayNight.Night);
    }

    public void fieldStateOn(field type)
    {
        if (InGameCanvas == null)
            Debug.LogError("InGameCanvas Invalid");
        if (type == field.SHADOW)
        {
            GameObject.Find("FieldState0").GetComponent<Image>().color = new Color(1, 1, 1);
            if (InGameSystemManager.Inst().isInvisible)
                PlayerManager.Inst().player.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
            else
                PlayerManager.Inst().player.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);
        }
        else if (type == field.FOUNTAIN)
            GameObject.Find("FieldState1").GetComponent<Image>().color = new Color(1, 1, 1);
        else if (type == field.OUTDOORFAN)
            GameObject.Find("FieldState2").GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void fieldStateOff()
    {
        if (InGameCanvas == null)
        {
            Debug.LogError("InGameCanvas Invalid");
            return;
        }
        GameObject.Find("FieldState0").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        GameObject.Find("FieldState1").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        GameObject.Find("FieldState2").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        if (InGameSystemManager.Inst().isInvisible)
            PlayerManager.Inst().player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,0.5f);
        else
            PlayerManager.Inst().player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
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

    public void updateUpgradeCostText(int cost)
    {
        UpgradeCanvas.transform.FindChild("upgradeMoney").GetComponent<Text>().text = cost.ToString();
    }

    public void Upgrade_Shop()
    {
        UpgradeCanvas.gameObject.SetActive(!UpgradeCanvas.gameObject.activeSelf);
        shopCanvas.gameObject.SetActive(!shopCanvas.gameObject.activeSelf);
    }
    
    public void newGame()
    {
        GameManager.Inst();
        GameManager.Inst().SaveGameState();
        SceneController.Inst().ChangeScene(1);
    }

    public void loadGame()
    {
        GameManager.Inst();
        try
        {
            GameManager.Inst().LoadGameState();
            SceneController.Inst().ChangeScene(1);
        }
        catch
        {
            Debug.Log("No Save File");
        }
    }

    public void updateUpgradeCanvas()
    {
        if (!GameManager.Inst().fan)
        {
            UpgradeCanvas.transform.GetChild(6).GetComponent<Button>().interactable= false;
            UpgradeCanvas.transform.GetChild(7).GetComponent<Button>().interactable = false;
            UpgradeCanvas.transform.GetChild(8).GetComponent<Button>().interactable = false;
        }
        else
        {
            UpgradeCanvas.transform.GetChild(6).GetComponent<Button>().interactable = true;
            UpgradeCanvas.transform.GetChild(7).GetComponent<Button>().interactable = true;
            UpgradeCanvas.transform.GetChild(8).GetComponent<Button>().interactable = true;
        }
        if (!GameManager.Inst().fanCharger)
        {
            UpgradeCanvas.transform.GetChild(9).GetComponent<Button>().interactable = false;
            UpgradeCanvas.transform.GetChild(10).GetComponent<Button>().interactable = false;
        }
        else
        {
            UpgradeCanvas.transform.GetChild(9).GetComponent<Button>().interactable = true;
            UpgradeCanvas.transform.GetChild(10).GetComponent<Button>().interactable = true;
        }
    }

    public void updateShopCanvas()
    {
        int i;
        for (i = 0; i < GameManager.Inst().itemList.Count; ++i)
        {
            shopCanvas.transform.FindChild("IngameItem" + i.ToString()).GetComponent<Image>().sprite = spriteOfItems[(int)GameManager.Inst().itemList[i].type];
        }
        for (; i < 3; ++i)
        {
            shopCanvas.transform.FindChild("IngameItem" + i.ToString()).GetComponent<Image>().sprite = spriteOfItems[8];
        }
    }

    public void updateItemEdit()
    {
        int i;
        for (i = 0; i < GameManager.Inst().itemList.Count; ++i)
        {
            shopCanvas.transform.FindChild("Panel").FindChild("IngameItem" + i.ToString()).GetComponent<Image>().sprite = spriteOfItems[(int)GameManager.Inst().itemList[i].type];
        }
        for (; i < 3; ++i)
        {
            shopCanvas.transform.FindChild("Panel").FindChild("IngameItem" + i.ToString()).GetComponent<Image>().sprite = spriteOfItems[8];
        }
    }
}