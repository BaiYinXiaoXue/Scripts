using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.Init();


        //ÇÐ»»BGM
        AudioManager.Instance.PlayBGM("battle");
        //UI
        UIManager.instance.ShowUI<FightUI>("FightUI");
        //
        EnemyBaseManager.Instance.LoadRes("10001");
        //³õÊ¼»¯ÅÆ×é
        FightCardManager.Instance.Init();


        FightManager.Instance.ChangeType(FightType.Player);



    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
