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
    public int maxWater; //최대 수분
    public float water; //수분
    public int batteryCapacity; //최대 배터리
    public float battery; //남은 배터리
    public int chargePerSec; //초당 배터리 충전량
    public DayNight time; //낮밤
    public float timeRemain; //낮밤바뀌기까지 남은 시간
    public bool inShadow; // 그림자 여부
    public bool isFan; //선풍기 사용 여부

    public float lossHealth; //초당 잃는 체력
    public float constant;

    public List<Field> fields;
    

    private void Awake()
    {
        maxHealth = 100;
        health = 100f;
        water = 100f;
        lossHealth = 40;

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
        timeRemain = 10f;
    }
    private void Update()
    {
        lossHealth = 40;
        timeRemain -= Time.deltaTime;
        if (timeRemain < 0)
        {
            if (time == DayNight.Day)
                time = DayNight.Night;
            else
                time = DayNight.Day;
            timeRemain = 10f;
        }
        constant = 1;
        if (time == DayNight.Day)
        {
            foreach (var item in fields)
            {
                if (item.type == field.SHADOW)
                {
                    constant *= 0.1f;
                    inShadow = true;
                }
                else if (item.type == field.SUN)
                {
                    if (GameManager.Inst().fanCharger)
                        battery += 10 + chargePerSec * Time.deltaTime;
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
        }
        else
        {
            constant = 0.5f;
        }
        lossHealth *= constant;
        if (isFan)
        {
            float consume = 100 * Mathf.Pow(0.9f, GameManager.Inst().fanEnergyConsumeLevel) * Time.deltaTime; // 배터리 소모량
            if (battery < consume)
                lossHealth -= (10 + GameManager.Inst().fanPerformLevel)*(battery/consume);
            else
                lossHealth -= 10 + GameManager.Inst().fanPerformLevel;
            battery -= consume;
            if (battery < 0)
                isFan = !isFan;
        }
        if (lossHealth * Time.deltaTime > 0)
            health -= lossHealth * Time.deltaTime * (1 - GameManager.Inst().hitResistLevel * 0.1f);
        if (health < 0) //player dead
        {

        }
    }

    public void playerDead()
    {
        
    }
}
