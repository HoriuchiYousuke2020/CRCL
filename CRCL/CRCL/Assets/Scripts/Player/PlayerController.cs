using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;


public class PlayerController : MonoBehaviour
{
    //キャラクター制御
    private PLAYER_STATE state;    //プレイヤーの状態
    private bool faceLeft;         //プレイヤーの向いている向き
    private float stateTime;       //プレイヤーの状態終了までの時間

    private Status m_playerStatus; //プレイヤーのステータス
    public Status m_statusType;    //見本にするステータス   
    private Item m_item;           //アイテム
    private int m_nowItemNumber;   //現在のアイテム番号
    public Score m_score;          //スコア
    bool m_downFlag;
    bool m_outFlag;
    //操作キー
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode JumpKey;
    public KeyCode AttackKey;
    public KeyCode ItemKey;

    [SerializeField]
   private GamePad.Index m_padNum;
   

    [SerializeField]
    private int m_playerNumber;
    //キー入力状態保存用
    private int leftKeyState;
    private int rightKeyState;


    //衝突判定用フラグ
    private CollisionCount col;
    private Collision nowCollision;
    //物理関係
    Rigidbody rb;
    private float gravity = 0.1f;
    private float friction = 0.9f;

    DIRECTION dir;

    GamepadState keyState;
    //一つ前の速度
    Vector3 m_currentVec;
    //一つ前のポジション
    Vector3 m_currentPos;

    const float haba = 1.5f;
    // Use this for initialization
    void Start()
    {
        Debug.unityLogger.logEnabled = false;
        state = PLAYER_STATE.NORMAL;
        faceLeft = true;
        stateTime = 0;
        rb = GetComponent<Rigidbody>();
        leftKeyState = 0;
        rightKeyState = 0;
        col = gameObject.GetComponent<CollisionCount>();

        m_score.SetScore(0);

        m_playerStatus = new Status();
        SetStatus();
        m_item = gameObject.AddComponent<Item>();
        m_nowItemNumber = 0;
        m_outFlag = false;

        m_currentVec = new Vector3(0, 0, 0);
        m_downFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentVec = rb.velocity;
        m_currentPos = transform.position;
        keyState = GamePad.GetState(m_padNum, false);
        if (m_outFlag == true)
        {
            Vector3 pos = Camera.main.transform.position;
            this.transform.position = new Vector3(pos.x, pos.y, 0);
            rb.velocity = Vector3.zero;
            m_outFlag = false;
            ColorChangeA(0.5f);
            state = PLAYER_STATE.PRESSED;
            stateTime = 120.0f;
        }
        Debug.Log("a");
        //左右移動制御用
        GetLRKeyState();
        CheckItemKey();
        //キャラクター制御
        switch (state)
        {
            case PLAYER_STATE.NORMAL: UpdateStateNormal(); break;
            case PLAYER_STATE.ATTACK: UpdateStateAttack(); break;
            case PLAYER_STATE.ITEMUSE: UpdateStateItemUse(); break;
            case PLAYER_STATE.DAMAGED: UpdateStateDamaged(); break;
            case PLAYER_STATE.SWOON: UpdateStateSwoon(); break;
            case PLAYER_STATE.PRESSED: UpdateStatePressed(); break;
            case PLAYER_STATE.GOAL: UpdateStateGoal(); break;
            default:
                Debug.Log(gameObject.name + "の状態が不正です");
                break;
        }

        //衝突判定
        string tag = nowCollision.transform.tag;
        switch (tag)
        {
            case "Player": CollisionPlayer(); break;
            case "Enemy": CollisionEnemy(); break;
            //case "Goal": CollisionGoal(); break;
            case "Item": break;
            default: break;
        }

        //壁判定
        if (col.GetIsPress())
        {
            Pressed();

        }

        //if(Input.GetAxis("joypad1 jump") > 0 )
        //{
        //    rb.AddForce(m_playerStatus.GetjumpPower() * Vector3.up);
        //}



        rb.velocity = new Vector3(rb.velocity.x * friction, rb.velocity.y - gravity * Time.deltaTime, 0);

    }

