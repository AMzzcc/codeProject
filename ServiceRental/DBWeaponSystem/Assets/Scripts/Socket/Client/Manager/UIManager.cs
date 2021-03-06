using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PanelName
//{
//    normalPanel,
//    noticePanel
//}

//public class UIManager : Singleton<UIManager>
//{
    
//    public Dictionary<PanelName, BasePanel> panelDict = new Dictionary<PanelName, BasePanel>();
//    private Dictionary<PanelName, string> panelPath = new Dictionary<PanelName, string>();
//    [SerializeField]
//    private Stack<BasePanel> normalPanelStack = new Stack<BasePanel>();
//    private Stack<BasePanel> noticePanelStack = new Stack<BasePanel>();

//    private Transform canvasTransform;
//    private Transform normalPanelTf;
//    private Transform noticePanelTf;
//    private MessagePanel messagePanel;

//    void Start()
//    {
//        InitPanel();
//        canvasTransform = GameObject.Find("Canvas").transform;
//        normalPanelTf = GameObject.Find("normalPanel").transform;
//        noticePanelTf = GameObject.Find("noticePanel").transform;
//        BasePanel panel=PushPanel(PanelName.Message,PanelName.noticePanel);
//        panel.OnPause();
//        PushPanel(PanelName.Start,PanelName.normalPanel);
//        PushPanel(PanelName.Input,PanelName.normalPanel);

//    }

//    /// <summary>
//    /// 把ui显示在界面上
//    /// </summary>
//    /// <param name="panelName"></param>
//    public BasePanel PushPanel(PanelName panelName,PanelName panelType=PanelName.normalPanel)
//    {
//        Debug.Log("push"+ panelName);
//        if (panelDict.TryGetValue(panelName, out BasePanel panel))
//        {
//            //noticePanel
//            if (panelType == PanelName.noticePanel)
//            {
//                noticePanelStack.Push(panel);
//                panel.OnEnter();
//                Debug.Log("调用Message");
//                return panel;
//            }
//            else if(panelType == PanelName.normalPanel)
//            {
//                if (normalPanelStack.Count > 0)
//                {
//                    BasePanel topPanel = normalPanelStack.Peek();
//                    topPanel.OnPause();
//                }

//                normalPanelStack.Push(panel);
//                panel.OnEnter();
//                Debug.Log("调用");
//                return panel;
//            }

           
//        }
//        else
//        {
//            BasePanel panelObject = SpawnPanel(panelName);
//            if (panelType == PanelName.normalPanel)
//            {
//                if (normalPanelStack.Count > 0)
//                {
//                    BasePanel topPanel = normalPanelStack.Peek();
//                    topPanel.OnPause();
//                }

//                normalPanelStack.Push(panelObject);
//                panelObject.OnEnter();
//                Debug.Log("生成");
//                return panelObject;
//            }
//            else
//            {
//                if (noticePanelStack.Count > 0)
//                {
//                    BasePanel topPanel = noticePanelStack.Peek();
//                    topPanel.OnPause();
//                }

//                noticePanelStack.Push(panelObject);
//                panelObject.OnEnter();
//                Debug.Log("生成");
//                return panelObject;
//            }
//        }
//        return null;
//    }

//    /// <summary>
//    /// 关闭当前ui
//    /// </summary>
//    public void PopPanel(PanelName panelType)
//    {
//        if (normalPanelStack.Count == 0) return;

//        if (panelType == PanelName.normalPanel)
//        {
//            BasePanel topPanel = normalPanelStack.Pop();
//            Debug.Log("弹出" + topPanel);
//            topPanel.OnExit();
//            if(normalPanelStack.Count > 0) 
//            {
//                BasePanel panel = normalPanelStack.Peek();
//                panel.OnRecovery();
//            }
            
//        }
//        else if(panelType==PanelName.noticePanel)
//        {
//            //BasePanel topPanel = noticePanelStack.Pop();
//            //Debug.Log("弹出" + topPanel);
//            //topPanel.OnExit();
//            //if (noticePanelStack.Count > 0)
//            //{
//            //    BasePanel panel = noticePanelStack.Peek();
//            //    panel.OnRecovery();
//            //}
//            if (noticePanelStack.Count > 0)
//            {
//                BasePanel panel = noticePanelStack.Peek();
//                panel.OnExit();
//            }

//        }
        
//    }

//    /// <summary>
//    /// 实例化对应的ui
//    /// </summary>
//    /// <param name="panelType"></param>
//    private BasePanel SpawnPanel(PanelName panelType)
//    {
//        if (panelPath.TryGetValue(panelType, out string path))
//        {
//            GameObject g = GameObject.Instantiate(Resources.Load(path)) as GameObject;
//            if (panelType == PanelName.Message)
//            {
//                g.transform.SetParent(noticePanelTf, false);
//            }
//            else
//            {
//                g.transform.SetParent(normalPanelTf, false);
//            }
//            //g.transform.SetParent(canvasTransform, false);
//            BasePanel panel = g.GetComponent<BasePanel>();
//            //panel.SetUIMgr = this;
//            panelDict.Add(panelType, panel);
//            return panel;
//        }
//        else
//        {
//            Debug.Log("空");
//            return null;
//        }
//    }

//    /// <summary>
//    /// 初始化ui路径
//    /// </summary>
//    private void InitPanel()
//    {
//        string panelpath = "Panel/";
//        string[] path = new string[] { "MessagePanel", "StartPanel", "InputPanel","RoomListPanel", "RoomPanel", "GamePanel" };
//        panelPath.Add(PanelName.Message, panelpath + path[0]);
//        panelPath.Add(PanelName.Start, panelpath + path[1]);
//        panelPath.Add(PanelName.Input, panelpath + path[2]);
//        panelPath.Add(PanelName.RoomList, panelpath + path[3]);
//        panelPath.Add(PanelName.Room, panelpath + path[4]);
//    }

//    public void SetMessagePanel(MessagePanel message)
//    {
//        messagePanel = message;
//    }

//    public void ShowMessage(string str, bool sync = false)
//    {
//        messagePanel.ShowMessage(str, sync);
//    }

//    public BasePanel GetPanel(PanelName type)
//    {
//        return panelDict[type];
//    }



//}
