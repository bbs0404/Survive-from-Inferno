﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;

public class GameManager : SingletonBehaviour<GameManager>
{
    public float distance;
    public List<Item> all_Items;
    public List<Item> ingame_Items;
    public List<Item> itemList;
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

        //save & load test
        money = 10;
        Debug.Log("original money before saving: " + money);
        SaveGameState();
        money = 123456;
        Debug.Log("changed money: " + money);
        LoadGameState();
        Debug.Log("Loaded money: " + money);
    }


    //게임 상태 저장
    public void SaveGameState()
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = ("\t");

        string path = Path.Combine(Application.persistentDataPath, @"test.xml");

        using (XmlWriter writer = XmlWriter.Create(path, settings))
        {
            writer.WriteStartDocument();
            writer.WriteComment(" Saved game data of the game Inferno");

            //시작
            writer.WriteStartElement("content");


            //이름 기록
            writer.WriteStartElement("user");
            writer.WriteAttributeString("name", "HAPPYNARU");
            writer.WriteEndElement();

            //아이템 리스트 기록
            writer.WriteStartElement("items");

            foreach(Item i in itemList)
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("amount", i.amount.ToString());
                writer.WriteAttributeString("type", i.type.ToString());
                writer.WriteEndElement();
            }
            
            writer.WriteEndElement();

            //스피드레벨, 열저항레벨, 수분소모레벨 기록
            writer.WriteStartElement("level");
            writer.WriteAttributeString("speedLevel", this.speedLevel.ToString());
            writer.WriteAttributeString("hitResistLevel", this.hitResistLevel.ToString());
            writer.WriteAttributeString("waterConsumeLevel", this.waterConsumeLevel.ToString());
            writer.WriteEndElement();

            //선풍기 관련 기록
            writer.WriteStartElement("fan");
            writer.WriteAttributeString("fan", this.fan.ToString());
            writer.WriteAttributeString("fanPerformLevel", this.fanPerformLevel.ToString());
            writer.WriteAttributeString("fanBatteryLevel", this.fanBatteryLevel.ToString());
            writer.WriteEndElement();

            //선풍기 충전기 관련 기록
            writer.WriteStartElement("charger");
            writer.WriteAttributeString("fanCharger", this.fanCharger.ToString());
            writer.WriteAttributeString("fanChargerLevel", this.fanChargerLevel.ToString());
            writer.WriteAttributeString("fanEnergyConsumeLevel", this.fanEnergyConsumeLevel.ToString());
            writer.WriteEndElement();

            //돈 기록
            writer.WriteStartElement("asset");
            writer.WriteAttributeString("money", this.money.ToString());
            writer.WriteEndElement();

            //최대거리, 스테이지 기록
            writer.WriteStartElement("maximum");
            writer.WriteAttributeString("maxDistance", this.maxDistance.ToString());
            writer.WriteAttributeString("maxStage", this.maxStage.ToString());
            writer.WriteEndElement();


            //끝
            writer.WriteEndElement();   //content 끝

            writer.WriteEndDocument();
            writer.Close();
        }

    }

    //게임 상태 로드
    public void LoadGameState()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path.Combine(Application.persistentDataPath, @"test.xml"));
        Debug.Log("Path of saved data: " + Path.Combine(Application.persistentDataPath, @"test.xml"));

        XmlElement content = doc["content"];

        //string name = content["user"].GetAttribute("name");
        
        //아이템리스트 로드
        foreach(XmlElement e in content["items"])
        {
            Item i = new Item();
            i.amount = System.Convert.ToInt32(e.GetAttribute("amount"));
            i.type = TryParseType(e.GetAttribute("type"));
            itemList.Add(i);
        }

        //그 외 로드
        this.speedLevel = System.Convert.ToInt32(content["level"].GetAttribute("speedLevel"));
        this.hitResistLevel = System.Convert.ToInt32(content["level"].GetAttribute("hitResistLevel"));
        this.waterConsumeLevel = System.Convert.ToInt32(content["level"].GetAttribute("waterConsumeLevel"));
        this.fan = System.Convert.ToBoolean(content["fan"].GetAttribute("fan"));
        this.fanPerformLevel = System.Convert.ToInt32(content["fan"].GetAttribute("fanPerformLevel"));
        this.fanBatteryLevel = System.Convert.ToInt32(content["fan"].GetAttribute("fanBatteryLevel"));
        this.fanCharger = System.Convert.ToBoolean(content["charger"].GetAttribute("fanCharger"));
        this.fanChargerLevel = System.Convert.ToInt32(content["charger"].GetAttribute("fanChargerLevel"));
        this.fanEnergyConsumeLevel = System.Convert.ToInt32(content["charger"].GetAttribute("fanEnergyConsumeLevel"));
        this.money = System.Convert.ToInt32(content["asset"].GetAttribute("money"));
        this.maxDistance = System.Convert.ToInt32(content["maximum"].GetAttribute("maxDistance"));
        this.maxStage = System.Convert.ToInt32(content["maximum"].GetAttribute("maxStage"));
        
    }

    //아이템 타입 파싱
    private itemList TryParseType(string type)
    {
        switch (type)
        {
            case "WATERBOTTLE":
                return global::itemList.WATERBOTTLE;
            case "BATTERY":
                return global::itemList.BATTERY;
            case "ICECREAM":
                return global::itemList.ICECREAM;
            case "MELTENICECREAM":
                return global::itemList.MELTENICECREAM;
            case "BBONG":
                return global::itemList.BBONG;
            case "HAPPINESSCIRCUIT":
                return global::itemList.HAPPINESSCIRCUIT;
            case "INVISIBLESOMETHING":
                return global::itemList.INVISIBLESOMETHING;
            case "FAN":
                return global::itemList.FAN;
            case "NONE":
                return global::itemList.NONE;
            default:
                return global::itemList.NONE;
        }
    }
    
    public void sceneChange(int num)
    {
        SceneManager.LoadScene(num);
    }
}
