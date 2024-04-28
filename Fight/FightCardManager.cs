using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightCardManager
{
    public static FightCardManager Instance =new FightCardManager();

    public List<string> cardList;

    public List<string> usedCardList;



    public void Init()
    {
        cardList = new List<string>();


        usedCardList = new List<string>();

        //定义临时集合

        List<string> tempList = new List<string>();

        // 玩家卡牌存到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);


        while (tempList.Count > 0)
        {
            //随机下标
            int tempIndex = Random.Range(0,tempList.Count);


            //添加到卡堆
            cardList.Add(tempList[tempIndex]);

            //临时集合删除
            tempList.RemoveAt(tempIndex);


        }

        Debug.Log(cardList.Count);
    }




    //
    public bool hascard()//是否有卡牌
    {

        return cardList.Count > 0;

    }


    public string DrawCard()//抽卡
    {
        string id = cardList[cardList.Count - 1];

        cardList.RemoveAt(cardList.Count-1);


        return id;
    }






}
