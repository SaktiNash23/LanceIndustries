using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reflector_SO", menuName = "Reflector")]
public class Reflector_SO : ScriptableObject
{
    public Material laserMaterialToChange;
    public Color laserColorToChange;
    public AnimationClip animClip_Rotation_0;
    public AnimationClip animClip_Rotation_90;
    public AnimationClip animClip_Rotation_180;
    public AnimationClip animClip_Rotation_270;
}
