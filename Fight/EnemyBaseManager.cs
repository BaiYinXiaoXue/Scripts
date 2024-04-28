using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseManager 
{


    public static EnemyBaseManager Instance=new EnemyBaseManager();

    private List<Enemy> enemyList;//储存战斗中的敌人

    /// <summary>
    /// 加载敌人
    /// </summary>
    /// <param name="id">关卡ID</param>
    public void LoadRes(string id)
    {

        enemyList = new List<Enemy>();


        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);

        string[] enemyIds = levelData["EnemyIds"].Split('=');

        string[] enemyPos = levelData["Pos"].Split('=');

        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');



            //敌人坐标
            
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);


            //根据ID获得敌人信息
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);



            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;

            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData);//储存敌人信息

            //储存到集合
            enemyList.Add(enemy);



            obj.transform.position =new Vector3(x, y, z);
        }

    }


    public void deleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //是否击杀所有判断
        if (enemyList.Count<=0)
        {
            FightManager.Instance.ChangeType(FightType.Win);


        }


    }


   public IEnumerator DoAllEnemyAction() //执行活着的怪物的行动
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }
        //行动完成后 更新敌人行动信息
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetrandomAction();
        }

        FightManager.Instance.ChangeType(FightType.Player);


    }



}
