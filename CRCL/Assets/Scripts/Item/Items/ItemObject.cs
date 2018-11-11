///2018. 10/21
///writer name Sato Momoya
///ItemObjectClass
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour {

    [SerializeField]
     const int MAX_ITEM_NUM = 8;
    enum ItemType
    {
        IDENTITY,
        RANDOM,


    }

    [SerializeField]
    private ItemType m_itemType;
    [SerializeField]
    private int m_itemNumber;

    private Item m_objectItem;
	// Use this for initialization
	void Start ()
    {
      
	
	}
	
	// Update is called once per frame
	void Update ()
    {
       if(m_itemType == ItemType.IDENTITY)
        {

        }

       if(m_itemType == ItemType.RANDOM)
        {
            m_itemNumber = Random.Range(1, MAX_ITEM_NUM);
        }
    }

    private void OnCollisionEnter(Collision c)
    {

        if (c.transform.tag == "Player")
        {
            switch (m_itemNumber)
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
