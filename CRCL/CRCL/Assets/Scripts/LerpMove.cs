using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMove : MonoBehaviour {
    [SerializeField, Range(0.0f, 10.0f)]
    private float m_time = 0.0f;

    [SerializeField]
    private Vector3 m_targetPosition = Vector3.zero;

    [SerializeField]
    private Color m_startColor = Color.white;
    [SerializeField]
    private Color m_targetColor = Color.white;

    private float m_startTime = 0.0f;
    private Vector3 m_startPosition = Vector3.zero;

    private Color CurrentColor
    {
        set
        {
            var render = GetComponent<Renderer>();
            if(render == null)
            {
                return;
            }

            if(render.material == null)
            {
                return;
            }

            render.material.color = value;
            
        }
    }

	// Use this for initialization
	void Start () {
        m_startTime = Time.time;
        m_startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float timeStep = m_time > 0.0f ? (Time.time - m_startTime) / m_time : 1.0f;
        if(m_time > 0.0f)
        {
            timeStep = (Time.time - m_startTime) / m_time;
        }
        else
        {
            timeStep = 1.0f;
        }

        timeStep = (1 - Mathf.Cos(Time.time * Mathf.PI)) / 2.0f;

        //transform.position = Vector3.Lerp(m_startPosition, m_targetPosition, timeStep);
        transform.position = Lerp(m_startPosition, m_targetPosition, timeStep, Linearity);
        CurrentColor = Color.Lerp(m_startColor, m_targetColor, timeStep);
    }

    static Vector3 Lerp (Vector3 startPosition, Vector3 targetPosition, float t, Func<float, float> v)
    {
        Vector3 lerpPosition = Vector3.zero;

        lerpPosition = (1 - v(t)) * startPosition + v(t) * targetPosition;

        return lerpPosition;
    }

    static float Linearity(float t)
    {
        float y = t;

        return y;
    }

    static float EaseIn(float t)
    {
        float y = t * t;

        return y;
    }
    
    static float EaseOut(float t)
    {
        float y = t * (2 - t);

        return y;
    }

    static float Cube(float t)
    {
        float y = t * t * (3 - 2 * t);

        return y;
    }

    static float CosLerp(float t)
    {
        float y = (1 - Mathf.Cos(t * Mathf.PI)) / 2.0f;

        return y;
    }
}
