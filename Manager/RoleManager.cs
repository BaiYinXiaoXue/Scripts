using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���� ���
public class RoleManager 
{

    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//�濨��ID

    public void Init()//�׻غ�ս��
    {
        cardList = new List<string>();
        
        //���
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1003");
        cardList.Add("1000");
        cardList.Add("1000");
        //�ķ�
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");
        

    }
    public void InitNewFight()//����ս��
    {
        cardList = new List<string>();

        //���
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        //�ķ�
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");

        

    }




}
