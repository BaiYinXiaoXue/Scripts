using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class LoginUI : UIBase
{

    private void Awake()
    {//��
        Register("bg/startBtn").onClick= onStartGameBtn;

        Register("bg/quitBtn").onClick = onQuitGameBtn;
    }


    private void onStartGameBtn(GameObject obj,PointerEventData pData) {


        Close();

        //ս����ʼ��
        FightManager.Instance.ChangeType(FightType.Init);
            


    }

    private void onQuitGameBtn(GameObject obj, PointerEventData quitGameData)
    {


        Application.Quit();
    }






}
