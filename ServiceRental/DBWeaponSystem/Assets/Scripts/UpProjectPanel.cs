using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpProjectPanel : MonoBehaviour
{
    public Text pname;
    public Text pcontent;
    public Text ptime;
    public Text ptype;
    public Text pmoney;


    public string pno = "";
    public Button confirmBtn;
    public Button cancelBtn;
    //public bool isUpdate = false;


    void Start()
    {
        confirmBtn.onClick.AddListener(UpProject);
        cancelBtn.onClick.AddListener(BackChoose);
    }


    void Update()
    {

    }

    /// <summary>
    /// 上架武器时同时插入在售列表和武器表
    /// </summary>
    public void UpProject()
    {
        pno = DateTime.Now.TimeOfDay.ToString();
        Debug.Log("'" + pno + "','" + pname.text + "','" + pcontent.text + "','" + ptime.text + "','" + ptype.text + "'," + pmoney.text +
            "," + 0 + ",'" + AppMgr.Instance.userno + "'");
        if (SqlOperator.InsertSQL("project", "'" + pno + "','" + pname.text + "','" + pcontent.text + "','" + ptime.text + "','" + ptype.text + "'," + pmoney.text +
            "," + 0 + ",'" + AppMgr.Instance.userno + "'"))
        {
            //if (SqlOperator.InsertSQL(PanelType.soldlist.ToString(), "'" + wno + "','" + AppMgr.Instance.userno + "','" + wname.text + "','" + wtype.text + "'," + wnumber.text + "," + wprice.text + ""))
            //    BackChoose();
            //else
            //    Debug.Log("在售列表插入失败");
            Function.Instance.Notice("发布成功");
            BackChoose();

        }
        else
            Debug.Log("添加失败");

    }

    public void BackChoose()
    {
        Function.Instance.BackChoose();
    }
}
