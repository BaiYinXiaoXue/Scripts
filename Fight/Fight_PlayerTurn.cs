using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn :FightUnit
{
    public override void Init()
    {
        Debug.Log("你的回合");
        UIManager.instance.ShowTip("你的回合", Color.green, delegate ()
        {
            //没有牌时洗牌
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
            //补能量
            FightManager.Instance.CurPowerCount = FightManager.Instance.MaxPowerCount;
            UIManager.instance.GetUI<FightUI>("FightUI").UpdatePower();




            Debug.Log("抽card");



            UIManager.instance.GetUI<FightUI>("FightUI").createCardItem(5);//抽五张牌

            UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

            //更新卡牌数量
            UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            
        });


    }

    public override void OnUpdate()
    {
       
    }


}
