using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefendCard : CardItem
{


    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse()==true)
        {
            int val = int.Parse(data["Arg0"]);
            AudioManager.Instance.playEffect("healspell");
            FightManager.Instance.DefenseCount += val;
            UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").UpdateDef();

            Vector3 pos = Camera.main.transform.position;
            pos.y = 0;
            playEffect(pos);

            UIManager.instance.GetUI<Combat_UI_Data>("Combat_UI_Data").Discard(Card_State_List_Reference_id);
            
        }
        else { base.OnEndDrag(eventData); }




       
    }




}
