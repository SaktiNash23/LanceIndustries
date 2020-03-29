using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


    public class GameManager : MonoBehaviour
    {
        /*
         Index References for allReflectorSO array 

         Index 0: Base Reflector
         Index 1: Translucent Reflector
         Index 2: Double Way Reflector
         Index 3: Split Reflector

         */
        [InfoBox("Ensure the Reflector Scriptable Objects are placed in the right order in the array", EInfoBoxType.Normal)]
        [ReorderableList]
        public Reflector_SO[] allReflectorSO;

        public static GameManager gameManagerInstance;


        void Awake()
        {
            if (gameManagerInstance == null)
            {
                gameManagerInstance = this;
            }
        }

    }
