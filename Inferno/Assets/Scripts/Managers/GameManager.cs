using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public float distance;
    public Item[] itemList = new Item[3];
    public int speedLevel; //속도
    public int hitResistLevel; // 열저항
    public int waterConsumeLevel; //수분 소모율
    public bool fan; //선풍기
    public int fanPerformLevel; //선풍기 성능
    public int fanBatteryLevel; //선풍기 배터리
    public bool fanCharger; //선풍기 충전기
    public int fanChargerLevel; //충전기 성능
    public int fanEnergyConsumeLevel; //에너지 효율
    public int money;

    public int maxDistance; //최대 거리
    public int maxStage; //클리어한 스테이지

    void Awake()
    {
        setStatic();
    }
}
