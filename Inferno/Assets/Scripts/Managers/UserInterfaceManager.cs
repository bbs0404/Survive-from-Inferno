﻿using System.Collections;
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
            else if (item.gameObject.name == "GameOverCanvas")
                GameOverCanvas = item;
        }
        setStatic();
    }

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

            InGameCanvas.transform.FindChild("HP Bar").FindChild("Mask").FindChild("HP").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().health * 7.5f, 60);
            InGameCanvas.transform.FindChild("HP Bar").FindChild("HealthText").GetComponent<Text>().text = ((int)InGameSystemManager.Inst().health).ToString();
            InGameCanvas.transform.FindChild("Water Bar").FindChild("Mask").FindChild("Water").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().water * 5, 85);
            InGameCanvas.transform.FindChild("HP Bar").FindChild("Stamina").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().stamina * 3, 7.5f);
            InGameCanvas.transform.FindChild("Battery").GetComponent<RectTransform>().sizeDelta = new Vector2(InGameSystemManager.Inst().battery, 20);
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
            PlayerManager.Inst().player.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
        }
        else if (type == field.FOUNTAIN)
            GameObject.Find("FieldState1").GetComponent<Image>().color = new Color(1, 1, 1);
        else if (type == field.OUTDOORFAN)
            GameObject.Find("FieldState2").GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void fieldStateOff()
    {
        if (InGameCanvas == null)
            Debug.LogError("InGameCanvas Invalid");
        GameObject.Find("FieldState0").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        GameObject.Find("FieldState1").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        GameObject.Find("FieldState2").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        PlayerManager.Inst().player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
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
}