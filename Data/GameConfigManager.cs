using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager
{

    public static GameConfigManager Instance = new GameConfigManager();
    private GameConfigData cardData;
    private GameConfigData enemyData;
    private GameConfigData levelData;//¹Ø¿¨
    private GameConfigData cardTypeData;




    private TextAsset textAsset;

    public void Init(){
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);

    }

    public List<Dictionary<string,string>> GetCardLines()
    {

        return cardData.Getlines();

    }
    public List<Dictionary<string, string>> GetEnemyLines()
    {

        return enemyData.Getlines();

    }
    public List<Dictionary<string, string>> GetLevelLines()
    {

        return levelData.Getlines();

    }

    public Dictionary<string, string> GetcardById(string id) {

        return cardData.GetOneById(id);
    
    }

    public Dictionary<string, string> GetEnemyById(string id)
    {

        return enemyData.GetOneById(id);

    }
    public Dictionary<string, string> GetLevelById(string id)
    {

        return levelData.GetOneById(id);

    }


    public Dictionary<string, string> GetCardTypeById(string id)
    {

        return cardTypeData.GetOneById(id);

    }



}
