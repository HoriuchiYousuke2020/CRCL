using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private int targetValue = 0;
    private STATE state;
    public Vector3 cameraPos;

    enum STATE
    {
        NORMAL,
        TARGET,
    }

    public GameObject[] player ;
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
        }

    }

    /// <summary>
    /// 通常の動作
    /// 一番上にいるプレイヤーに追従する
    /// </summary>
    void NormalCamera()
    {
        //ｙ座標が高いオブジェクトの座標を取得
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].transform.position.y > player[targetValue].transform.position.y)
            {
                targetValue = i;
            }
        }

        float distanceX = player[targetValue].transform.position.x - this.transform.position.x;
        float distanceY = player[targetValue].transform.position.y - this.transform.position.y;
        this.transform.position = new Vector3(cameraPos.x + this.transform.position.x + (distanceX / 10), this.transform.position.y + (distanceY / 10), cameraPos.z);
    }
}
