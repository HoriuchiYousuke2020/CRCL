using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisPlayItem : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_player;

    [SerializeField]
    private int PlayerNumber;

    private Text m_text;
    // Use this for initialization
    void Start()
    {
        if (PlayerNumber > Setting.PlayerNum)
        {
            this.gameObject.SetActive(false);
        }
        m_text =this.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_text.text = m_player.GetItem().GetName();
    }
}
