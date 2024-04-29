using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;



public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public int Card_State_List_Reference_id;
    public Dictionary<string, string> data;


    public void Init(Dictionary<string,string> data,int id)
    {
        this.data = data;

        Card_State_List_Reference_id=id;


    }

    private int index;
   





    //������
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

    private void Start()//���Ƴ�ʼ��
    {

        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);

        transform.Find("bg/icon").GetComponent<Image>().sprite= Resources.Load<Sprite>(data["Icon"]);

        transform.Find("bg/msgTxt").GetComponent<Text>().text = data["Des"];

        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];

        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];

        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];



        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));

    }


    Vector2 initPos;//��קǰλ��
    //��ק
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

                UIManager.instance.ShowTip("���ò���", Color.red);
                return false;
            }
            else
            {

                FightManager.Instance.CurPowerCount -= cost;
                UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").UpdatePower();

                return true;



            }
        

    }
    public void playEffect(Vector3 pos)//����ʹ�ú����Ч
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"]))as GameObject;
        effectObj.transform.position = pos;
        Destroy(effectObj, 2);


    }

}
