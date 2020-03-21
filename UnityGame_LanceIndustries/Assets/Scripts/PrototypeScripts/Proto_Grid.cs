using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_Grid : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Reflector within Grid");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Reflector out of Grid");
    }

}
