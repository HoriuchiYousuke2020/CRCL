///2018. 10/21
///writer name Sato Momoya
///ItemObjectClass
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour {
    public int m_itemNumber;
    private Item m_objectItem;
	// Use this for initialization
	void Start ()
    {
      
	    switch(m_itemNumber)
        {
            case 0: m_objectItem = gameObject.AddComponent<Item>(); break;
            //ここより下にバフ効果を受けるアイテムを書く
            case 1: m_objectItem = gameObject.AddComponent<SpeedUp>(); break;
            case 2: m_objectItem = gameObject.AddComponent<AttackUp>(); break;
            case 3: m_objectItem = gameObject.AddComponent<JumpUp>(); break;
            case 4: m_objectItem = gameObject.AddComponent<HpUp>(); break;
            case 5: m_objectItem = gameObject.AddComponent<DefenceUp>(); break;
            case 6: m_objectItem = gameObject.AddComponent<ScoreUp>(); break;
            case 7: m_objectItem = gameObject.AddComponent<Replaceall>(); break;
            case 8: m_objectItem = gameObject.AddComponent<ReplaceFirstPlace>(); break;
            ///ここより下にデバフ効果を受けるアイテムを書く
            case -1: m_objectItem = gameObject.AddComponent<SpeedDown>(); break;
            case -2: m_objectItem = gameObject.AddComponent<AttackDown>(); break;
            case -3: m_objectItem = gameObject.AddComponent<JumpDown>(); break;
            case -4: m_objectItem = gameObject.AddComponent<HpDown>(); break;
            case -5: m_objectItem = gameObject.AddComponent<DefenceDown>(); break;
            case -6: m_objectItem = gameObject.AddComponent<ScoreDown>(); break;

            //登録外のアイテムに触れた場合
            default: Debug.Log("未知のアイテムが存在しています登録されているアイテム番号を確認してください"); break;
        }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter(Collision c)
    {

        if (c.transform.tag == "Player")
        {

            c.transform.GetComponent<PlayerController>().SetItem(this.m_objectItem);
            c.transform.GetComponent<PlayerController>().SetNowItemNumber(m_itemNumber);
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
