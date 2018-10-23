using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerB : MonoBehaviour
{
    private Status m_playerStatus;
    public Status m_statusType;

    private Item m_item;

    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Jump;
    public KeyCode ItemKey;
    public Score m_score;



    //衝突判定用フラグ
    private bool[] collisionFlag = new bool[4];
    Rigidbody rb;
    private float gravity = 0.1f;
    private float friction = 0.9f;
    private bool m_coinFlag;// コインゲットフラグ
    private bool m_downFlag;// ダウンフラグ
    private int  m_nowItemNumber;//現在のアイテム番号
    private bool m_useItem;     //アイテム使用フラグ

    //プレイヤの状態
    enum STATEB
    {
        NORMAL,
        ATTACK,
        ITEMUSE,
        DAMAGE,
        DEAD,

        ANY
    }
    STATEB state = STATEB.NORMAL;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_coinFlag = false;
        m_score.SetScore(0);
        m_playerStatus = new Status();
        SetStatus();
  //      m_playerStatus =  m_statusType;
        m_item = gameObject.AddComponent<Item>();
        m_nowItemNumber = 0;

    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case STATEB.NORMAL:
                Move();
                Jumping();
             
                break;
            case STATEB.ATTACK:break;
            case STATEB.ITEMUSE:
                UseItem();
                break;
            case STATEB.DAMAGE: break;
            case STATEB.DEAD:  break;
        }


        if (m_coinFlag == true)
        {
            m_score.AddScore(100);
            m_coinFlag = false;
        }

        //アイテム番号が0番以下なら強制的に使用する
        if (m_nowItemNumber < 0)
        {
            state = STATEB.ITEMUSE;
        }

        if (Input.GetKeyDown(ItemKey) && m_nowItemNumber != 0)
        {
            state = STATEB.ITEMUSE;

            //  m_item = gameObject.AddComponent<Item>();
        }
            rb.velocity = new Vector3(rb.velocity.x * friction, rb.velocity.y - gravity * Time.deltaTime, 0);
        TestDown();
       
          
        
    }

    void Move()
    {
        if (Input.GetKey(Left))
        {
            rb.velocity = new Vector3(-m_playerStatus.GetSpeed(), rb.velocity.y, 0);
        }
        if (Input.GetKey(Right))
        {
            rb.velocity = new Vector3(m_playerStatus.GetSpeed(), rb.velocity.y, 0);
        }
    }

    void Jumping()
    {
        if (Input.GetKeyDown(Jump))
        {
            rb.AddForce(m_playerStatus.GetjumpPower() * Vector3.up);
        }


    }
    
    void SetStatus()
    {
       
        //ここでステータスをセットする
        m_playerStatus.SetHp(m_statusType.GetHp());
        m_playerStatus.SetPower(m_statusType.GetPower());
        m_playerStatus.SetDefence(m_statusType.GetDefense());
        m_playerStatus.SetSpeed(m_statusType.GetSpeed());
        m_playerStatus.SetJumpPower(m_statusType.GetjumpPower());
    } 
    void TestDown()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_downFlag = true;
        }
    }

    void UseItem()
    {

       // m_item.UseItem(this);
        if(m_item.GetUsedFlag() == true)
        {
            m_item = gameObject.AddComponent<Item>();
            m_nowItemNumber = 0;
            state = STATEB.NORMAL;
        }
      

    }

    void OnCollisionExit(Collision c)
    {
        if(c.transform.tag == "StageObject")
        {
            //お互いの座標からどちらから衝突したかを確認する
            Vector3 distance = c.transform.position - this.transform.position;
            float angle = Mathf.Atan2(distance.y, distance.x);
            angle = angle * Mathf.Rad2Deg;
         
            if (angle < 0)
            {
                collisionFlag[(int)DIRECTION.UP] = false;
            }
            else
            {
                collisionFlag[(int)DIRECTION.DOWN] = false;
            }
        }


    }

    void OnCollisionEnter(Collision c)
    {
        if(c.transform.tag == "StageObject")
        {
            //お互いの座標からどちらから衝突したかを確認する
            Vector3 distance = c.transform.position - this.transform.position;
            float angle = Mathf.Atan2(distance.y, distance.x);
            angle = angle * Mathf.Rad2Deg;
            Debug.Log(c.contacts[0].normal);
           // Debug.Log(angle);
            if(angle < 0)
            {
                collisionFlag[(int)DIRECTION.UP] = true;
            }
            else
            {
                collisionFlag[(int)DIRECTION.DOWN] = true;
            }
            //挟まった時の処理
            if (collisionFlag[(int)DIRECTION.UP] && collisionFlag[(int)DIRECTION.DOWN])
            {
                Vector3 pos = Camera.main.transform.position;
                this.transform.position = new Vector3(pos.x, pos.y, 0);
                rb.velocity = Vector3.zero;
                for (int i = 0; i < 4; i++)
                {
                    collisionFlag[i] = false;
                }
            }
          
           
           
        }
        ////////////////////////////////////
        if(c.transform.tag == "Enemy")  //もし敵と当たったら   
        {

        }

        ////////////////////////////////////
        if (c.transform.tag == "Coin") //もしコインと当たったら
        {
            m_coinFlag = true;
        }

        if(c.transform.tag == "Item") //もしアイテムと当たったら
        {
            //アイテム番号を調べそれにあったアイテムを取得する
            switch (c.transform.GetComponent<ItemObject>().GetItemNumber())
            {
                case 0: m_nowItemNumber =  c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<Item>(); break;
                //ここより下にバフ効果を受けるアイテムを書く
                case 1: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<SpeedUp>();break;
                case 2: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<AttackUp>(); break;
                case 3: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<JumpUp>(); break;
                case 4: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<HpUp>(); break;
                case 5: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<DefenceUp>(); break;
                case 6: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<ScoreUp>(); break;
                case 7: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<Replaceall>(); break;
                case 8: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<ReplaceFirstPlace>(); break;
                ///ここより下にデバフ効果を受けるアイテムを書く
                case -1: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<SpeedDown>(); break;
                case -2: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<AttackDown>(); break;
                case -3: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<JumpDown>(); break;
                case -4: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<HpDown>(); break;
                case -5: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<DefenceDown>(); break;
                case -6: m_nowItemNumber = c.transform.GetComponent<ItemObject>().GetItemNumber(); m_item = gameObject.AddComponent<ScoreDown>(); break;

                //登録外のアイテムに触れた場合
                default: Debug.Log("未知のアイテムに触れました。登録されているアイテム番号を確認してください");  break;

            }

        }
        
    }

    enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        MAX
    }

public  Score GetScore()    //スコアを取得する
    {
        return m_score;
    }

public Status GetStatus()
    {
        return m_playerStatus;
    }

public bool GetDownFlag()   //ダウンフラグを取得
    {
        return m_downFlag;
    }

public void SetDownFlag(bool flag)// ダウンフラグをセット
    {
        m_downFlag = flag;
    }


public Item GetItem()
    {
        return m_item;
    }

}
