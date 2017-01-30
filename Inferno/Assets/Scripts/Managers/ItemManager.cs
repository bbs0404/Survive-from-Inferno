using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonBehaviour<ItemManager>
{
    public List<Item> lists;
    private void Awake()
    {
        lists.Add(this.gameObject.AddComponent<Icecream>());
        lists.Add(this.gameObject.AddComponent<BBong>());
        lists.Add(this.gameObject.AddComponent<Battery>());
        lists.Add(this.gameObject.AddComponent<HappinessCircuit>());
        lists.Add(this.gameObject.AddComponent<InvisibleSomething>());
        lists.Add(this.gameObject.AddComponent<MeltenIcecream>());
        lists.Add(this.gameObject.AddComponent<Waterbottle>());

        setStatic();
    }

    public void useItem(int num)
    {
        if (GameManager.Inst().itemList.Count > num)
        {
            GameManager.Inst().itemList[num].use();
            if (GameManager.Inst().itemList[num].amount <= 0 && GameManager.Inst().itemList[num].type != itemList.FAN)
            {
                GameManager.Inst().itemList.RemoveAt(num);
            }
            UserInterfaceManager.Inst().updateInGameCanvas();
        }
    }

    public void useFan()
    {
        InGameSystemManager.Inst().isFan = !InGameSystemManager.Inst().isFan;
    }
}
