using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FightType
{//ö��
    None,
    Init,
    Player,
    Enemy,
    Win,
    Loss


}



public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fighUnit;


    public int MaxHp;
    public int CurHp;


    public int MaxPowerCount;//�������
    public int CurPowerCount;
    public int DefenseCount;


    public void Init()
    {
        MaxHp = 100;
        CurHp = 100;
        MaxPowerCount = 3;
        CurPowerCount = 3;
        DefenseCount = 0;


    }



    


    private void Awake()
    {
        Instance = this;
    }


    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:

                fighUnit = new FightInit();


                break;
            case FightType.Player:
                fighUnit = new Fight_PlayerTurn();


                break;
            case FightType.Enemy:
                fighUnit = new Fight_EnemyTurn();


                break;
            case FightType.Win:
                fighUnit = new Fight_Win();


                break;
            case FightType.Loss:
                fighUnit = new Fight_Loss();


                break;
        
        }

        fighUnit.Init();//��ʼ��





    }


    private void Update()
    {
        if (fighUnit!=null) {

            fighUnit.OnUpdate();
        }




    }



    public void getPlayHit(int hit)//��������߼�
    {
        if (DefenseCount >= hit)
        {
            DefenseCount -= hit;

        }
        else
        {
            hit -= DefenseCount;
            DefenseCount = 0;
            CurHp -= hit;
            if (CurHp <= 0)
            {
                CurHp = 0;
                //�л�����Ϸʧ��
                ChangeType(FightType.Loss);
            }
            
        }



        UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").UpdateHP();
        UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").UpdateDef();
    }
   




}
