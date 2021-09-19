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

    private UnityAction onClickCallback;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        onClickCallback = () => GameplayUIManager.Instance.reflectorColorPanel.EnablePanel(true, reflectorType);
        button.onClick.AddListener(onClickCallback);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(onClickCallback);
    }
}
