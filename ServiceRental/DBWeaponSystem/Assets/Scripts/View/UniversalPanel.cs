using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PanelType
{
    none,
    userlist,
    customer,
    freer,
    project,
    record,
    timelist
    //    ,
    //makeProject
}
public class UniversalPanel : MonoBehaviour
{
    public Button findBtn;
    public Button filterBtn;
    public Button tongjiBtn;
    public GameObject tongjiBtnObject;
    public Button backBtn;
    public Text title;
    public Text notice;
    public Text input;
    public PanelType panelType=PanelType.none;
    public GameObject messageBtnPre;

    public GameObject viewArea;
    public List<string> message = new List<string>();
    public GameObject acceptPanel;
    public int operatorNum=-1; //操作了第几个物品

    public GameObject soldShaixuanPanel;
    public GameObject confirmPanel;

    //购物
    public int BtnNum = 0;


    private void Awake()
    {
        messageBtnPre=Resources.Load("Prefabs/MessageBtn") as GameObject;

    }

    void Start()
    {
        findBtn.onClick.AddListener(Find);
        filterBtn.onClick.AddListener(Filter);
        tongjiBtn.onClick.AddListener(TongJi);
        backBtn.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Init(PanelType type)
    {
        panelType = type;
        Reflesh();
    }

    public void Reflesh()
    {
        filterBtn.GetComponentInChildren<Text>().text = "筛选";
        BtnNum = 0;
        if (panelType != PanelType.none)
        {
            switch (panelType)
            {
                case PanelType.userlist:
                    notice.text = "输入用户编号";
                    title.text = "用户信息";
                    ReadAndPrintSql(panelType.ToString(), "用户ID   用户密码   用户名   用户类型","uno='" + AppMgr.Instance.userno + "'");
                    break;

                case PanelType.customer:
                    notice.text = "输入客户编号";
                    title.text = "客户列表";
                    ReadAndPrintSql(panelType.ToString(), "客户编号   客户名   客户等级   客户电话");
                    break;
                case PanelType.freer:
                    notice.text = "输入自由职业者编号";
                    title.text = "自由职业者列表";
                    ReadAndPrintSql(panelType.ToString(), "服务商编号   服务商名  职业类型  电话");
                    break;
                case PanelType.project:
                    notice.text = "输入项目编号";
                    title.text = "项目列表";
                    //tongjiBtnObject.SetActive(true);
                    ReadAndPrintSql(panelType.ToString(), "项目编号 项目名 项目内容 项目时间 要求职业 报酬 客户ID","pfind = 0");
                    break;
                case PanelType.record:
                    notice.text = "输入合同编号";
                    title.text = "合同记录列表";
                    ReadAndPrintSql(panelType.ToString(), "合同编号 项目编号 客户编号  服务商编号 时间");
                    break;
                case PanelType.timelist:
                    notice.text = "请输入项目编号";
                    title.text = "时间表";
                    //tongjiBtnObject.SetActive(true);
                    filterBtn.GetComponentInChildren<Text>().text = "修改时间";
                    ReadAndPrintSql(PanelType.record.ToString(), "项目编号  任务时间","fno='" + AppMgr.Instance.userno + "'","pno,rtime");
                    break;

            }
        }
    }

    public void Find()
    {
        switch (panelType)
        {
            case PanelType.userlist:
                notice.text = "输入用户编号";
                ReadAndPrintSql(panelType.ToString(), "用户ID   用户名   用户密码   用户余额", "uno='" + input.text+"'");
                break;

            case PanelType.customer:
                notice.text = "输入客户编号";
                ReadAndPrintSql(panelType.ToString(), "客户编号   客户名   客户等级   客户电话", "cno='" + input.text + "'");
                break;
            case PanelType.freer:
                notice.text = "输入服务商编号";
                ReadAndPrintSql(panelType.ToString(), "服务商编号   服务商名  职业类型  电话", "fno='" + input.text + "'");
                break;
            case PanelType.project:
                notice.text = "输入项目编号";
                ReadAndPrintSql(panelType.ToString(), "项目编号 项目名 项目内容 项目时间 要求职业 报酬 客户ID", "pno='" + input.text + "'");
                break;
            case PanelType.record:
                notice.text = "输入合同编号";
                ReadAndPrintSql(panelType.ToString(), "合同编号 项目编号 客户编号  服务商编号 时间", "rno='" + input.text + "'");
                break;
            case PanelType.timelist:
                notice.text = "输入项目编号";
                ReadAndPrintSql(PanelType.record.ToString(), "项目编号 项目名 项目内容 项目时间 要求职业 报酬 客户ID", "pno='" + input.text + "'");
                break;
        }
    }

    public void Filter()
    {
        switch (panelType)
        {
            case PanelType.userlist:
                notice.text = "输入用户编号";
                ReadAndPrintSql(panelType.ToString(), "用户ID   用户名   用户密码   用户余额");
                break;

            case PanelType.customer:
                notice.text = "输入武器编号";
                ReadAndPrintSql(panelType.ToString(), "武器编号   武器名   武器类型   武器描述   武器稀有度");
                break;

            case PanelType.freer:
                soldShaixuanPanel.SetActive(true);
                break;

            case PanelType.project:
                notice.text = "输入订单编号";
                ReadAndPrintSql(panelType.ToString(), "订单编号 买家ID 卖家ID 武器编号 类型 数量 总金额");
                break;
            case PanelType.record:
                notice.text = "输入记录编号";
                ReadAndPrintSql(panelType.ToString(), "记录编号 武器编号 卖家ID  数量 武器价格");
                break;
            case PanelType.timelist:
                //Function.Instance.UpWeaponlist();
                notice.text = "请输入项目编号";
                ReadAndPrintSql(PanelType.record.ToString(), "项目编号  任务时间", "fno='" + AppMgr.Instance.userno + "'", "pno,rtime");
                break;

        }
    }

    public void TongJi()
    {
        //switch (panelType)
        //{
        //    case PanelType.orderlist:
        //        //notice.text = "输入订单编号";
        //        //ReadAndPrintSql(panelType.ToString(), "订单 买家ID 卖家ID 武器编号 类型 数量 总金额");
        //        break;
        //}
    }


    public void Back()
    {
        panelType = PanelType.none;
        Function.Instance.BackChoose();
    }

    private void ReadAndPrintSql(string tableName,string notice,string condition=null,string target=null)
    {
        message.Clear();
        for(int i = 0; i < viewArea.transform.childCount; i++)
        {
            viewArea.transform.GetChild(i).gameObject.SetActive(false);
        }
        message = SqlOperator.SelectSQL(tableName,notice,condition,target);
        foreach(string str in message)
        {
            GameObject messageBtn = Instantiate(messageBtnPre);
            messageBtn.GetComponentInChildren<Text>().text= str;
            messageBtn.GetComponentInChildren<MessageButton>().btnNum = BtnNum;
            messageBtn.transform.parent = viewArea.transform;
            AddLong();
            BtnNum++;
        }
      
    } 

    public string GetValueFromSql(string targetTable,string condition,string target)
    {
        message.Clear();
        message = SqlOperator.SelectSQL(targetTable, null, condition,target);
        return message[0];
    }

    void AddLong()
    {
        viewArea.GetComponent<RectTransform>().sizeDelta = 
            new Vector2(viewArea.GetComponent<RectTransform>().sizeDelta.x, viewArea.GetComponent<RectTransform>().sizeDelta.y+50);
    }



    public void HitButton(int num)
    {
        switch (panelType)
        {
            case PanelType.userlist:

                break;

            case PanelType.customer:

                break;
            case PanelType.freer:
                
                break;
            case PanelType.project:
                operatorNum = num;
                acceptPanel.SetActive(true);
                break;
            case PanelType.record:

                break;
            case PanelType.timelist:
                //operatorNum = num;
                //confirmPanel.SetActive(true);
                //confirmPanel.GetComponent<ConfirmPanel>().notice.text = "确认是否下架商品";
                break;
        }
    }

    public void AcceptProject()
    {
        if (operatorNum >= 0)
        {
            ProjectModel project = new ProjectModel(message[operatorNum]);
            string tempprofession = SqlOperator.SelectSQL("freer", null, "fname='" + AppMgr.Instance.userName + "'", "ftype")[0];
            string profession = "";
            for(int i=0;i<tempprofession.Length;i++)
            {
                if (tempprofession[i] != ' ') profession += tempprofession[i];
            }
            if (project.ptype != profession)
            {
                Function.Instance.Notice("职业不匹配,要求:"+ project.ptype+"   你的职业是:"+profession);
                Debug.Log(project.ptype+"!="+ profession+".");
                return;
            }
            else
                Debug.Log("匹配");
            SqlOperator.UpdateSQL("project", "pfind=" + 1, "pno='" + project.pno+"'");

            SqlOperator.InsertSQL("record", "'" + DateTime.Now.TimeOfDay.ToString()
                        + "','" + project.pno + "','" + project.cno + "','"
                        + AppMgr.Instance.userno + "','" + project.ptime + "'");
            Function.Instance.Notice("签订成功");

            Reflesh();
        }
            

        //SqlOperator.InsertSQL("record", "'" + DateTime.Now.TimeOfDay.ToString()
        //                + "','" + AppMgr.Instance.userno + "','" + sold.sno + "','"
        //                + sold.wno + "'," + buyNum + "," + payMoney + "");
    }


    public void BuyConfirm(int buyNum)
    {
        if(operatorNum>=0)
        {
            SoldListModel sold = new SoldListModel(message[operatorNum]);
            if(AppMgr.Instance.userno==sold.sno)
            {
                Function.Instance.Notice("不能买自己的东西，购买失败");
                return;
            }
            float payMoney = buyNum * sold.wprice;
            float userMoney = Function.Instance.GetUserBalance();
            if (sold.wnumber>=buyNum)
            {
                if(userMoney>=payMoney)
                {
                    Function.Instance.Notice("购买成功");
                    //Debug.Log(sold.wname);
                    //更新用户表,在售表,生成订单
                    SqlOperator.UpdateSQL("userlist", "ubalance = " + 
                        (userMoney - payMoney), "uno='" + AppMgr.Instance.userno + "'");
                    SqlOperator.UpdateSQL("userlist", "ubalance = " + 
                        (userMoney + payMoney), "uno='" + sold.sno + "'");
                    SqlOperator.UpdateSQL("soldlist", "wnumber = " + 
                        (sold.wnumber - buyNum), "wno= '" + sold.wno + 
                        "' and sno='" + sold.sno + "'");
                    SqlOperator.InsertSQL("orderlist", "'" + DateTime.Now.TimeOfDay.ToString() 
                        + "','" + AppMgr.Instance.userno + "','" + sold.sno + "','" 
                        + sold.wno + "'," + buyNum + "," + payMoney + "");   
                }
                else
                {
                    Function.Instance.Notice("余额不足，购买失败");
                }
                
            }
            else
            {
                Function.Instance.Notice("超出剩余数量，购买失败");

            }
            Reflesh();
        }
    }

    public void SoldShaixuan(string upDown,string type)
    {
        soldShaixuanPanel.SetActive(false);
        message.Clear();
        for (int i = 0; i < viewArea.transform.childCount; i++)
        {
            viewArea.transform.GetChild(i).gameObject.SetActive(false);
        }
        string strtemp = "";
        //asc表示升序，desc表示降序
        if (upDown=="价格升序")
        {
            strtemp = "asc";
        }
        else
        {
            strtemp = "desc";
        }
        message = SqlOperator.SelectSQL("soldlist", "武器编号  卖家ID  武器名  类型  数量  武器价格", "wtype='"+type+"' order by wprice "+strtemp);
        foreach (string str in message)
        {
            GameObject messageBtn = Instantiate(messageBtnPre);
            messageBtn.GetComponentInChildren<Text>().text = str;
            messageBtn.GetComponentInChildren<MessageButton>().btnNum = BtnNum;
            messageBtn.transform.parent = viewArea.transform;
            AddLong();
            BtnNum++;
        }
    }

    public void DownWeapon(bool isDown)
    {
        if(isDown)
        {
            SoldListModel sold = new SoldListModel(message[operatorNum]);
            SqlOperator.DeleteSQL("soldlist", "wno='" + sold.wno + "' and sno = '" + sold.sno + "'");
        }
        confirmPanel.SetActive(false);
       
    }
}
