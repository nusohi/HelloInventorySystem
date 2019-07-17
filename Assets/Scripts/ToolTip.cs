using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Text toolTipText;
    private Text foregroundText;
    private Vector3 offset = new Vector3(15, -10, 0);

    private CanvasGroup canvasGroup;
    private float targetAlpha = 0f;
    private float alphaSpeed = 0.25f;


    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        toolTipText = GetComponent<Text>();
        foregroundText = transform.Find("Content").GetComponent<Text>();
    }
    
    private void Update() {
        if (canvasGroup.alpha != targetAlpha) {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, alphaSpeed);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) <= 0.001f) {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }


    public void Show(string text) {
        toolTipText.text = text;
        foregroundText.text = text;
        targetAlpha = 1f;
    }

    public void Hide() {
        targetAlpha = 0f;
    }

    public void SetPos(Vector3 pos) {
        transform.localPosition = offset + pos;
    }

}
