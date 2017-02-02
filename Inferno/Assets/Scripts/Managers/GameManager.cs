using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;

[System.Serializable]
public class GameManager
{
    private static GameManager inst = null;

    private GameManager()
    {
        all_items.Add(global::itemList.BATTERY, new Battery());
        all_items.Add(global::itemList.BBONG, new BBong());
        all_items.Add(global::itemList.HAPPINESSCIRCUIT, new HappinessCircuit());
        all_items.Add(global::itemList.ICECREAM, new Icecream());
        all_items.Add(global::itemList.INVISIBLESOMETHING, new InvisibleSomething());
        all_items.Add(global::itemList.MELTENICECREAM, new MeltenIcecream());
        all_items.Add(global::itemList.WATERBOTTLE, new Waterbottle());
    }

    public static GameManager Inst()
    {
        if (inst == null)
        {
            inst = new GameManager();
        }
        return inst;
    }
    public Dictionary<itemList, Item> all_items = new Dictionary<itemList, Item>();
    public float distance;
    public List<Item> itemList = new List<Item>();
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

        XmlNode root = doc.FirstChild.NextSibling.NextSibling;      //xml버전과 comment를 생략. 바로 첫 노드로 넘어감

        foreach (XmlNode node in root)
        {
            if (node.NodeType == XmlNodeType.Element)
            {
                switch (node.Name)
                {
                    case "user":
                        string tmp = node.Attributes["name"].Value;
                        break;
                    case "asset":
                        this.money = ReadIntFromXML(node, "money");
                        break;
                    case "maximum":
                        this.maxDistance = ReadIntFromXML(node, "maxDistance");
                        this.maxStage = ReadIntFromXML(node, "maxStage");
                        break;
                }
            }
        }
    }

    //Int 파싱
    private int ReadIntFromXML(XmlNode node, string attributes)
    {
        string tmpValue = node.Attributes[attributes].Value;
        int Value = 0;
        int.TryParse(tmpValue, out Value);

        return Value;
    }
    
    public bool hasItem(itemList type)
    {
        foreach (var item in itemList)
        {
            if (item.type == type)
            {
                return true;
            }
        }
        return false;
    }
}
