using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitPoint2 : MonoBehaviour
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
        Debug.Log(player.GetComponent<PlayerController>().GetStatus().GetHp());
        this.GetComponent<Image>().fillAmount = player.GetComponent<PlayerController>().GetStatus().GetHp();

    }
}
