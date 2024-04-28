using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System;



public class FightUI : UIBase
{
    private Text cardCountTxt;//��������
    private Text usedCardCountTxt;//���ƶ�
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Image defImg;
    private Text defTxt;

    private List<CardItem> CardItemList;

    private void Awake()
    {
        CardItemList = new List<CardItem>();

        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardCountTxt = transform.Find("usedCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/hpTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();

        defTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();


        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);

    }

    private void onChangeTurnBtn()//��һغϽ������л����˻غ�
    {
        //ֻ����һغϲ����л�
        if (FightManager.Instance.fighUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);

        }

        
    }

    private void Start()
    {
        UpdateCardCount();
        UpdateDef();
        UpdateHP();
        
        
        UpdatePower();
        UpdateUsedCardCount();
        
    }


  //  public static int tmp=0;






    public void UpdateHP()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount= (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;



    }


    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
        



    }


    public void UpdateDef()
    {

        defTxt.text = FightManager.Instance.DefenseCount.ToString();


    }



    //��������
    public void UpdateCardCount()
    {

        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();


    }

    public void UpdateUsedCardCount()
    {

        usedCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();


    }

    //��������
    public void createCardItem(int count)
    {
        if (count>FightCardManager.Instance.cardList.Count)
        {
            // tmp = count;
            count = FightCardManager.Instance.cardList.Count;
           // tmp -= count;


        }

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"),transform)as GameObject;

            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
            //var item =obj.AddComponent<CardItem>();

           

            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetcardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            //CardItem
            

            item.Init(data);
            CardItemList.Add(item);



        }




    }

    public void UpdateCardItemPos()
    {
        float offset = 1000.0f / CardItemList.Count;
        Vector2 startPos = new Vector2(-CardItemList.Count/2.0f*offset+offset*0.5f,-575);


        for (int i = 0; i < CardItemList.Count; i++)
        {
            CardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos,0.5f);
          
            

            startPos.x = startPos.x + offset;
        }
        

        




    }

    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.playEffect("lose");
        item.enabled = false;//���ÿ����߼�

        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);

        //��������
        cardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
        //�Ӽ���ɾ��
        CardItemList.Remove(item);

        //ˢ�¿���λ��
        UpdateCardItemPos();


        //�ӵ����ƶ�
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);
        item.transform.DOScale(0,0.25f);
        Destroy(item.gameObject, 1);

    }




    public void removeAllCards()//ɾ�����п���
    {

        for (int i = CardItemList.Count-1; i >=0; i--)
        {
            RemoveCard(CardItemList[i]);
        }

    }





}
