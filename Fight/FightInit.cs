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
        UIManager.instance.ShowUI<Combat_UI_Data>("Combat_UI_Data");
        //
        EnemyBaseManager.Instance.LoadRes("10001");


        FightManager.Instance.ChangeType(FightType.Player);



    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
