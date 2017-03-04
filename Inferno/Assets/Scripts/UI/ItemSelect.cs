using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour {
    [SerializeField]
    private int count;

    private void OnEnable()
    {
        count = 3;
        GameManager.Inst().itemList = new List<Item>();
        UserInterfaceManager.Inst().updateItemEdit();
        foreach (var item in GameObject.FindObjectsOfType<Button>())
        {
            if (item.transform.parent.name == "Panel")
                continue;
            else
                item.enabled = false;
        }
    }

    public void selectItem(int n)
    {
        itemList type = (itemList) n;
        if (GameManager.Inst().all_Items[type].amount > 0 && !GameManager.Inst().itemList.Contains(GameManager.Inst().all_Items[type]))
        {
            GameManager.Inst().itemList.Add(GameManager.Inst().all_Items[type]);
            --count;
        }
        UserInterfaceManager.Inst().updateItemEdit();
        if (count == 0)
        {
            enableCanvasButtons();
            UserInterfaceManager.Inst().updateShopCanvas();
            gameObject.SetActive(false);
        }
    }

    public void enableCanvasButtons()
    {
        foreach (var item in GameObject.FindObjectsOfType<Button>())
        {
            item.enabled = true;
        }
        UserInterfaceManager.Inst().updateShopCanvas();
    }
}
