using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FightType
{//枚举
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


    public int MaxPowerCount;//最大能量
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

        fighUnit.Init();//初始化





    }


    private void Update()
    {
        if (fighUnit!=null) {

            fighUnit.OnUpdate();
        }




    }



    public void getPlayHit(int hit)//玩家受伤逻辑
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
                //切换到游戏失败
                ChangeType(FightType.Loss);
            }
            
        }



        UIManager.instance.GetUI<FightUI>("FightUI").UpdateHP();
        UIManager.instance.GetUI<FightUI>("FightUI").UpdateDef();
    }
   




}
