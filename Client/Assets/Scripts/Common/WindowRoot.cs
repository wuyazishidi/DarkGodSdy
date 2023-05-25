/****************************************************
    文件：WindowRoot.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/4 4:31:31
	功能：UI界面基类
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour {

    protected ResSvc resSvc = null;
    protected AudioSvc audioSvc = null;
    protected NetSvc netSvc = null;
    protected TimerSvc timerSvc = null;

    /// <summary>
    /// 改变物体的状态
    /// </summary>
    /// <param name="isActive"></param>
    public void SetWndState(bool isActive = true) {
        if (gameObject.activeSelf != isActive) {
            SetActive(gameObject, isActive);
        }
        if (isActive) {
            InitWnd();
        }
        else {
            ClearWnd();
        }
    }

    /// <summary>
    /// 获取物体的状态
    /// </summary>
    /// <returns></returns>
    public bool GetWndState() {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// 界面的初始化
    /// </summary>
    protected virtual void InitWnd() {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
        netSvc = NetSvc.Instance;
        timerSvc = TimerSvc.Instance;
    }

    /// <summary>
    /// 清除界面的引用
    /// </summary>
    protected virtual void ClearWnd() {
        resSvc = null;
        audioSvc = null;
        netSvc = null;
        timerSvc = null;
    }

    #region Tool Functions

    protected void SetActive(GameObject go, bool isActive = true) {
        go.SetActive(isActive);
    }
    protected void SetActive(Transform trans, bool state = true) {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true) {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true) {
        img.transform.gameObject.SetActive(state);
    }
    protected void SetActive(Text txt, bool state = true) {
        txt.transform.gameObject.SetActive(state);
    }

    protected void SetText(Text txt, string context = "") {
        txt.text = context;
    }
    protected void SetText(Transform trans, int num = 0) {
        SetText(trans.GetComponent<Text>(), num);
    }
    protected void SetText(Transform trans, string context = "") {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Text txt, int num = 0) {
        SetText(txt, num.ToString());
    }

    protected void SetSprite(Image img, string path) {
        Sprite sp = resSvc.LoadSprite(path, true);
        img.sprite = sp;
    }

    protected T GetOrAddComponect<T>(GameObject go) where T : Component {
        T t = go.GetComponent<T>();
        if (t == null) {
            t = go.AddComponent<T>();
        }
        return t;
    }

    protected Transform GetTrans(Transform trans, string name) {
        if (trans != null) {
            return trans.Find(name);
        }
        else {
            return transform.Find(name);
        }
    }
    #endregion

    #region Click Evts
    protected void OnClick(GameObject go, Action<object> cb, object args) {
        PEListener listener = GetOrAddComponect<PEListener>(go);
        listener.onClick = cb;
        listener.args = args;
    }

    protected void OnClickDown(GameObject go, Action<PointerEventData> cb) {
        PEListener listener = GetOrAddComponect<PEListener>(go);
        listener.onClickDown = cb;
    }

    protected void OnClickUp(GameObject go, Action<PointerEventData> cb) {
        PEListener listener = GetOrAddComponect<PEListener>(go);
        listener.onClickUp = cb;
    }

    protected void OnDrag(GameObject go, Action<PointerEventData> cb) {
        PEListener listener = GetOrAddComponect<PEListener>(go);
        listener.onDrag = cb;
    }
    #endregion
}