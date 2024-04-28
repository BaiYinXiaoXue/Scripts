using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Kick_Card : CardItem, IPointerDownHandler
{


    public override void OnBeginDrag(PointerEventData eventData)
    {





    }


    public override void OnDrag(PointerEventData eventData)
    {



    }


    public override void OnEndDrag(PointerEventData eventData)
    {





    }


    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.playEffect("sword");

        UIManager.instance.ShowUI<LineUI>("LineUI");


        //��ʼλ��
        UIManager.instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);///////



        Cursor.visible = false;//�����
        StopAllCoroutines();//�ر�����Эͬ����
        //UIManager.instance.CloseUI("LineUI");
        //����Эͬ����
        StartCoroutine(OnMouseDownRight(eventData));
    }

    IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {//��������Ҽ� ����ѭ��

            if (Input.GetMouseButton(1))
            {
                break;


            }
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                pData.position,
                pData.pressEventCamera,
                out pos
                ))
            {
                //����λ��
                UIManager.instance.GetUI<LineUI>("LineUI").SetEndPos(pos);///////////////////

                //���߼��
                CheckRayToEnemy();



            }


            yield return null;


        }
        //����ѭ������ʾ���
        Cursor.visible = true;
        UIManager.instance.CloseUI("LineUI");//�ر�UI
    }
    Enemy hitEnemy;//���߼�⵽���˵Ľű�

    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {

            hitEnemy = hit.transform.GetComponent<Enemy>();

            hitEnemy.OnSelect();//ѡ��
            if (Input.GetMouseButton(0))//�������ʹ��
            {
                StopAllCoroutines();

                Cursor.visible = true;
                UIManager.instance.CloseUI("LineUI");//�ر�UI

                if (TryUse() == true)
                {
                    //������Ч&vfx
                    playEffect(hitEnemy.transform.position);


                    //����
                    int val = Random.Range(2, 7);
                    hitEnemy.Hit(val);

                    if (FightCardManager.Instance.hascard() == true)
                    {

                        UIManager.instance.GetUI<FightUI>("FightUI").createCardItem(1);
                        UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

                        Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));

                        playEffect(a);
                        val = Random.Range(2, 7);
                        UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardCount();
                        UIManager.instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();
                    }

                    FightManager.Instance.DefenseCount += val;
                    UIManager.instance.GetUI<FightUI>("FightUI").UpdateDef();



                }
                //δѡ��ʱ
                hitEnemy.OnUnSelect();

                hitEnemy = null;//���õ��˽ű�Ϊnull

            }





        }
        else
        {
            //δ�䵽����
            if (hitEnemy != null)
            {

                hitEnemy.OnUnSelect();
                hitEnemy = null;

            }

        }



    }





}


















