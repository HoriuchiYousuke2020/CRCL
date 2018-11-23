using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitPoint : MonoBehaviour
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
        this.GetComponent<Image>().fillAmount = 0.5f * (player.GetComponent<PlayerController>().GetStatus().GetHp()-1);
    }
}
