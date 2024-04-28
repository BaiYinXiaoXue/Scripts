using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
    
    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
        UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
        UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        UIManager.instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();


    }
}
