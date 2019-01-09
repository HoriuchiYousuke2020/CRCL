using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCameraCollision : MonoBehaviour
{
    public GameObject prefab;
    //上右下左の順
    Vector3[] DIRECTION = 
        {
        new Vector3(0, 1, 0),
        new Vector3(1, 0, 0),
        new Vector3(0, -1, 0),
        new Vector3(-1, 0, 0)
    };


    // Use this for initialization
    void Start()
    {

        Vector3 position = this.transform.position;

        for (int i=0;i<4;i++)
        {
            Vector3 Iposition = Vector3.zero;

            //上下なら
            if (i % 2 == 0)
            {
                //
                Iposition = DIRECTION[i] * 6.0f * (-this.transform.position.z / 10);
                GameObject obj = Instantiate(prefab, Iposition, Quaternion.identity);
                obj.transform.localScale = new Vector3(16 * (-this.transform.position.z / 10), 1, 1);
                obj.transform.parent = this.transform;
            }
            //左右なら
            else
            {
                Iposition = DIRECTION[i] * 8.0f * (-this.transform.position.z / 10);
                GameObject obj = Instantiate(prefab, Iposition, Quaternion.identity);
                obj.transform.localScale = new Vector3(1, 12 * (-this.transform.position.z / 10), 1);
                obj.transform.parent = this.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
