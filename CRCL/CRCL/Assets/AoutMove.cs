using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoutMove : MonoBehaviour
{
    int ran;
    int time;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        InitRand();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AddTime();
        if(IsJump())
        {
            Jump();
        }
    }

    void Move()
    {
        rb.velocity = new Vector3(3.0f, rb.velocity.y, 0);
    }

    void InitRand()
    {
        ran = Random.Range(120, 180);
    }

    void AddTime()
    {
        time++;
    }

    void ResetTime()
    {
        time = 0;
    }
    bool IsJump()
    {
        if (time > ran)
        {
            InitRand();
            ResetTime();
            return true;
        }
        return false;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * 300);
    }
}
