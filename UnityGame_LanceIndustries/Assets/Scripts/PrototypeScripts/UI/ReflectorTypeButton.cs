using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ReflectorTypeButton : MonoBehaviour
{
    [SerializeField] protected REFLECTOR_TYPE reflectorType;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(EnablePanel);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(EnablePanel);
    }

    private void EnablePanel()
    {
        if (!GameManager.Instance.GameStarted)
            GameplayUIManager.Instance.reflectorColorPanel.EnablePanel(true, reflectorType);
    }
}
