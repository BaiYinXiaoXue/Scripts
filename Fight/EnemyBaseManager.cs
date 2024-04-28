using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseManager 
{


    public static EnemyBaseManager Instance=new EnemyBaseManager();

    private List<Enemy> enemyList;//����ս���еĵ���

    /// <summary>
    /// ���ص���
    /// </summary>
    /// <param name="id">�ؿ�ID</param>
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



            //��������
            
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);


            //����ID��õ�����Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);



            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;

            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData);//���������Ϣ

            //���浽����
            enemyList.Add(enemy);



            obj.transform.position =new Vector3(x, y, z);
        }

    }


    public void deleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //�Ƿ��ɱ�����ж�
        if (enemyList.Count<=0)
        {
            FightManager.Instance.ChangeType(FightType.Win);


        }


    }


   public IEnumerator DoAllEnemyAction() //ִ�л��ŵĹ�����ж�
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }
        //�ж���ɺ� ���µ����ж���Ϣ
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetrandomAction();
        }

        FightManager.Instance.ChangeType(FightType.Player);


    }



}
