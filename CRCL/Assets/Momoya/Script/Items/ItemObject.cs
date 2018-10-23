///2018. 10/21
///writer name Sato Momoya
///ItemObjectClass
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour {
    public int m_itemNumber;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter(Collision  c)
    {
       
            if (c.transform.tag == "Player")
            {
                Destroy(gameObject);
            }
        
    }

    public int GetItemNumber() //アイテム番号をゲットする
    {
        return m_itemNumber;
    }

    public void SetItemNumber(int itemnumber)
    {
        //もしアイテム自体をゲーム中に変更したい場合使うかも
        m_itemNumber = itemnumber;
    }
}
