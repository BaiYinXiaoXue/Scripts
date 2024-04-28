using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //游戏数据
        GameConfigManager.Instance.Init();

        //BGM
        AudioManager.Instance.Init();

        //UI
        UIManager.instance.ShowUI<LoginUI>("LoginUI");

        //BGM
        AudioManager.Instance.PlayBGM("bgm1");


        //角色信息
        RoleManager.Instance.Init();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
