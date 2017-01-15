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
}
