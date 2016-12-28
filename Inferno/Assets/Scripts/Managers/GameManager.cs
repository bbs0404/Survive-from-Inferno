using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField]
    private int money;
    [SerializeField]
    private float distance;
    [SerializeField]
    private int speedLevel;
    private void Awake()
    {
        setStatic();
    }

    public void addMoney(int amount)
    {
        money += amount;
    }
    public void subMoney(int amount)
    {
        money -= amount;
    }
    public int getMoney()
    {
        return money;
    }
    public void setMoney(int amount)
    {
        money = amount;
    }
    public float getDistance()
    {
        return distance;
    }
    public void setDistance(float amount)
    {
        distance = amount;
    }
    public int getSpeedLevel()
    {
        return speedLevel;
    }
    public void setSpeedLevel(int amount)
    {
        speedLevel = amount;
    }
}
