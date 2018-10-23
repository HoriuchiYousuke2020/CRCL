using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetB : MonoBehaviour
{
    private int targetValue = 0;
    private Vector3 targetPosition;
    private STATE state;
    enum STATE
    {
        NORMAL,
        TARGET,
    }

    public GameObject[] player = new GameObject[4];
    // Use this for initialization
    void Start()
    {
        state = STATE.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case STATE.NORMAL: NormalCamera(); break;
            case STATE.TARGET: TargetCamera(); break;
        }

    }

    /// <summary>
    /// 通常の動作
    /// 一番上にいるプレイヤーに追従する
    /// </summary>
    void NormalCamera()
    {
        //ｙ座標が高いオブジェクトの座標を取得
        for (int i = 0; i < 4; i++)
        {
            if (player[i].transform.position.y > player[targetValue].transform.position.y)
            {
                targetValue = i;
            }
        }

        float distanceX = player[targetValue].transform.position.x - this.transform.position.x;
        float distanceY = player[targetValue].transform.position.y - this.transform.position.y;
        this.transform.position = new Vector3(this.transform.position.x + (distanceX / 10), this.transform.position.y + (distanceY / 10), -10);
    }

    /// <summary>
    /// ターゲット指定
    /// targetPositionを追い続けるといいなー
    /// </summary>
    void TargetCamera()
    {
        targetPosition = player[0].transform.position;
        this.transform.position = targetPosition;

        float distanceX = targetPosition.x - this.transform.position.x;
        float distanceY = targetPosition.y - this.transform.position.y;
        this.transform.position = new Vector3(this.transform.position.x + (distanceX / 10), this.transform.position.y + (distanceY / 10), -10);

    }


}
