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


public class GuideManagers : MonoBehaviour
{

    public List<GuideUIList> guideList = new List<GuideUIList>();

    private int currentIndex = 0;

    private bool isFinish = false;

    private GameObject maskPrefabs;

    /// <summary>
    /// 新手引导入口
    /// </summary>
    public void Next()
    {
        if (isFinish || currentIndex > guideList.Count)
        {
            return;
        }

        if (currentIndex != 0 && guideList[currentIndex - 1].go.GetComponent<EventTriggerListener>() != null)
        {
            EventTriggerListener.GetListener(guideList[currentIndex - 1].go).onClick -= null;
        }

        if (maskPrefabs == null)
        {
            maskPrefabs = Instantiate(Resources.Load<GameObject>("RectGuidance_Panel"), this.transform);
        }

        maskPrefabs.GetComponent<RectGuidance>().Init(guideList[currentIndex].go.GetComponent<Image>()); ;

        currentIndex++;

        if (currentIndex < guideList.Count)
        {
            EventTriggerListener.GetListener(guideList[currentIndex - 1].go).onClick += (go) =>
            {
                Next();
            };
        }

        else if (currentIndex == guideList.Count)
        {
            EventTriggerListener.GetListener(guideList[currentIndex - 1].go).onClick += (go) =>
            {
                maskPrefabs.gameObject.SetActive(false);

                EventTriggerListener.GetListener(guideList[currentIndex - 1].go).onClick -= null;

                isFinish = true;
                Debug.Log("新手引导结束");
            };

        }
    }

    public void FirstBtnClick()
    {
        Debug.Log("点击第一步按钮");
    }

    public void SecondBtnClick()
    {
        Debug.Log("点击第二步按钮");
    }

    public void ButtonClick(int index)
    {
        Debug.Log($"点击第 {index} 步按钮");
    }
}


[Serializable]
public class GuideUIList
{
    public GameObject go;

    public GuideUIList(GameObject go)
    {
        this.go = go;
    }
}

