using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBong : Item {

    public BBong()
    {
        type = itemList.BBONG;
        amount = 0;
        int[] temp = { 3000 };
        cost = temp;
        label = "간다 간다 뿅간다!";
    }

    public BBong(int num)
    {
        type = itemList.BBONG;
        amount = num;
        int[] temp = { 3000 };
        cost = temp;
        label = "간다 간다 뿅간다!";
    }

    public override void use()
	{
        InGameSystemManager.Inst().useCoroutine(effect1());

        GameManager.Inst().all_Items[itemList.BBONG].amount--;
        if (GameManager.Inst().all_Items[itemList.BBONG].amount == 0)
            GameManager.Inst().itemList.Remove(GameManager.Inst().all_Items[itemList.BBONG]);
    } 

    IEnumerator effect1() {
        GameManager.Inst().speedLevel += 10;
        GameManager.Inst().hitResistLevel += 50;
        yield return new WaitForSeconds(5);
        GameManager.Inst().speedLevel -= 10;
        GameManager.Inst().hitResistLevel -= 50;
        Debug.Log("수분고정은 나중에");
    }
}
