using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorColorPanel : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    [Header("REFLECTOR BUTTONS REFERENCES")]
    [SerializeField] protected ReflectorButton whiteReflectorButton;
    [SerializeField] protected ReflectorButton redReflectorButton;
    [SerializeField] protected ReflectorButton blueReflectorButton;
    [SerializeField] protected ReflectorButton yellowReflectorButton;

    [Header("SPRITES")]
    [SerializeField] protected Sprite whiteBasicReflectorSprite;
    [SerializeField] protected Sprite redBasicReflectorSprite;
    [SerializeField] protected Sprite blueBasicReflectorSprite;
    [SerializeField] protected Sprite yellowBasicReflectorSprite;
    [SerializeField] protected Sprite whiteTranslucentReflectorSprite;
    [SerializeField] protected Sprite redTranslucentReflectorSprite;
    [SerializeField] protected Sprite blueTranslucentReflectorSprite;
    [SerializeField] protected Sprite yellowTranslucentReflectorSprite;
    [SerializeField] protected Sprite whiteDoubleWayReflectorSprite;
    [SerializeField] protected Sprite redDoubleWayReflectorSprite;
    [SerializeField] protected Sprite blueDoubleWayReflectorSprite;
    [SerializeField] protected Sprite yellowDoubleWayReflectorSprite;
    [SerializeField] protected Sprite whiteThreeWayReflectorSprite;
    [SerializeField] protected Sprite redThreeWayReflectorSprite;
    [SerializeField] protected Sprite blueThreeWayReflectorSprite;
    [SerializeField] protected Sprite yellowThreeWayReflectorSprite;

    public bool Enabled { get; private set; }

    private int panelDisplayedHash;
    private REFLECTOR_TYPE currentReflectorType;

    private void Start()
    {
        panelDisplayedHash = Animator.StringToHash("Enabled");
    }

    public void EnablePanel(bool enable, REFLECTOR_TYPE reflectorType = REFLECTOR_TYPE.DEFAULT)
    {
        currentReflectorType = reflectorType;

        // Set Reflector Type & Color
        whiteReflectorButton.ReflectorType = reflectorType;
        redReflectorButton.ReflectorType = reflectorType;
        blueReflectorButton.ReflectorType = reflectorType;
        yellowReflectorButton.ReflectorType = reflectorType;
        whiteReflectorButton.ReflectorColor = LASER_COLOR.WHITE;
        redReflectorButton.ReflectorColor = LASER_COLOR.RED;
        blueReflectorButton.ReflectorColor = LASER_COLOR.BLUE;
        yellowReflectorButton.ReflectorColor = LASER_COLOR.YELLOW;

        // Set Reflector Sprite
        switch (reflectorType)
        {
            case REFLECTOR_TYPE.BASIC:
                whiteReflectorButton.Image.sprite = whiteBasicReflectorSprite;
                redReflectorButton.Image.sprite = redBasicReflectorSprite;
                blueReflectorButton.Image.sprite = blueBasicReflectorSprite;
                yellowReflectorButton.Image.sprite = yellowBasicReflectorSprite;
                break;
            case REFLECTOR_TYPE.TRANSLUCENT:
                whiteReflectorButton.Image.sprite = whiteTranslucentReflectorSprite;
                redReflectorButton.Image.sprite = redTranslucentReflectorSprite;
                blueReflectorButton.Image.sprite = blueTranslucentReflectorSprite;
                yellowReflectorButton.Image.sprite = yellowTranslucentReflectorSprite;
                break;
            case REFLECTOR_TYPE.DOUBLE_WAY:
                whiteReflectorButton.Image.sprite = whiteDoubleWayReflectorSprite;
                redReflectorButton.Image.sprite = redDoubleWayReflectorSprite;
                blueReflectorButton.Image.sprite = blueDoubleWayReflectorSprite;
                yellowReflectorButton.Image.sprite = yellowDoubleWayReflectorSprite;
                break;
            case REFLECTOR_TYPE.THREE_WAY:
                whiteReflectorButton.Image.sprite = whiteThreeWayReflectorSprite;
                redReflectorButton.Image.sprite = redThreeWayReflectorSprite;
                blueReflectorButton.Image.sprite = blueThreeWayReflectorSprite;
                yellowReflectorButton.Image.sprite = yellowThreeWayReflectorSprite;
                break;
        }

        // Refresh Text
        GameplayUIManager.Instance.RefreshReflectorStockUIs(reflectorType);

        // Animation
        animator.SetBool(panelDisplayedHash, enable);
        Enabled = enable;
    }

    #region Editor Button Action Binding Functions
    public void EnablePanelEditor(bool enable)
    {
        EnablePanel(enable, currentReflectorType);
    }
    #endregion
}
