using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxBase : PoolObject
{
    [Header("SETTINGS")]
    [SerializeField] protected bool destroyOnFinishPlaying;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    public float GetCurrentAnimationClipLength => animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

    public void DestroyOnFinishPlaying()
    {
        if(destroyOnFinishPlaying)
            Push();
    }
}