    //通常状態の処理
    void UpdateStateNormal()
    {
        Move();
        Jump();
        if (this.GetComponent<Renderer>().material.color.a <= 1.0f)
        {
            ColorChangeA(1.0f);
        }
        if (GamePad.GetButtonDown(GamePad.Button.X,m_padNum) || Input.GetKeyDown(AttackKey))
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
            state = PLAYER_STATE.NORMAL;
        }
    }

    //アイテム使用状態の処理
    void UpdateStateItemUse()
    {
        UseItem();
    }
   
    //ダメージを受けた状態の処理
    void UpdateStateDamaged()
    {
        stateTime -= 60.0f * Time.deltaTime;
        if (stateTime <= 0)
        {
            state = PLAYER_STATE.NORMAL;

        }
    }

    //気絶状態の処理
    void UpdateStateSwoon()
    {
        stateTime -= 60.0f * Time.deltaTime;
      
        if (stateTime <= 0)
        {
            m_downFlag = true;
            state = PLAYER_STATE.NORMAL;
            ColorChangeA(1.0f);
            m_playerStatus.SetHp(3);
        }
    }

    //壁に挟まれた状態の処理
    void UpdateStatePressed()
    {
      
        stateTime -= 60.0f * Time.deltaTime;
        if (stateTime <= 0)
        {
            m_downFlag = true;
            state = PLAYER_STATE.NORMAL;
            ColorChangeA(1.0f);
            m_playerStatus.SetHp(3);
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
                if (col.collisionCount[(int)DIRECTION.LEFT] == 0 || col.collisionCount[(int)DIRECTION.DOWN] > 0)
                {
                    rb.velocity = new Vector3(-m_playerStatus.GetSpeed(), rb.velocity.y, 0);
                    faceLeft = true;
                }
                break;
            //右に移動
            case 2:
                if (col.collisionCount[(int)DIRECTION.RIGHT] == 0 || col.collisionCount[(int)DIRECTION.DOWN] > 0)
                {
                    rb.velocity = new Vector3(m_playerStatus.GetSpeed(), rb.velocity.y, 0);
                    faceLeft = false;
                }
                break;
            //例外
            default:
                Debug.Log("プレイヤーの左右移動で不正な値が渡されました");
                break;
        }

       

      
    }

    void UpdateStateGoal()
    {
        m_score.AddScore(1000);
        SceneManager.LoadScene("ResultScene");
    }

    void Jump()
    {
        if ((GamePad.GetButtonDown(GamePad.Button.A,m_padNum)|| Input.GetKeyDown(JumpKey)) && col.collisionCount[(int)DIRECTION.DOWN] > 0)
        {
            rb.AddForce(m_playerStatus.GetjumpPower() * Vector3.up);
            Debug.Log("");
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

    public void Damaged()
    {
       // m_playerStatus.SetHp(m_playerStatus.GetHp() - 1);
        state = PLAYER_STATE.DAMAGED;
        stateTime = 60.0f;
    }

    void Swoon()
    {
        ColorChangeA(0.5f);
        state = PLAYER_STATE.SWOON;
        stateTime = 300.0f;
    }

    void Pressed()
    {
        Vector3 pos = Camera.main.transform.position;
        //リスポーン地点を左右randomに分ける
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            this.transform.position = new Vector3(pos.x - haba, pos.y, 0);
        }
        else
        {
            this.transform.position = new Vector3(pos.x + haba, pos.y, 0);
        }
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        rb.velocity = Vector3.zero;
        col.SetIsPress(false);
        ColorChangeA(0.5f);
        state = PLAYER_STATE.PRESSED;
        stateTime = 120.0f;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag == "Item"|| c.transform.tag == "Coin") return;
        nowCollision = c;
    }

    void OnCollisionExit(Collision c)
    {
        if (c.transform.tag == "Player")
        {
            //float a,b;
            //a = m_currentPos.magnitude - transform.position.magnitude;
            //b = rb.velocity.magnitude;
            //if (a < -2.0f && (Input.GetKey(KeyCode.Space) == false ) && b < 0)
            //{
            //    rb.velocity = new Vector3(0,0,0);
            //}
         
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.transform.tag == "Goal")
        {
            CollisionGoal();
        }
    }

    void GetLRKeyState()
    {
        if (keyState.LeftStickAxis.x < 0 || Input.GetKey(LeftKey))
        {
            leftKeyState++;
        }
        else
        {
            leftKeyState = 0;
        }

        if (keyState.LeftStickAxis.x > 0 || Input.GetKey(RightKey))
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
        if ((GamePad.GetButtonDown(GamePad.Button.B,m_padNum)|| Input.GetKeyDown(ItemKey)) && m_nowItemNumber != 0)
        {
            state = PLAYER_STATE.ITEMUSE;
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
        NORMAL,
        ATTACK,
        ITEMUSE,
        DAMAGED,
        SWOON,
        PRESSED,
        GOAL,
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
            state = PLAYER_STATE.NORMAL;
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

    /// <summary>
    /// アイテムの取得
    /// </summary>
    /// <param name="item">拾ったアイテム</param>
    public void SetItem(Item item)
    {
        m_item = item;
    }

    /// <summary>
    /// Color.aの変更
    /// </summary>
    /// <param name="a">アルファの値</param>
    void ColorChangeA(float a)
    {
        Color color = this.GetComponent<Renderer>().material.color;
        color.a = a;
        this.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// プレイヤ衝突時の処理
    /// </summary>
    void CollisionPlayer()
    {
        //敵が攻撃状態で横から衝突したらダメージを受ける
        dir = (DIRECTION)col.GetDirection(nowCollision.gameObject);
        if (nowCollision.transform.gameObject.GetComponent<PlayerController>().state == PLAYER_STATE.ATTACK &&
            (dir == DIRECTION.LEFT || dir == DIRECTION.RIGHT))
        {
          rb.AddForce(nowCollision.contacts[0].normal * 800.0f);
            if (m_playerStatus.GetHp() >0)
            {
                Debug.Log(m_playerStatus.GetHp());
               m_playerStatus.SetHp(m_playerStatus.GetHp() - 1);
                if (m_playerStatus.GetHp() == 0)
                {
                    Swoon();
                }
                else
                {

                    Damaged();

                }
            }
        }
    }

    /// <summary>
    /// Enemy衝突時の処理
    /// </summary>
    void CollisionEnemy()
    {
        //衝突時の反発
        //if (Mathf.Abs(nowCollision.contacts[0].normal.y) > 0)
        //{
        //    rb.AddForce(nowCollision.contacts[0].normal * 50.0f);
        //}
       /* else*/ if (Mathf.Abs(nowCollision.contacts[0].normal.x) > 0)
        {
            rb.AddForce(nowCollision.contacts[0].normal * 50.0f);
        }

        if (m_playerStatus.GetHp() > 0)
        {
            Debug.Log(m_playerStatus.GetHp());
            m_playerStatus.SetHp(m_playerStatus.GetHp() - 1);
            if (m_playerStatus.GetHp() == 0)
            {
                Swoon();
            }
            else
            {

                Damaged();

            }
        }
    }

    /// <summary>
    /// ゴール衝突時の処理
    /// </summary>
    void CollisionGoal()
    {
        state = PLAYER_STATE.GOAL;
    }

    void OnBecameInvisible()
    {
        m_outFlag = true;
    }

    public Color GetColor()
    {
        Color col = this.GetComponent<Renderer>().material.color;
        col.a = 1;
        return col;
    }
}
