using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn :FightUnit
{
    public override void Init()
    {
        Debug.Log("��Ļغ�");
        UIManager.instance.ShowTip("��Ļغ�", Color.green, delegate ()
        {
            //û����ʱϴ��
            if (FightCardManager.Instance.hascard()==false)
            {
                FightCardManager.Instance.Init();
                UIManager.instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();


                if (FightCardManager.Instance.cardList.Count> FightUI.tmp) {
                    UIManager.instance.GetUI<FightUI>("FightUI").createCardItem(FightUI.tmp);
                    FightUI.tmp = 0;
                }
                else
                {
                    FightUI.tmp = FightCardManager.Instance.cardList.Count;
                    UIManager.instance.GetUI<FightUI>("FightUI").createCardItem(FightUI.tmp);
                    FightUI.tmp = 0;

                }


            }
            //������
            FightManager.Instance.CurPowerCount = FightManager.Instance.MaxPowerCount;
            UIManager.instance.GetUI<FightUI>("FightUI").UpdatePower();




            Debug.Log("��card");



            UIManager.instance.GetUI<FightUI>("FightUI").createCardItem(5);//��������

            UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

            //���¿�������
            UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            
        });


    }

    public override void OnUpdate()
    {
       
    }


}
