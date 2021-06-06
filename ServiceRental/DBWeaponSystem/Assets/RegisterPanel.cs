using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{
    public Button confirmBtn;
    public Button cancelBtn;

    public Text uname;
    public Text upassword;
    public Text utellphone;
    public Text utype;
    public Text uprofession;

    public GameObject noticePanel;
    // Start is called before the first frame update
    void Start()
    {
        confirmBtn.onClick.AddListener(CheckInput);
        cancelBtn.onClick.AddListener(Close);
    }

    public void CheckInput()
    {
        if (uname.text == "" || upassword.text == "")
        {
            //UIManager.Instance.ShowMessage("玩家名字不能为空！");
            Debug.Log("不能为空");
            return;
        }
        Register();
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void Register()
    {
        int tempType = 0;
        switch(utype.text)
        {
            case "客户":
                tempType = 0;
                break;
            case "自由职业者":
                tempType = 1;
                break;
            case "管理员":
                tempType = 2;
                break;
        }

        if (SqlOperator.AddUser(uname.text, upassword.text,tempType,utellphone.text,uprofession.text))
        {
            AppMgr.Instance.userName = uname.text;
            AppMgr.Instance.utype = utype.text;
            Debug.Log("注册成功");
            SceneManager.LoadScene(1);
        }
        else
        {
            noticePanel.SetActive(true);
            noticePanel.GetComponent<NoticePanel>().Notice("注册失败，该用户名已存在");
            Debug.Log("注册失败，该用户名已存在");
        }

    }
}
