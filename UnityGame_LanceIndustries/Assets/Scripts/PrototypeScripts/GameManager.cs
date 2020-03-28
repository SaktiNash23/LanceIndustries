using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     Index References for allReflectorSO array 
     
     Index 0: Base Reflector
     Index 1: Translucent Reflector
     Index 2: Double Way Reflector
     Index 3: Split Reflector

     */
    public Reflector_SO[] allReflectorSO;
    public static GameManager gameManagerInstance;

    void Awake()
    {
        if(gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
    }
  
}
