using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class LoginUI : UIBase
{

    private void Awake()
    {//来
        Register("bg/startBtn").onClick= onStartGameBtn;

        Register("bg/quitBtn").onClick = onQuitGameBtn;
    }


    private void onStartGameBtn(GameObject obj,PointerEventData pData) {


        Close();

        //战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);
            


    }

    private void onQuitGameBtn(GameObject obj, PointerEventData quitGameData)
    {


        Application.Quit();
    }






}
