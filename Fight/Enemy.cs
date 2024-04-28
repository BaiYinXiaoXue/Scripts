using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//敌人行动
public enum ActionType{
    None,
    Defend,
    Attack,
    DefendAndAttack,
    Unkonwn,
    Strengthen,
    AttackAndStrengthen,
    Magic,
    Strategy,
    AttackAndStrategy,
    Sleep,
    DefendAndStrengthen,
    Debuff,
    Stun,
    Escape,
    Special,
    Placeholder

}

public class Enemy : MonoBehaviour
{

    protected Dictionary<string, string> data;

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;
    public Transform attackTf;
    public Transform defendTf;



    public Text HpTxt;
    public Text DefTxt;
    public Image HpImg;



    public int Defend;
    public int Attack;
    public int CurHp;
    public int MaxHp;


    public Animator ani;


    //meshre
    SkinnedMeshRenderer _meshRenderer;


    public void Init(Dictionary<string, string> data)
    {
        this.data = data;


    }





    // Start is called before the first frame update
    void Start()
    {

        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();

        ani = transform.GetComponentInChildren<Animator>();



        type = ActionType.None;
        hpItemObj = UIManager.instance.createHpItem();
        actionObj = UIManager.instance.createActionIcon();


        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position+Vector3.down*0.3f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);


        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");


        DefTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        HpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        HpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        SetrandomAction();

        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);





        UpdateDefend();
        UpdateHp();


        

    }
    
    public void SetrandomAction()
    {
        int ran = Random.Range(1, 3);


        type = (ActionType)ran;


        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);

                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);

                break;
            case ActionType.DefendAndAttack:
                break;
            case ActionType.Unkonwn:
                break;
            case ActionType.Strengthen:
                break;
            case ActionType.AttackAndStrengthen:
                break;
            case ActionType.Magic:
                break;
            case ActionType.Strategy:
                break;
            case ActionType.AttackAndStrategy:
                break;
            case ActionType.Sleep:
                break;
            case ActionType.DefendAndStrengthen:
                break;
            case ActionType.Debuff:
                break;
            case ActionType.Stun:
                break;
            case ActionType.Escape:
                break;
            case ActionType.Special:
                break;
            case ActionType.Placeholder:
                break;
            
        }










    }








    public void UpdateHp()
    {
        HpTxt.text = CurHp + "/" +MaxHp;
        HpImg.fillAmount = (float)CurHp / (float)MaxHp;

    }


    public void UpdateDefend()
    {
        DefTxt.text = Defend.ToString();
    }


    public void OnSelect()//被卡选中，显示边框
    {
        _meshRenderer.material.SetColor("_OtlColor",Color.red);



    }

    public void OnUnSelect()//被卡选中，显示边框
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.black);



    }


    public void Hit(int val)//受伤
    {
        //先掉甲
        if (Defend >= val)
        {
            Defend -= val;

            //播放受伤
            ani.Play("hit",0,0);

        }
        else
        {
            val -= Defend;
            Defend = 0;
            CurHp -= val;

            if (CurHp<=0)
            {
                CurHp = 0;
                ani.Play("die");

                EnemyBaseManager.Instance.deleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                ani.Play("hit");


            }


        }
        UpdateDefend();
        UpdateHp();





    }



    public void hideAction()//图标隐藏
    {


        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);

    }
    public IEnumerator DoAction()//敌人行动
    {
        hideAction();
        //播放动画，默认攻击,excel可以配置
        ani.Play("attack");
        yield return new WaitForSeconds(0.5f);//等待配置的时间


        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                Defend += 1;
                UpdateDefend();
                //可以播放对应特效

                break;
            case ActionType.Attack:
                FightManager.Instance.getPlayHit(Attack);


                Camera.main.DOShakePosition(0.1f,0.2f, 5,27);

                break;
            case ActionType.DefendAndAttack:



                break;
            case ActionType.Unkonwn:
                break;
            case ActionType.Strengthen:
                break;
            case ActionType.AttackAndStrengthen:
                break;
            case ActionType.Magic:
                break;
            case ActionType.Strategy:
                break;
            case ActionType.AttackAndStrategy:
                break;
            case ActionType.Sleep:
                break;
            case ActionType.DefendAndStrengthen:
                break;
            case ActionType.Debuff:
                break;
            case ActionType.Stun:
                break;
            case ActionType.Escape:
                break;
            case ActionType.Special:
                break;
            case ActionType.Placeholder:
                break;
            default:
                break;
        }

        //等待动画播放结束
        yield return new WaitForSeconds(0.7f);
        ani.Play("idle");


    }

}
