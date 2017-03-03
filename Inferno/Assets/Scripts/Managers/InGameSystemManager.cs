﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isGameOver;
    public bool isPaused;
    public float distance;
    public float stamina;
    public float stamCharge = 30;

    public float lossHealth; //초당 잃는 체력
    public float constant;

    public List<Field> fields;

    public GameObject cloud;
    [SerializeField]
    private float cloudTimer;

    public bool isBbong;
    public bool isBbongSideEffect;
    public float sideEffectTimer;

    public bool isHappy;
    public bool isInvisible;

    public bool deadByCar;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Initialize;
    }

    private void Initialize(Scene arg0, LoadSceneMode arg1)
    {
        maxHealth = 100;
        maxWater = 100;
        health = 100f;
        water = 100f;
        lossHealth = 8 * (1 - 0.1f * GameManager.Inst().hitResistLevel);
        stamina = 100;
        cloudTimer = Random.Range(5, 10);
        isGameOver = false;
        isPaused = false;
        isBbong = false;
        isHappy = false;
        isInvisible = false;
        isBbongSideEffect = false;
        sideEffectTimer = 5;
        deadByCar = false;

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
        if (!isGameOver && !isPaused)
        {
            if (isBbongSideEffect)
                water = 0;
            inShadow = false;
            lossHealth = 8 * (1 - 0.1f * GameManager.Inst().hitResistLevel);
            timeRemain -= Gametime.deltaTime;
            cloudTimer -= Gametime.deltaTime;

            if (timeRemain < 0)
            {
                if (time == DayNight.Day)
                    time = DayNight.Night;
                else
                    time = DayNight.Day;
                UserInterfaceManager.Inst().timeFog();
                timeRemain = 10f;
            }
            constant = 1;
            inShadow = false;
            UserInterfaceManager.Inst().fieldStateOff();
            foreach (var item in fields)
            {
                if (item.type == field.SHADOW && !inShadow)
                {
                    constant *= 0.1f;
                    inShadow = true;
                    if (time != DayNight.Night)
                        UserInterfaceManager.Inst().fieldStateOn(field.SHADOW);
                }
                else if (item.type == field.ASPHALT)
                {
                    if (!inShadow)
                    {
                        constant *= 1.5f;
                    }
                    else
                    {
                        constant *= 0.8f;
                    }
                }
                else if (item.type == field.OUTDOORFAN)
                {
                    constant *= 1.2f;
                    UserInterfaceManager.Inst().fieldStateOn(field.OUTDOORFAN);
                }
                else if (item.type == field.FOUNTAIN)
                {
                    constant *= 0.5f;
                    water += 20 * Gametime.deltaTime;
                    if (water > maxWater)
                        water = maxWater;
                    UserInterfaceManager.Inst().fieldStateOn(field.FOUNTAIN);
                }
                else if (item.type == field.CROSSWALK)
                {

                }
            }
            if (time == DayNight.Day)
            {
                if (!inShadow) //그림자에 있지 않는 경우
                {
                    if (GameManager.Inst().fanCharger)
                        battery += Mathf.Min(battery + 10 + chargePerSec * Gametime.deltaTime, batteryCapacity);
                    water -= 0.1f;
                    if (water < 0)
                        water = 0;
                }
            }
            else
            {
                constant = 0.4f;
            }

            if (isBbong)
                constant *= 0.5f;

            lossHealth *= constant;

            if (isFan)
            {
                float consume = 10 * Mathf.Pow(0.9f, GameManager.Inst().fanEnergyConsumeLevel) * Gametime.deltaTime; // 배터리 소모량
                if (battery < consume)
                    lossHealth -= (4 + GameManager.Inst().fanPerformLevel * 0.4f) * (battery / consume);
                else
                    lossHealth -= 4 + GameManager.Inst().fanPerformLevel * 0.4f;
                battery -= consume;
                if (battery < 0)
                    isFan = !isFan;
            }
            if (lossHealth * Gametime.deltaTime > 0)
            {
                if (isHappy)
                    health -= lossHealth * Gametime.deltaTime * (1 - GameManager.Inst().hitResistLevel * 0.1f);
                else
                    health -= lossHealth * Gametime.deltaTime * (1 - GameManager.Inst().hitResistLevel * 0.1f) * (1 + (100 - water) / 100);

            }
            UserInterfaceManager.Inst().updateInGameCanvas();

            if (health < 0) //player dead
            {
                playerDead();
            }

            if (cloudTimer < 0)
            {
                cloudTimer = Random.Range(5f, 10);
                int n = Random.Range(0, 4);
                for (int i = 0; i < n; ++i)
                {
                    GameObject cld;
                    float j;
                    (cld = Instantiate(cloud)).transform.position = PlayerManager.Inst().player.transform.position + new Vector3(Random.Range(-30, 30), 10);
                    cld.transform.localScale = new Vector3(j = Random.Range(1f, 2f), j, j);
                }
            }
        }
    }

    public void playerDead()
    {
        isGameOver = true;
        StartCoroutine(FadeOutBGM());
        Invoke("GameOver", 3);
        UserInterfaceManager.Inst().disableCanvas(UserInterfaceManager.Inst().InGameCanvas);
        PlayerManager.Inst().player.GetComponent<Animator>().SetTrigger("FAINT");
        GameManager.Inst().money += (int)(distance * 5);
    }

    public void playerDeadByCar()
    {
        isGameOver = true;
        deadByCar = true;
        StartCoroutine(FadeOutBGM());
        Invoke("GameOver", 3);
        UserInterfaceManager.Inst().disableCanvas(UserInterfaceManager.Inst().InGameCanvas);
        GameManager.Inst().money = Mathf.Max(GameManager.Inst().money - 1000, 0);
        car();
    }

    private void car()
    {
        StartCoroutine(carAccident());
    }

    public void GameClear()
    {

    }

    private void GameOver()
    {
        UserInterfaceManager.Inst().updateInGameCanvas();
        GameManager.Inst().SaveGameState();
    }

    public void useCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    IEnumerator carAccident()
    {
        CrossWalk cross = null;
        foreach (var item in fields)
        {
            if (item.type == field.CROSSWALK)
            {
                cross = (CrossWalk)item;
            }
        }
        Invoke("Faint", 1.5f);
        if (PlayerController.isMoving)
        {
            if (PlayerManager.Inst().player.transform.position.x < cross.transform.position.x)
            {
                for (float i = PlayerManager.Inst().player.transform.position.x; i < cross.transform.position.x; i += 0.08f)
                {
                    PlayerManager.Inst().player.transform.position = new Vector3(i, PlayerManager.Inst().player.transform.position.y, PlayerManager.Inst().player.transform.position.z);
                    yield return null;
                }
            }
            else
            {
                for (float i = PlayerManager.Inst().player.transform.position.x; i > cross.transform.position.x; i -= 0.08f)
                {
                    PlayerManager.Inst().player.transform.position = new Vector3(i, PlayerManager.Inst().player.transform.position.y, PlayerManager.Inst().player.transform.position.z);
                    yield return null;
                }
            }
        }
    }

    private void Faint()
    {
        PlayerManager.Inst().player.GetComponent<Animator>().SetTrigger("FAINT");
    }

    public void useItem(int num)
    {
        if (GameManager.Inst().itemList.Count > num)
        {
            GameManager.Inst().itemList[num].use();
            UserInterfaceManager.Inst().updateInGameCanvas();
        }
    }

    public void useFan()
    {
        if (battery > 0)
            isFan = !isFan;
    }
    
    IEnumerator FadeOutBGM()
    {
        for (float i=SoundManager.Inst().getBGM().volume; i> SoundManager.Inst().getBGM().volume / 2; i-=0.02f)
        {
            SoundManager.Inst().getBGM().volume = i;
            yield return null;
        }
    }
}
