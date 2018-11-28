using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fade : MonoBehaviour
{
    public enum State
    {
        SEE_THROUGH,
        OPACITY,
        NEITHER,
    } 

    [SerializeField]
    private GameObject Panel;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float Time;

    [SerializeField]
    private State state;

    public State STATE
    {
        get { return state; }
    }
    
    private float a;    //アルファ値

    // Use this for initialization
    void Start ()
    {
        switch (state)
        {
            case State.SEE_THROUGH:
                a = 0.0f;
                break;
            case State.OPACITY:
                a = 1.0f;
                break;
            case State.NEITHER:
                a = Panel.GetComponent<Image>().color.a;
                break;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Panel.GetComponent<Image>().color = new Color(0, 0, 0, a);
    }

    IEnumerator FadeInImage()
    {
        while(a <= 1.0f)
        {
            a += 1.0f / (Time * 60);
            yield return null;
        }

        state = State.OPACITY;
    }

    IEnumerator FadeOutImage()
    {
        while (a >= 0.0f)
        {
            a -= 1.0f / (Time * 60);
            yield return null;
        }

        state = State.SEE_THROUGH;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInImage());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutImage());
    }
}
