using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reflector_SO", menuName = "Reflector")]
public class Reflector_SO : ScriptableObject
{
    public string reflectableDirections;
    public Sprite reflectorSprite;
    public bool spriteFlipX;
    public bool spriteFlipY;
    public string reflectDirTag;
    public Material laserMaterial;
    public float zRotation;

}
