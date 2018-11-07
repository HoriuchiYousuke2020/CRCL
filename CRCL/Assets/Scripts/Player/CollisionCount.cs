using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCount : MonoBehaviour {


    //衝突判定用フラグ
    public int[] collisionCount = new int[(int)DIRECTION.MAX];
    public int[] collisionWallCount = new int[(int)DIRECTION.MAX];
    private Dictionary<int, DIRECTION> collisionDataMap = new Dictionary<int, DIRECTION>();
    private bool isPress;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < (int)DIRECTION.MAX; i++)
        {
            collisionCount[i] = 0;
            collisionWallCount[i] = 0;
        }
        collisionDataMap.Clear();
        isPress = false;
    }

    public DIRECTION GetDirection(GameObject obj)
    {
        int key = obj.GetHashCode();
        return collisionDataMap[key];
    }

    void OnCollisionEnter(Collision c)
    {
        ContactPoint point = c.contacts[0];


        //アイテムかコインの時は処理を行わない
        string tag = c.transform.tag; 
        if(tag == "Item" || tag == "Coin")
        {
            return;
        }

        //衝突方向によってフラグを設定する
        DIRECTION dir;
        Debug.Log(point.normal);
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

        //壁との処理
        if (tag == "StageObject")
        {
            collisionWallCount[(int)dir] += 1;
            //挟まった時の処理
            if (collisionWallCount[(int)DIRECTION.UP] > 0 && collisionWallCount[(int)DIRECTION.DOWN] > 0 ||
                collisionWallCount[(int)DIRECTION.RIGHT] > 0 && collisionWallCount[(int)DIRECTION.LEFT] > 0)
            {
                isPress = true;
            }
        }

        collisionDataMap.Add(c.gameObject.GetHashCode(), dir);
    }

    void OnCollisionExit(Collision c)
    {
        Debug.Log("~"+c.gameObject.name + c.gameObject.GetHashCode().ToString() + ":" + collisionDataMap.Count);
       
        //アイテムかコインの時は処理を行わない
        string tag = c.transform.tag;
        if (tag == "Item" || tag == "Coin" )
        {
            return;
        }
        int key = c.gameObject.GetHashCode();


        DIRECTION dir = collisionDataMap[key];
        //衝突方向によってフラグを設定する
        switch (dir)
        {
            case DIRECTION.UP: collisionCount[(int)DIRECTION.UP] -= 1; break;
            case DIRECTION.DOWN: collisionCount[(int)DIRECTION.DOWN] -= 1; break;
            case DIRECTION.RIGHT: collisionCount[(int)DIRECTION.RIGHT] -= 1; break;
            case DIRECTION.LEFT: collisionCount[(int)DIRECTION.LEFT] -= 1; break;
            default:
                Debug.Log("error!");
                break;
        }
        //壁との処理
        if (tag == "StageObject")
        {
            collisionWallCount[(int)dir] -= 1;
        }
        collisionDataMap.Remove(key);

    }

    public bool GetIsPress()
    {
        return isPress;
    }

    public void SetIsPress(bool flag)
    {
        isPress = flag;
    }

    public enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        MAX
    }


}
