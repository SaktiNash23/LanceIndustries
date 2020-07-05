﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGridUI : MonoBehaviour
{
    [SerializeField] Image leftWall;
    [SerializeField] Image rightWall;
    [SerializeField] Image topWall;
    [SerializeField] Image bottomWall;

    [SerializeField] Image originPoint;

    [SerializeField] Image destinationPoint;

    [SerializeField] Image leftPortal;
    [SerializeField] Image rightPortal;
    [SerializeField] Image topPortal;
    [SerializeField] Image bottomPortal;

    [SerializeField] Sprite basicWallSprite;
    [SerializeField] Sprite whiteWallSprite;
    [SerializeField] Sprite redWallSprite;
    [SerializeField] Sprite yellowWallSprite;
    [SerializeField] Sprite blueWallSprite;

    [SerializeField] Sprite whiteOriginPointSprite;
    [SerializeField] Sprite redOriginPointSprite;
    [SerializeField] Sprite yellowOriginPointSprite;
    [SerializeField] Sprite blueOriginPointSprite;

    [SerializeField] Sprite whiteDestinationPointSprite;
    [SerializeField] Sprite redDestinationPointSprite;
    [SerializeField] Sprite yellowDestinationPointSprite;
    [SerializeField] Sprite blueDestinationPointSprite;

    [SerializeField] Sprite portalSetOneSprite;
    [SerializeField] Sprite portalSetTwoSprite;

    public void ToggleWall(BorderColor wallColor, SNAPPING_DIR wallDir, bool show)
    {
        switch (wallDir)
        {
            case SNAPPING_DIR.LEFT:
                leftWall.enabled = show;
                ApplyWallColor(leftWall, wallColor);
                break;
            case SNAPPING_DIR.RIGHT:
                rightWall.enabled = show;
                ApplyWallColor(rightWall, wallColor);
                break;
            case SNAPPING_DIR.UP:
                topWall.enabled = show;
                ApplyWallColor(topWall, wallColor);
                break;
            case SNAPPING_DIR.DOWN:
                bottomWall.enabled = show;
                ApplyWallColor(bottomWall, wallColor);
                break;
        }
    }

    public void ToggleOriginPoint(BorderColor originPointColor, Quaternion targetRot, bool show)
    {
        switch (originPointColor)
        {
            case BorderColor.WHITE:
                originPoint.transform.rotation = targetRot;
                originPoint.sprite = whiteOriginPointSprite;
                break;
            case BorderColor.RED:
                originPoint.transform.rotation = targetRot;
                originPoint.sprite = redOriginPointSprite;
                break;
            case BorderColor.YELLOW:
                originPoint.transform.rotation = targetRot;
                originPoint.sprite = yellowOriginPointSprite;
                break;
            case BorderColor.BLUE:
                originPoint.transform.rotation = targetRot;
                originPoint.sprite = blueOriginPointSprite;
                break;
            default:
                break;
        }

        originPoint.enabled = show;
    }

    public void ToggleDestinationPoint(BorderColor destinationPointColor, Quaternion targetRot, bool show)
    {
        switch (destinationPointColor)
        {
            case BorderColor.WHITE:
                destinationPoint.transform.rotation = targetRot;
                destinationPoint.sprite = whiteDestinationPointSprite;
                break;
            case BorderColor.RED:
                destinationPoint.transform.rotation = targetRot;
                destinationPoint.sprite = redDestinationPointSprite;
                break;
            case BorderColor.YELLOW:
                destinationPoint.transform.rotation = targetRot;
                destinationPoint.sprite = yellowDestinationPointSprite;
                break;
            case BorderColor.BLUE:
                destinationPoint.transform.rotation = targetRot;
                destinationPoint.sprite = blueDestinationPointSprite;
                break;
            default:
                break;
        }

        destinationPoint.enabled = show;
    }

    public void TogglePortal(bool setOne, SNAPPING_DIR portalDir, bool show)
    {
        switch (portalDir)
        {
            case SNAPPING_DIR.LEFT:
                leftPortal.enabled = show;
                if (setOne)
                    leftPortal.sprite = portalSetOneSprite;
                else
                    leftPortal.sprite = portalSetTwoSprite;
                break;
            case SNAPPING_DIR.RIGHT:
                rightPortal.enabled = show;
                if (setOne)
                    rightPortal.sprite = portalSetOneSprite;
                else
                    rightPortal.sprite = portalSetTwoSprite;
                break;
            case SNAPPING_DIR.UP:
                topPortal.enabled = show;
                if (setOne)
                    topPortal.sprite = portalSetOneSprite;
                else
                    topPortal.sprite = portalSetTwoSprite;
                break;
            case SNAPPING_DIR.DOWN:
                bottomPortal.enabled = show;
                if (setOne)
                    bottomPortal.sprite = portalSetOneSprite;
                else
                    bottomPortal.sprite = portalSetTwoSprite;
                break;
        }
    }

    public void ToggleAllWalls(bool show)
    {
        leftWall.enabled = rightWall.enabled = topWall.enabled = bottomWall.enabled = show;
    }

    public void ToggleAllPortals(bool show)
    {
        leftPortal.enabled = rightPortal.enabled = topPortal.enabled = bottomPortal.enabled = show;
    }

    private void ApplyWallColor(Image targetWall, BorderColor wallColor)
    {
        switch (wallColor)
        {
            case BorderColor.WHITE:
                targetWall.sprite = whiteWallSprite;
                break;
            case BorderColor.RED:
                targetWall.sprite = redWallSprite;
                break;
            case BorderColor.YELLOW:
                targetWall.sprite = yellowWallSprite;
                break;
            case BorderColor.BLUE:
                targetWall.sprite = blueWallSprite;
                break;
            default:
                targetWall.sprite = basicWallSprite;
                break;
        }
    }
}
