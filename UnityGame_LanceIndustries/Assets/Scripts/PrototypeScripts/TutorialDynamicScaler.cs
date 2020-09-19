using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class TutorialDynamicScaler : MonoBehaviour
{
    public GameObject content;
    float aspectRatio = 0.0f;
    public int numberOfTutorialPanels = 0;

    private void Awake()
    {
        Debug.Log("Aspect : " + Camera.main.aspect);
        aspectRatio = Camera.main.aspect;

        if(aspectRatio > 0.55f)
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(1080.0f * numberOfTutorialPanels, 1920.0f);
        }
        else if(aspectRatio > 0.49f && aspectRatio < 0.51f)
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(1080.0f * numberOfTutorialPanels, 2160.0f);
        }
        else if(aspectRatio > 0.47f && aspectRatio < 0.49f)
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(1080.0f * numberOfTutorialPanels, 2280.0f);
        }
    }
}
