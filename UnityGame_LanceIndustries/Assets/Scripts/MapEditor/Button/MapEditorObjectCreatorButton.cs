using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class MapEditorObjectCreatorButton : MonoBehaviour
{
    [BoxGroup("OBJECT CREATOR BUTTON SETTINGS")] [SerializeField] bool usedToTogglePanel;

    [BoxGroup("OBJECT CREATOR BUTTON REFERENCES")] [SerializeField] Button btnObjectCreator;
    [BoxGroup("OBJECT CREATOR BUTTON REFERENCES")] [SerializeField] TMP_Text txtHotKey;
    [BoxGroup("OBJECT CREATOR BUTTON REFERENCES")] [SerializeField] MapEditorInSceneObject mapEditorInSceneObjectPrefab;

    //------------------------------ MONOBEHAVIOUR FUNCTIONS ------------------------------//

    private void Start()
    {
        if(!usedToTogglePanel)
            btnObjectCreator.onClick.AddListener(ObjectCreatorButtonAction);
    }

    //------------------------------ BUTTON ACTIONS ------------------------------//

    private void ObjectCreatorButtonAction()
    {
        if (!MapEditorInputManager.Instance.OptionMenuVisibility)
        {
            MapEditorInSceneObject sceneObject = Instantiate(mapEditorInSceneObjectPrefab, new Vector3(-0.5f, -3.7f, 0.0f), mapEditorInSceneObjectPrefab.transform.rotation);
            MapEditorInputManager.Instance.SelectObject(sceneObject);
        }
    }

    //------------------------------ UI RELATED FUNCTIONS ------------------------------//
    public void ToggleHotKeyText(bool show)
    {
        if (show)
            txtHotKey.gameObject.SetActive(true);
        else
            txtHotKey.gameObject.SetActive(false);
    }
}
