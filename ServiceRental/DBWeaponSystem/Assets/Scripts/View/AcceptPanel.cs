using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptPanel : MonoBehaviour
{

    public Button confirmBtn;
    public Button cancelBton;
    void Start()
    {
        confirmBtn.onClick.AddListener(Accept);
        cancelBton.onClick.AddListener(Cancel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Accept()
    {
        Function.Instance.Accept();
        this.gameObject.SetActive(false);
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }

}
