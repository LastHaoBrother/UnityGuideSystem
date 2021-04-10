// ========================================================
// Description：
// Author：lch 
// CreateTime：2021/04/10 00:13
// Version：2020.2.5f1
// ========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 事件渗透   参考https://www.xuanyusong.com/archives/4408
/// </summary>
public class GuidanceEventPenetrate : MonoBehaviour, ICanvasRaycastFilter
{
    private Image targetImage;
    public void SetTargetImage(Image target)
    {
        targetImage = target;
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (targetImage == null)
        {
            return true;
        }
        return !RectTransformUtility.RectangleContainsScreenPoint(targetImage.rectTransform, sp, eventCamera);
    }
}
