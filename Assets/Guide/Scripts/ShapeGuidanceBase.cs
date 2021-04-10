// ========================================================
// Description：
// Author：lch 
// CreateTime：2021/04/10 00:13
// Version：2020.2.5f1
// ========================================================
using UnityEngine;
using UnityEngine.UI;

public class ShapeGuidanceBase : MonoBehaviour
{
    public Image target;
    protected Vector3[] corners = new Vector3[4];
    protected Material material;
    protected float shrinkTime = 0.5f;
    protected GuidanceEventPenetrate eventPenetrate;

    protected Canvas canvas;

    public virtual void Init(Image target)
    {
        this.target = target;
        eventPenetrate = GetComponent<GuidanceEventPenetrate>();
        if (eventPenetrate != null)
        {
            eventPenetrate.SetTargetImage(target);
        }
        target.rectTransform.GetWorldCorners(corners);
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    protected Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
        return position;
    }

}
