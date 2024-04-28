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

        //������ʱ����

        List<string> tempList = new List<string>();

        // ��ҿ��ƴ浽��ʱ����
        tempList.AddRange(RoleManager.Instance.cardList);


        while (tempList.Count > 0)
        {
            //����±�
            int tempIndex = Random.Range(0,tempList.Count);


            //��ӵ�����
            cardList.Add(tempList[tempIndex]);

            //��ʱ����ɾ��
            tempList.RemoveAt(tempIndex);


        }

        Debug.Log(cardList.Count);
    }




    //
    public bool hascard()//�Ƿ��п���
    {

        return cardList.Count > 0;

    }


    public string DrawCard()//�鿨
    {
        string id = cardList[cardList.Count - 1];

        cardList.RemoveAt(cardList.Count-1);


        return id;
    }






}
