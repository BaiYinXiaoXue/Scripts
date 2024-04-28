using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;



public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public Dictionary<string, string> data;//卡牌信息

public void Init(Dictionary<string,string> data)
    {
        this.data = data;



    }

    private int index;
   





    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();


        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor",Color.green);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);


    }

    public void OnPointerExit(PointerEventData eventData)
    {

        transform.DOScale(1f, 0.25f);
        transform.SetSiblingIndex(index);


        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);

    }

    private void Start()//卡牌初始化
    {

        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);

        transform.Find("bg/icon").GetComponent<Image>().sprite= Resources.Load<Sprite>(data["Icon"]);

        transform.Find("bg/msgTxt").GetComponent<Text>().text = data["Des"];

        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];

        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];

        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];



        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));

    }


    Vector2 initPos;//拖拽前位置
    //拖拽
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;
        AudioManager.Instance.playEffect("en1");

    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos
            
            ))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;

        }




    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }





    public virtual bool TryUse() {

       
            int cost = int.Parse(data["Expend"]);

            if (cost > FightManager.Instance.CurPowerCount)
            {

                AudioManager.Instance.playEffect("lose");

                UIManager.instance.ShowTip("费用不足", Color.red);
                return false;
            }
            else
            {

                FightManager.Instance.CurPowerCount -= cost;
                UIManager.instance.GetUI<FightUI>("FightUI").UpdatePower();

                UIManager.instance.GetUI<FightUI>("FightUI").RemoveCard(this);


                return true;



            }
        

    }
    public void playEffect(Vector3 pos)//卡牌使用后的特效
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"]))as GameObject;
        effectObj.transform.position = pos;
        Destroy(effectObj, 2);


    }

}
