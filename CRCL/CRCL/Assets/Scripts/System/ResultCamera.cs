using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + new Vector3(6, -player.transform.position.y, this.transform.position.z);
    }
}
