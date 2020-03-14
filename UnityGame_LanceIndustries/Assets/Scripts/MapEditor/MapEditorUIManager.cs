using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class MapEditorUIManager : MonoBehaviour
{
    [BoxGroup("UI REFERENCES")] [SerializeField] RectTransform contentMapList;
    [BoxGroup("UI REFERENCES")] [SerializeField] CanvasGroup cgPanelMapList;

    [BoxGroup("PREFABS")] [SerializeField] MapButton mapButtonPrefab;

    //-------------------------- MONOBEHAVIOUR FUNCTIONS --------------------------//

    public void SetupMapList()
    {
        for (int i = 0; i < 20; i++)
            Instantiate(mapButtonPrefab, cgPanelMapList.transform, false);

        float heightMapButton = mapButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        contentMapList.sizeDelta = new Vector2(contentMapList.sizeDelta.x, heightMapButton * 20 + cgPanelMapList.GetComponent<VerticalLayoutGroup>().spacing * 20);
    }
}
