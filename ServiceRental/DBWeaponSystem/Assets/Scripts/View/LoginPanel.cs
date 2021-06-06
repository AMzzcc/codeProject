using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public Button confirmBtn;
    public Button cancelBtn;
    public InputField userName;
    public InputField password;
    public Text utype;

    public GameObject noticePanel;

    void Start()
    {
        confirmBtn.onClick.AddListener(CheckInput);
        cancelBtn.onClick.AddListener(Close);
        //noticePanelPre=Resources.Load("Prefabs/NoticePanel") as GameObject;
    }

    public void CheckInput()
    {
        if (userName.text == "" || password.text == "")
        {
            //UIManager.Instance.ShowMessage("玩家名字不能为空！");
            Debug.Log("不能为空");
            return;
        }
        Login();
    }

    public void Login()
    {
        if (SqlOperator.IsMatchAccount(userName.text, password.text))
        {
            AppMgr.Instance.userName = userName.text;
            List<string>str=SqlOperator.SelectSQL("userlist",null,"uname='"+userName.text+"'","uno");
            AppMgr.Instance.userno = str[0];
            Debug.Log(str[0]);
            AppMgr.Instance.utype = utype.text;

            //GameObject noticePanel = Instantiate(noticePanelPre);
            //noticePanel.GetComponent<NoticePanel>().Notice("登陆成功");
            //noticePanel.transform.parent = GameObject.Find("Canvas").transform;
            //Debug.Log("登陆成功");
            SceneManager.LoadScene(1);
        }
        else
        {
            noticePanel.SetActive(true);
            noticePanel.GetComponent<NoticePanel>().Notice("登陆失败");
            Debug.Log("登陆失败");
        }



        //UIManager.Instance.PopPanel(PanelType.normalPanel);
        //UIManager.Instance.ShowMessage(inputText.text);
    }


    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}