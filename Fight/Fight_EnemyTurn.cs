using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit
{
    public override void Init()
    {
       //ɾ�����п���
       UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").Discard_Hand();
        UIManager.instance.ShowTip("�з��غ�", Color.red, delegate () {
            FightManager.Instance.StartCoroutine(EnemyBaseManager.Instance.DoAllEnemyAction());
           
        
        });

    }
    public override void OnUpdate()
    {
        
    }


}
