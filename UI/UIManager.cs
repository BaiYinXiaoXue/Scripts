using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private Transform canvasTf;

    private List<UIBase> uiList;

    private void Awake()
    {
        instance = this;
        canvasTf = GameObject.Find("Canvas").transform;
        uiList = new List<UIBase>();
    }

    public UIBase ShowUI<T>(string uiName) where T :UIBase
        {
        UIBase ui = Find(uiName);
        if (ui==null) {
            GameObject obj =Instantiate(Resources.Load("UI/" + uiName), canvasTf) as GameObject;

            obj.name = uiName;


            ui = obj.AddComponent<T>();

            uiList.Add(ui);

            }
        else
        {
            ui.Show();


        }
        return ui;
    
    
    
    
    }


    private void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui!=null) { ui.Hide(); }



    }

    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null) {
            uiList.Remove(ui);
            Destroy(ui.gameObject); }


    }

    public void CloseAllUI()
    {
        for (int a = uiList.Count-1; a >=0; a--)
        {
            Destroy(uiList[a].gameObject);   
        }

        uiList.Clear();


    }








    public UIBase Find(string uiName)
    {
        for (int i = 0; i < uiList.Count; i++)
        {

            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }



            
        }

        return null;
    }


    public T GetUI<T>(string uiName) where T : UIBase//获得某个界面的脚本
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            return ui.GetComponent<T>();


        }
        return null;
    }
    









    public GameObject createActionIcon()//行动图标
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"),canvasTf)as GameObject;
        obj.transform.SetAsFirstSibling();//在父集的一位
        return obj;


    }


    public GameObject createHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTf) as GameObject;
        obj.transform.SetAsFirstSibling();//在父集的一位
        return obj;


    }




    public void ShowTip(string msg,Color color,System.Action callback=null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTf) as GameObject;
        Text text=obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("bg").DOScaleY(1, 0.5f);
        Tween scale2 = obj.transform.Find("bg").DOScaleY(0, 0.5f);

        DG.Tweening.Sequence seq = DOTween.Sequence();

        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();


            }

            MonoBehaviour.Destroy(obj,1.2f);

        });




    }




}
