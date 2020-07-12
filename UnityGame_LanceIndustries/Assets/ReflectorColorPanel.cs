using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorColorPanel : MonoBehaviour
{
    private Animator reflectorColorPanel_Anim;

    void Awake()
    {
        reflectorColorPanel_Anim = GetComponent<Animator>();   
    }

    //This function is called in the ReflectorColorPanel_Popdown animation as an Animation Event, cause we want the popdown animation to play until
    //finished, then only disable the reflector color panel
    public void disableReflectorColorPanel()
    {
        GameManager.gameManagerInstance.isReflectorColorPanelActive = false;
        gameObject.SetActive(false);
    }

}
