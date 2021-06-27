using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorColorPanel : MonoBehaviour
{
    [SerializeField] Animator animator;

    int panelDisplayedHash;

    public bool Enabled { get; private set; }

    private void Start()
    {
        panelDisplayedHash = Animator.StringToHash("Enabled");
    }

    public void EnablePanel(bool enable)
    {
        animator.SetBool(panelDisplayedHash, enable);
        Enabled = enable;
    }
}
