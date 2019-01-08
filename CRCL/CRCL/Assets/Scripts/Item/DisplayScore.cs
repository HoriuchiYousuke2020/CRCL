using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayScore : MonoBehaviour
{
    [SerializeField]
    private Score m_score;

    [SerializeField]
    private int PlayerNumber;

    private Text m_text;
    // Use this for initialization
    void Start ()
    {
		if(PlayerNumber > Setting.PlayerNum)
        {
            this.gameObject.SetActive(false);
        }
        m_text = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       m_text.text =  m_score.GetScore().ToString();        
    }
}
