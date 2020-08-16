using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Video;
using TMPro;

public class TutorialPanel : MonoBehaviour
{
    [ResizableTextArea]
    public string TutorialTitle;

    public RenderTexture videoRenderTexture;

    public VideoClip tutorialVideoClip;

    [ResizableTextArea]
    public string tutorialDescriptionText;

    //Start is called before the first frame update
    void Start()
    {
        InitializeTutorialPanel();
    }

    private void InitializeTutorialPanel()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = TutorialTitle.ToString();

        transform.GetChild(1).GetComponent<RawImage>().texture = videoRenderTexture;

        transform.GetChild(1).GetChild(0).GetComponent<VideoPlayer>().clip = tutorialVideoClip;

        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = tutorialDescriptionText;

    }
}
