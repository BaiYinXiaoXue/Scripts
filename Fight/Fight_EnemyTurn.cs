using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit
{
    public override void Init()
    {
       //删除所有卡牌
       UIManager.instance.GetUI<FightUI>("FightUI").removeAllCards();
        UIManager.instance.ShowTip("敌方回合", Color.red, delegate () {
            FightManager.Instance.StartCoroutine(EnemyBaseManager.Instance.DoAllEnemyAction());
           
        
        });

    }
    public override void OnUpdate()
    {
        
    }


}
