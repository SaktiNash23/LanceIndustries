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
        MapEditorInSceneObject sceneObject = Instantiate(mapEditorInSceneObjectPrefab, new Vector3(-0.5f, -3.7f, 0.0f), mapEditorInSceneObjectPrefab.transform.rotation);
        MapEditorInputManager.Instance.SelectObject(sceneObject);
        //MapEditorInputManager.Instance.SelectObject(sceneObject);
    }
}
