using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Proto_Grid : MonoBehaviour
{
    public bool isOccupied_Grid = false;
    public GameObject reflectorStored_Grid;
    

    public bool isOccupied_GridAccessor
    {
        get
        {
            return isOccupied_Grid;
        }

        set
        {
            isOccupied_Grid = value;
        }
    }
}
