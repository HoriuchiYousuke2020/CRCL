using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replaceall : Item
{

    private void Start()
    {
        m_itemNumber = 7;
    }

    public override void UseItem(PlayerController player)
    {
        //全員の位置を入れ替える    
        player.GetItem().SetScrollsFlag((int)SCROLLS_STATE.REPLACE_All, true);
    }

}

public class ReplaceFirstPlace : Item
{

    private void Start()
    {
        m_itemNumber = 8;
    }

    public override void UseItem(PlayerController player)
    {
        //全員の位置を入れ替える    
        player.GetItem().SetScrollsFlag((int)SCROLLS_STATE.REPLACEA_FIRSTPLACE, true);
    }

}