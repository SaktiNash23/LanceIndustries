using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorColorPanel : MonoBehaviour
{
    public Animator ReflectorColorPanelAnimator { get; private set; }

    void Awake()
    {
        ReflectorColorPanelAnimator = GetComponent<Animator>();
    }

    //This function is called in the ReflectorColorPanel_Popdown animation as an Animation Event, cause we want the popdown animation to play until
    //finished, then only disable the reflector color panel
    public void disableReflectorColorPanel()
    {
        GameManager.Instance.isReflectorColorPanelActive = false;
        gameObject.SetActive(false);
    }

}
