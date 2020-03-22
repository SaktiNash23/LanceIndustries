using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LibraryBase : ScriptableObject
{
    [BoxGroup("LIBRARY BASE INFO")] public string idNo;
    [BoxGroup("LIBRARY BASE INFO")] public string libraryName;
}
