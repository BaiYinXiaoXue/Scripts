using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//卡牌 金币
public class RoleManager 
{

    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//存卡牌ID

    public void Init()//首回合战斗
    {
        cardList = new List<string>();
        
        //五打
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1003");
        cardList.Add("1000");
        cardList.Add("1000");
        //四防
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");
        

    }
    public void InitNewFight()//其他战斗
    {
        cardList = new List<string>();

        //五打
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        //四防
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");

        

    }




}
