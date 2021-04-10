// ========================================================
// Description：
// Author：lch 
// CreateTime：2021/04/10 00:13
// Version：2020.2.5f1
// ========================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGuidance : ShapeGuidanceBase
{
    public static CircleGuidance instance;

    private float radius;
    private float currentRadius;
    private void Awake()
    {
        instance = this;
    }
    public override void Init(Image target)
    {
        base.Init(target);
        //计算最终高亮显示区域的半径
        radius = Vector2.Distance(WorldToCanvasPos(canvas, corners[0]), WorldToCanvasPos(canvas, corners[2])) / 2;
        //计算高亮显示区域的中心
        float x = corners[0].x + ((corners[3].x - corners[0].x) / 2);
        float y = corners[0].y + ((corners[1].y - corners[0].y) / 2);
        Vector3 centerWorld = new Vector3(x, y, 0);
        Vector2 center = WorldToCanvasPos(canvas, centerWorld);
        //设置遮罩材质中的中心变量
        Vector4 centerMat = new Vector4(center.x, center.y, 0, 0);
        material = GetComponent<Image>().material;
        material.SetVector("_Center", centerMat);
        //计算当前高亮显示区域的半径
        RectTransform canRectTransform = canvas.transform as RectTransform;
        if (canRectTransform != null)
        {
            //获取画布区域的四个顶点
            canRectTransform.GetWorldCorners(corners);
            //将画布顶点距离高亮区域中心最近的距离昨晚当前高亮区域半径的初始值
            foreach (var corner in corners)
            {
                currentRadius = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, corner), corner), currentRadius);
            }
        }
        material.SetFloat("_Slider", currentRadius);
    }
    private float shrinkVelocity = 0f;
    private void Update()
    {
        //从当前半径到目标半径差值显示收缩动画
        float value = Mathf.SmoothDamp(currentRadius, radius, ref shrinkVelocity, shrinkTime);
        if (!Mathf.Approximately(value, currentRadius))
        {
            currentRadius = value;
            material.SetFloat("_Slider", currentRadius);
        }
    }

}
