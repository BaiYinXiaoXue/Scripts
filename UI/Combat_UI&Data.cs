using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System;

using Random=UnityEngine.Random;

public class Card_State{
    //0: Library
    //1: Hand
    //2: Graveyard
    public int State=0;
    public string Card_id="";
}

public class Combat_UI_Data : UIBase
{
    private Text cardCountTxt;//��������
    private Text usedCardCountTxt;//���ƶ�
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Image defImg;
    private Text defTxt;

    public static Stack Stack_Main;

    private List<CardItem> Card_item_in_Hand;

    private List<Card_State> Card_State_List;

    //Store id of item in Card_State_List
    private List<int> Card_in_Library;
    //Store id of item in Card_State_List
    private List<int> Card_in_Hand;
    //Store id of item in Card_State_List
    private List<int> Card_in_Graveyard;

    //Store id of item in Card_State_List
    private List<int> Library_Order;

    private void Awake()
    {
        Card_item_in_Hand = new List<CardItem>();
        Card_State_List = new List<Card_State>();
        Stack_Main =new Stack();

        for (int id = 0;id<RoleManager.Instance.cardList.Count;id++){
            Card_State temp_item;
            temp_item = new Card_State();
            temp_item.State=0;
            temp_item.Card_id=RoleManager.Instance.cardList[id];
            Card_State_List.Add(temp_item);
        }
        Refresh_Card_State();
        Shuffle();

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

    public bool Library_Hv_Card(){
        return (Card_in_Library.Count>0);
    }

    private void Start()
    {
        UpdateCardCount();
        UpdateDef();
        UpdateHP();
        
        
        UpdatePower();
        UpdateUsedCardCount();
        
    }

    public void UpdateHP()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount= (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
    }

    private void Refresh_Card_State(){
        Card_in_Library= new List<int>();
        Card_in_Hand= new List<int>();
        Card_in_Graveyard= new List<int>();
        for (int id = 0;id<Card_State_List.Count;id++){
            switch (Card_State_List[id].State){
                case 0:
                    Card_in_Library.Add(id);
                    break;
                case 1:
                    Card_in_Hand.Add(id);
                    break;
                case 2:
                    Card_in_Graveyard.Add(id);
                    break;
            }
        }
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

        cardCountTxt.text = Card_in_Library.Count.ToString();


    }

    public void UpdateUsedCardCount()
    {

        usedCardCountTxt.text = Card_in_Graveyard.Count.ToString();


    }

    private void Shuffle(){
        for (int id = 0;id<Card_in_Graveyard.Count;id++){
            Card_State_List[Card_in_Graveyard[id]].State=0;
        }
        Refresh_Card_State();
        Library_Order= new List<int>();
        List<int> temp_RNG;
        temp_RNG = new List<int>();
        for (int id = 0;id<Card_in_Library.Count;id++){
            temp_RNG.Add(Card_in_Library[id]);
        }
        for (int id = 0;id<Card_in_Library.Count;id++){
            //Choose and add random object
            int choice;
            choice =Random.Range(0,temp_RNG.Count);
            Library_Order.Add(temp_RNG[choice]);
            //remove used object
            temp_RNG.RemoveAt(choice);
        }
        Refresh_Card_State();
    }

    public void Draw_Card(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (!Library_Hv_Card()){
                Shuffle();
            }
            Draw_Single_Card();
        }
        UpdateCardItemPos();

    }

    private void Draw_Single_Card(){
        //Get Card id
        int Card_List_Pos=Library_Order[0];
        Card_State_List[Card_List_Pos].State=1;
        Library_Order.RemoveAt(0);

        GameObject obj = Instantiate(Resources.Load("UI/CardItem"),transform)as GameObject;
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);

        //Read data from Card_State_List
        Dictionary<string, string> data = GameConfigManager.Instance.GetcardById(Card_State_List[Card_List_Pos].Card_id);
        CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
        item.Init(data,Card_List_Pos);

        Card_item_in_Hand.Add(item);
        UpdateCardItemPos();
    }

    public void UpdateCardItemPos()
    {
        Refresh_Card_State();
        UpdateUsedCardCount();
        UpdateCardCount();
        float offset;
        offset = new float();
        offset = 1000.0f / Card_in_Hand.Count;
        Vector2 startPos = new Vector2(-Card_in_Hand.Count/2.0f*offset+offset*0.5f,-575);

        for (int i = 0; i < Card_in_Hand.Count; i++)
        {
            Card_item_in_Hand[i].GetComponent<RectTransform>().DOAnchorPos(startPos,0.5f);
            startPos.x = startPos.x + offset;
        }

    }

    public void Discard(int id)
    {
        int Choice = 0;
        for (int t_id = 0;t_id<Card_item_in_Hand.Count;t_id++){
            if (Card_item_in_Hand[t_id].Card_State_List_Reference_id==id){
                Choice=t_id;
            }
        }
        Card_State_List[id].State=2;
        AudioManager.Instance.playEffect("lose");
        Card_item_in_Hand[Choice].enabled = false;//���ÿ����߼�
        //�ӵ����ƶ�
        Card_item_in_Hand[Choice].GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);
        Card_item_in_Hand[Choice].transform.DOScale(0,0.25f);
        Destroy(Card_item_in_Hand[Choice].gameObject, 1);

        Card_item_in_Hand.RemoveAt(Choice);

        UpdateCardItemPos();
    }




    public void Discard_Hand()//ɾ�����п���
    {

        for (int i = Card_in_Hand.Count-1; i >=0; i--)
        {
            Discard(Card_in_Hand[i]);
        }

    }





}
