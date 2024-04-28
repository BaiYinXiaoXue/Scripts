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


            // «∑Ò”–ø®≥È
            // int val = int.Parse(data["Arg0"]);
            int val = Random.Range(0, 4);

            if (FightCardManager.Instance.hascard()==true) {

                UIManager.instance.GetUI<FightUI>("FightUI").createCardItem(val);
                UIManager.instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

                Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));

                playEffect(a);
                 
            }
            else
            {

                UIManager.instance.GetUI<FightUI>("FightUI").createCardItem(val);
                

            }



            
        }
        
    }








}
