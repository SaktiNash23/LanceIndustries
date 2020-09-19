using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    private Slider sliderProgressBar;

    private void Awake()
    {
        sliderProgressBar = GetComponent<Slider>();
    }

    public void UpdateProgressBar(float progress)
    {
        sliderProgressBar.value = progress;
    }
}
