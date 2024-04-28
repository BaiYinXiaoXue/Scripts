using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class AttackCardItem : CardItem, IPointerDownHandler
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
        AudioManager.Instance.playEffect("en1");

        UIManager.instance.ShowUI<LineUI>("LineUI");


        //开始位置
       UIManager.instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);///////



        Cursor.visible = false;//关鼠标
        StopAllCoroutines();//关闭所有协同程序
        //UIManager.instance.CloseUI("LineUI");
        //启用协同程序
        StartCoroutine(OnMouseDownRight(eventData));
    }

    IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {//按下鼠标右键 跳出循环

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
                //结束位置
                UIManager.instance.GetUI<LineUI>("LineUI").SetEndPos(pos);///////////////////

                //射线检测
                CheckRayToEnemy();



            }


            yield return null;


        }
        //跳出循环，显示鼠标
        Cursor.visible = true;
        UIManager.instance.CloseUI("LineUI");//关闭UI
    }
    Enemy hitEnemy;//射线检测到敌人的脚本

    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray,out hit,10000,LayerMask.GetMask("Enemy")  )){

            hitEnemy = hit.transform.GetComponent<Enemy>();

            hitEnemy.OnSelect();//选中
            if (Input.GetMouseButton(0))//按下左键使用
            {
                StopAllCoroutines();

                Cursor.visible = true;
                UIManager.instance.CloseUI("LineUI");//关闭UI

                if (TryUse()==true)
                {
                    //播放音效&vfx
                    playEffect(hitEnemy.transform.position);


                    //受伤
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);

                }
                //未选择时
                hitEnemy.OnUnSelect();

                hitEnemy = null;//设置敌人脚本为null

            }





        }
        else
        {
            //未射到怪物
            if (hitEnemy != null)
            {

                hitEnemy.OnUnSelect();
                hitEnemy = null;

            }

        }



    }



}
