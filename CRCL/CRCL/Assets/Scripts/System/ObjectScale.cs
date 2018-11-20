using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScale : MonoBehaviour {
    float m_scal;
    Vector3 m_thisScal;
    float time;
    [SerializeField]
    private float m_countSize;
	// Use this for initialization
	void Start () {
        time = 0;
        m_scal = 1.0f;
        m_thisScal = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
        time += m_countSize;
        m_scal = Mathf.Cos(Mathf.Sin(time)) * Mathf.Cos(Mathf.Sin(time));
        
        if(m_scal < 0.0f)
        {
            m_scal = m_scal * -1;
        }
        this.transform.localScale = new Vector3(m_thisScal.x + m_scal,m_thisScal.y + m_scal,m_thisScal.z);
	}
}
