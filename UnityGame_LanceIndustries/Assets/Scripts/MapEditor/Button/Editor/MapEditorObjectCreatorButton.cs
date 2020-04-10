using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class MapEditorObjectCreatorButton : MonoBehaviour
{
    [BoxGroup("OBJECT CREATOR BUTTON REFERENCES")] [SerializeField] Button btnObjectCreator;
    [BoxGroup("OBJECT CREATOR BUTTON REFERENCES")] [SerializeField] MapEditorInSceneObject mapEditorInSceneObjectPrefab;

    //------------------------------ MONOBEHAVIOUR FUNCTIONS ------------------------------//

    private void Start()
    {
        btnObjectCreator.onClick.AddListener(ObjectCreatorButtonAction);
    }

    //------------------------------ BUTTON ACTIONS ------------------------------//

    private void ObjectCreatorButtonAction()
    {
        MapEditorInSceneObject sceneObject = Instantiate(mapEditorInSceneObjectPrefab, Vector3.zero, Quaternion.identity);
        //MapEditorInputManager.Instance.SelectObject(sceneObject);
    }
}
