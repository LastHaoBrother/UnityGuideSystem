// ========================================================
// Description：
// Author：lch 
// CreateTime：2021/04/10 00:13
// Version：2020.2.5f1
// ========================================================
using UnityEngine;
using UnityEngine.UI;

public class RectGuidance : ShapeGuidanceBase
{
    public static RectGuidance instance;
    private float targetOffsetX = 0;
    private float targetOffsetY = 0;
    private float currentOffsetX = 0f;
    private float currentOffsetY = 0f;

    private void Awake()
    {
        instance = this;
    }

    public override void Init(Image target)
    {
        base.Init(target);
        //计算高亮显示区域在画布中的范围
        targetOffsetX = Vector2.Distance(WorldToCanvasPos(canvas, corners[0]), WorldToCanvasPos(canvas, corners[3])) / 2f;
        targetOffsetY = Vector2.Distance(WorldToCanvasPos(canvas, corners[0]), WorldToCanvasPos(canvas, corners[1])) / 2f;
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
            //计算偏移初始值
            for (int i = 0; i < corners.Length; i++)
            {
                if (i % 2 == 0)
                {
                    currentOffsetX = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, corners[i]), center), currentOffsetX);
                }
                else
                {
                    currentOffsetY = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, corners[i]), center), currentOffsetY);
                }
            }
        }
        //设置遮罩材质中当前偏移的变量
        material.SetFloat("_SliderX", currentOffsetX);
        material.SetFloat("_SliderY", currentOffsetY);
    }
    /// <summary>
    /// 收缩速度
    /// </summary>
    private float shrinkVelocityX = 0f;
    private float shrinkVelocityY = 0f;
    private void Update()
    {
        //从当前偏移量到目标偏移量差值显示收缩动画
        float valueX = Mathf.SmoothDamp(currentOffsetX, targetOffsetX, ref shrinkVelocityX, shrinkTime);
        float valueY = Mathf.SmoothDamp(currentOffsetY, targetOffsetY, ref shrinkVelocityY, shrinkTime);
        if (!Mathf.Approximately(valueX, currentOffsetX))
        {
            currentOffsetX = valueX;
            material.SetFloat("_SliderX", currentOffsetX);
        }
        if (!Mathf.Approximately(valueY, currentOffsetY))
        {
            currentOffsetY = valueY;
            material.SetFloat("_SliderY", currentOffsetY);
        }
    }
}
