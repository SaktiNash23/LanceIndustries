using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LibraryLinker : MonoBehaviour
{
    private static LibraryLinker _instance;

    public static LibraryLinker Instance
    {
        get { return _instance; }
    }

    [BoxGroup("DROP ALL LIBRARY HERE")] [SerializeField] List<LibraryBase> dataLibraries;

    private MapInfoLibrary _mapInfoLib;

    public MapInfoLibrary MapInfoLib
    {
        get
        {
            if (_mapInfoLib == null)
                _mapInfoLib = (MapInfoLibrary)GetLibraryByName("MapInfoLibrary");
            return _mapInfoLib;
        }
    }

    //----------------------------- MONOBEHAVIOUR FUNCTIONS -----------------------------//

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private LibraryBase GetLibraryByName(string libName)
    {
        foreach (var dataLibrary in dataLibraries)
            if (dataLibrary.libraryName == libName)
                return dataLibrary;

        Debug.LogError("Library with name " + "<color=red>" + libName + "</color> couldn't be found.");
        return null;
    }
}
