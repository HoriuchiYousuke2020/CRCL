using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform targetTfm;

    private RectTransform canvasRectTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 1.5f, 0);

    void Start()
    {
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        myRectTfm = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 pos;

        switch (canvas.renderMode)
        {

            case RenderMode.ScreenSpaceOverlay:
                myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);

                break;

            case RenderMode.ScreenSpaceCamera:
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTfm, screenPos, Camera.main, out pos);
                myRectTfm.localPosition = pos;
                break;

            case RenderMode.WorldSpace:
                myRectTfm.LookAt(Camera.main.transform);

                break;
        }
    }
}