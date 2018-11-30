using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisPlayItem : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_player;

    [SerializeField]
    private int PlayerNumber;

    // Use this for initialization
    void Start()
    {
        if (PlayerNumber > Setting.PlayerNum)
        {
            this.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.GetComponent<TextMesh>().text = m_player.GetItem().GetName();
    }
}
