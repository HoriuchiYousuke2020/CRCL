using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    //キャラクター制御
    private PLAYER_STATE state;
    private bool faceLeft;
    private float stateTime;

    private Status m_playerStatus; //プレイヤーのステータス
    public Status m_statusType;    //見本にするステータス   
    private Item m_item;           //アイテム
    private int m_nowItemNumber;//現在のアイテム番号
    public Score m_score;       //スコア
    bool m_downFlag;
    //操作キー
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode JumpKey;
    public KeyCode AttackKey;
    public KeyCode ItemKey;

    //キー入力状態保存用
    private int leftKeyState;
    private int rightKeyState;


    //衝突判定用フラグ
    private int[] collisionCount                        = new int[(int)DIRECTION.MAX];
    private int[] collisionWallCount                    = new int[(int)DIRECTION.MAX];
    private Dictionary<int, DIRECTION> collisionDataMap = new Dictionary<int, DIRECTION>();

    //物理関係
    Rigidbody rb;
    private float gravity = 0.1f;
    private float friction = 0.9f;

    DIRECTION dir;

    // Use this for initialization
    void Start()
    {
        state = PLAYER_STATE.DEFAULT;
        faceLeft = true;
        stateTime = 0;
        rb = GetComponent<Rigidbody>();
        leftKeyState = 0;
        rightKeyState = 0;
        for (int i = 0; i < (int)DIRECTION.MAX; i++)
        {
            collisionCount[i] = 0;
            collisionWallCount[i] = 0;
        }
        collisionDataMap.Clear();

        m_score.SetScore(0);
        
        m_playerStatus = new Status();
        SetStatus();
        m_item = gameObject.AddComponent<Item>();
        m_nowItemNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //左右移動制御用
        GetLRKeyState();
        CheckItemKey();
        //キャラクター制御
        switch (state)
        {
            case PLAYER_STATE.DEFAULT: UpdateStateDefault(); break;
            case PLAYER_STATE.ATTACK:  UpdateStateAttack();  break;
            case PLAYER_STATE.ITEM:    UpdateStateItem();    break;
            case PLAYER_STATE.SWOON:   UpdateStateSwoon();   break;
            default:
                Debug.Log("error!");
                break;
        }
       
        rb.velocity = new Vector3(rb.velocity.x * friction, rb.velocity.y - gravity * Time.deltaTime, 0);
    }

    //通常状態の処理
    void UpdateStateDefault()
    {
        Move();
        Jump();

        if (Input.GetKeyDown(AttackKey))
        {
            Attack();
        }
    }

    //攻撃状態の処理
    void UpdateStateAttack()
    {
        stateTime -= 60.0f * Time.deltaTime;
        if (stateTime <= 0)
        {
            state = PLAYER_STATE.DEFAULT;
        }
    }

    //アイテム使用状態の処理
    void UpdateStateItem()
    {
        UseItem();
    }

    //気絶状態の処理
    void UpdateStateSwoon()
    {
        stateTime -= 60.0f * Time.deltaTime;
        if (stateTime <= 0)
        {
            state = PLAYER_STATE.DEFAULT;
            m_playerStatus.SetHp(3);
            Color color = this.GetComponent<Renderer>().material.color;
            color.a = 1.0f;
            this.GetComponent<Renderer>().material.color = color;
        }
    }

    void Move()
    {
        
        switch (CheckStateLRKeyLater())
        {
            //左右に移動していない
            case 0:
                break;
            //左に移動
            case 1:
                if (collisionCount[(int)DIRECTION.LEFT] == 0 || collisionCount[(int)DIRECTION.DOWN] > 0)
                {
                    rb.velocity = new Vector3(-m_playerStatus.GetSpeed(), rb.velocity.y, 0);
                    faceLeft = true;
                }
                break;
            //右に移動
            case 2:
                if (collisionCount[(int)DIRECTION.RIGHT] == 0 || collisionCount[(int)DIRECTION.DOWN] > 0)
                {
                    rb.velocity = new Vector3(m_playerStatus.GetSpeed(), rb.velocity.y, 0);
                    faceLeft = false;
                }
                break;
            //例外
            default:
                Debug.Log("error!");
                break;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(JumpKey) && collisionCount[(int)DIRECTION.DOWN] > 0)
        {
            rb.AddForce(m_playerStatus.GetjumpPower() * Vector3.up);
        }
    }

    void Attack()
    {
        state = PLAYER_STATE.ATTACK;
        stateTime = 60.0f;

        if (faceLeft)
        {
            rb.AddForce(1000.0f * Vector3.left);
        }
        else
        {
            rb.AddForce(1000.0f * Vector3.right);
        }
    }

    void Swoon()
    {
        state = PLAYER_STATE.SWOON;
        stateTime = 300.0f;

        Color color = this.GetComponent<Renderer>().material.color;
        color.a = 0.5f;
        this.GetComponent<Renderer>().material.color = color;

    }

    void OnCollisionExit(Collision c)
    {
        //Debug.Log("~"+c.gameObject.name + c.gameObject.GetHashCode().ToString() + ":" + collisionDataMap.Count);

        int key = c.gameObject.GetHashCode();
        DIRECTION dir = collisionDataMap[key];
        //衝突方向によってフラグを設定する
        switch (dir)
        {
            case DIRECTION.UP:    collisionCount[(int)DIRECTION.UP]    -= 1; break;
            case DIRECTION.DOWN:  collisionCount[(int)DIRECTION.DOWN]  -= 1; break;
            case DIRECTION.RIGHT: collisionCount[(int)DIRECTION.RIGHT] -= 1; break;
            case DIRECTION.LEFT:  collisionCount[(int)DIRECTION.LEFT]  -= 1; break;
            default:
                Debug.Log("error!");
                break;
        }
        //壁との処理
        if (c.transform.tag == "StageObject")
        {
            collisionWallCount[(int)dir] -= 1;
        }
        collisionDataMap.Remove(key);
        
    }

    void OnCollisionEnter(Collision c)
    {
        ContactPoint point = c.contacts[0];

        //衝突方向によってフラグを設定する
        // DIRECTION dir;
        if (c.transform.tag != "Item" && c.transform.tag != "Coin")
        {

            if (point.normal.y < 0)
            {
                collisionCount[(int)DIRECTION.UP] += 1;
                dir = DIRECTION.UP;
            }
            else if (point.normal.y > 0)
            {
                collisionCount[(int)DIRECTION.DOWN] += 1;
                dir = DIRECTION.DOWN;
            }
            else if (point.normal.x < 0)
            {
                collisionCount[(int)DIRECTION.RIGHT] += 1;
                dir = DIRECTION.RIGHT;
            }
            else
            {
                collisionCount[(int)DIRECTION.LEFT] += 1;
                dir = DIRECTION.LEFT;
            }
        }
        if (c.transform.tag == "StageObject")
        {
            //壁との処理

            collisionWallCount[(int)dir] += 1;
            //挟まった時の処理
            if (collisionWallCount[(int)DIRECTION.UP] > 0 && collisionWallCount[(int)DIRECTION.DOWN] > 0 ||
                collisionWallCount[(int)DIRECTION.RIGHT] > 0 && collisionWallCount[(int)DIRECTION.LEFT] > 0)
            {
                //Debug.Log("death : " + this.gameObject.name);
                Vector3 pos = Camera.main.transform.position;
                this.transform.position = new Vector3(pos.x, pos.y, 0);
                rb.velocity = Vector3.zero;
                Swoon();
            }
        }
        //プレイヤーとの処理
        else if (c.transform.tag == "Player")
        {
            //敵が攻撃状態で横から衝突したらダメージを受ける
            if(c.transform.gameObject.GetComponent<PlayerController>().state == PLAYER_STATE.ATTACK &&
                (dir == DIRECTION.LEFT || dir == DIRECTION.RIGHT))
            {
                m_playerStatus.SetHp(m_playerStatus.GetHp() - 1);
                if (m_playerStatus.GetHp() == 0)
                {
                    Swoon();
                }
            }
        }

        ////////////////////////////////////
        if (c.transform.tag == "Enemy")  //もし敵と当たったら   
        {

        }

        
        collisionDataMap.Add(c.gameObject.GetHashCode(), dir);
        

    }

    void GetLRKeyState()
    {
        if (Input.GetKey(LeftKey))
        {
            leftKeyState++;
        }
       else
        {
            leftKeyState = 0;
        }
    
        if (Input.GetKey(RightKey))
        {
            rightKeyState++;
        }
        else
        {
            rightKeyState = 0;
        }
    }
    
    void CheckItemKey()
    {
        if(Input.GetKeyDown(ItemKey) && m_nowItemNumber != 0)
        {
            state = PLAYER_STATE.ITEM;
        }
    }


    int CheckStateLRKeyLater()
    {

        //デフォルトは何も押されていない状態
        int later = 0;

        //各キーの入力状態
        int key_state1, key_state2;

        key_state1 = leftKeyState;
        key_state2 = rightKeyState;


        if (key_state1 > 0)
        {   //一番目が押されていたら
            if (key_state2 > 0 && key_state2 < key_state1)
            {       //二番目が後に押されていたら
                later = 2;
            }
            else
            {
                later = 1;
            }
        }
        else if (key_state2 > 0)
        {   //二番目が押されていたら
            later = 2;
        }

        return later;
    }

    enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        MAX
    }

    enum PLAYER_STATE
    {
        DEFAULT,
        ATTACK,
        ITEM,
        SWOON,
        MAX
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

    void UseItem()
    {

        m_item.UseItem(this);
        if (m_item.GetUsedFlag() == true)
        {
            m_item = gameObject.AddComponent<Item>();
            m_nowItemNumber = 0;
            state = PLAYER_STATE.DEFAULT;
        }


    }

 

    public Status GetStatus()
    {
        return m_playerStatus;
    }

    public Item GetItem()
    {
        return m_item;
    }

    public Score GetScore()    //スコアを取得する
    {
        return m_score;
    }

    public bool GetDownFlag()   //ダウンフラグを取得
    {
        return m_downFlag;
    }

    public void SetDownFlag(bool flag)// ダウンフラグをセット
    {
        m_downFlag = flag;
    }

    public int GetNowItemNumber()
    {
        return m_nowItemNumber;
    }

    public void SetNowItemNumber(int item)
    {
        m_nowItemNumber = item;
    }

    public void SetItem(Item item)
    {
        m_item = item;
    }
}
