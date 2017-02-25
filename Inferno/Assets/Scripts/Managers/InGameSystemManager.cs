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
    public bool isGameOver;
    public float distance;
    public float stamina;
    public float stamCharge = 30;

    public float lossHealth; //초당 잃는 체력
    public float constant;

    public List<Field> fields;

    public GameObject cloud;
    [SerializeField]
    private float cloudTimer;

    private void Awake()
    {
        maxHealth = 100;
        maxWater = 100;
        health = 100f;
        water = 100f;
        lossHealth = 8 * (1 - 0.1f * GameManager.Inst().hitResistLevel);
        stamina = 100;
        cloudTimer = Random.Range(5, 10);
        isGameOver = false;

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
        if (!isGameOver)
        {
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
                        battery += 10 + chargePerSec * Gametime.deltaTime;
                    water -= 0.1f;
                    if (water < 0)
                        water = 0;
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
                    lossHealth -= (10 + GameManager.Inst().fanPerformLevel) * (battery / consume);
                else
                    lossHealth -= 10 + GameManager.Inst().fanPerformLevel;
                battery -= consume;
                if (battery < 0)
                    isFan = !isFan;
            }
            if (lossHealth * Gametime.deltaTime > 0)
                health -= lossHealth * Gametime.deltaTime * (1 - GameManager.Inst().hitResistLevel * 0.1f) * (1 + (100 - water) / 100);

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
        Invoke("GameOver", 3);
        UserInterfaceManager.Inst().disableCanvas(UserInterfaceManager.Inst().InGameCanvas);
        GameManager.Inst().money += (int)(distance * 2);
        PlayerManager.Inst().player.GetComponent<Animator>().SetTrigger("FAINT");
    }

    public void playerDeadByCar()
    {
        isGameOver = true;
        Invoke("GameOver", 3);
        UserInterfaceManager.Inst().disableCanvas(UserInterfaceManager.Inst().InGameCanvas);
        Invoke("car",0.1f);
    }

    private void GameOver()
    {
        UserInterfaceManager.Inst().updateInGameCanvas();
    }

    public void useCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    private void car()
    {
        StartCoroutine(carAccident());
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
        PlayerManager.Inst().player.GetComponent<Animator>().SetTrigger("FAINT");
    }
}
