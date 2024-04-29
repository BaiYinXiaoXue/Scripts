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
            //������
            FightManager.Instance.CurPowerCount = FightManager.Instance.MaxPowerCount;
            UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").UpdatePower();

            Debug.Log("��card");

            UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").Draw_Card(5);//��������
            
        });


    }

    public override void OnUpdate()
    {
       
    }


}
