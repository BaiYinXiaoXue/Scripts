using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        Debug.Log("shit");
        FightManager.Instance.StopAllCoroutines();

        //��ʾʧ�ܽ���

    }

    public override void OnUpdate()
    {
        
    }

}
