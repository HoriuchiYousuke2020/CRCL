using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replaceall : Item
{
    public Replaceall()
    {
        m_itemNumber = 7;
        m_itemName = "シャッフル";
    }

    private void Start()
    {
      
    }

    public override void UseItem(PlayerController player)
    {
        //全員の位置を入れ替える    
        player.GetItem().SetScrollsFlag((int)SCROLLS_STATE.REPLACE_All, true);
    }

}

public class ReplaceFirstPlace : Item
{
  public  ReplaceFirstPlace()
    {
        m_itemNumber = 8;
        m_itemName = "トップチェンジ";
    }

    private void Start()
    {
       
    }

    public override void UseItem(PlayerController player)
    {
        //全員の位置を入れ替える    
        player.GetItem().SetScrollsFlag((int)SCROLLS_STATE.REPLACEA_FIRSTPLACE, true);
    }

}