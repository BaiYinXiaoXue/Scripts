using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {

        if (TryUse() == true)
        {


            //�Ƿ��п���
            // int val = int.Parse(data["Arg0"]);
            //int val = Random.Range(0, 5);
            int val = 4;
            
            UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").Draw_Card(val);

            Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));

            playEffect(a);

            UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").Discard(Card_State_List_Reference_id);

        }
        else { base.OnEndDrag(eventData); }
        
    }








}
