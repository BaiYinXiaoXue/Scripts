using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.Init();


        //�л�BGM
        AudioManager.Instance.PlayBGM("battle");
        //UI
        UIManager.instance.ShowUI<FightUI>("FightUI");
        //
        EnemyBaseManager.Instance.LoadRes("10001");
        //��ʼ������
        FightCardManager.Instance.Init();


        FightManager.Instance.ChangeType(FightType.Player);



    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
