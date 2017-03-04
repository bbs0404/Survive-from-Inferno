using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBong : Item {

    public BBong()
    {
        type = itemList.BBONG;
        amount = 0;
        int[] temp = { 1500 };
        cost = temp;
        label = "간다 간다 뿅간다!";
    }

    public BBong(int num)
    {
        type = itemList.BBONG;
        amount = num;
        int[] temp = { 1500 };
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
        InGameSystemManager.Inst().isBbong = true;
        SoundManager.Inst().getBGM().pitch = 1.2f;
        yield return new WaitForSeconds(10);
        InGameSystemManager.Inst().isBbong = false;
        InGameSystemManager.Inst().isBbongSideEffect = true;
        SoundManager.Inst().getBGM().pitch = 1;
        yield return new WaitForSeconds(5);
        InGameSystemManager.Inst().isBbongSideEffect = false;
        Debug.Log("수분고정은 나중에");
    }
}
