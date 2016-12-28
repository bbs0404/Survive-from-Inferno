using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayNight
{
    Day,
    Night
}

public class InGameSystemManager : SingletonBehaviour<InGameSystemManager> {

    public int maxHealth; //최대 체력
    public float health; //체력
    public float water; //수분
    public int batteryCapacity; //최대 배터리
    public float battery; //남은 배터리
    public int chargePerSec; //초당 배터리 충전량
    public DayNight time; //낮밤
    public bool inShadow; // 그림자 여부
    public bool isFan; //선풍기 사용 여부

    public int lossHealth; //초당 땡볕에서 잃는 체력
    public float constant;

    public List<Field> fields;
    

    private void Awake()
    {
        maxHealth = 100;
        health = 100f;
        water = 100f;

        if (GameManager.Inst().fan)
        {
            batteryCapacity = 100 * (GameManager.Inst().fanBatteryLevel + 1);
            battery = (float)batteryCapacity;
        }
        else
        {
            batteryCapacity = 0;
            battery = 0f;
        }
        if (GameManager.Inst().fanCharger)
        {
            chargePerSec = 10 + 5 * GameManager.Inst().fanChargerLevel;
        }
        time = DayNight.Day;
    }
    private void Update()
    {
        if (time == DayNight.Day)
        {
            battery += chargePerSec * Time.deltaTime;
            if (battery > batteryCapacity)
                battery = (float)batteryCapacity;
        }
        constant = 1;
        foreach (var item in fields)
        {
            if (item.type == field.SHADOW)
            {
                constant *= 0.1f;
                inShadow = true;
            }
            else if (item.type == field.SUN)
            {
                inShadow = false;
            }
            else if (item.type == field.ASPHALT)
            {
                if (!inShadow)
                {
                    constant *= 1.5f;
                }
            }
            else if (item.type == field.OUTDOORFAN)
            {
                constant *= 1.2f;
            }
            else if (item.type == field.FOUNTAIN)
            {
                constant *= 0.5f;
                water += 10 * Time.deltaTime;
            }
            else if (item.type == field.CROSSWALK)
            {
                
            }
        }
        health -= lossHealth * constant;
    }
}
