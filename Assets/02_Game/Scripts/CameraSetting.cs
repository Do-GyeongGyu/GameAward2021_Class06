using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    private GameObject Main;
    private GameObject Sub;
    private bool SubCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        Main = GameObject.Find("Main Camera");
        Sub = GameObject.Find("Sub Camera");

        Sub.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SubCheck == false && Input.GetKeyDown(KeyCode.Z))
        {
            //サブカメラをアクティブに設定
            Main.SetActive(false);
            Sub.SetActive(true);
            SubCheck = true;
        }
        else if (SubCheck == true && Input.GetKeyDown(KeyCode.Z))
        {
            //メインカメラをアクティブに設定
            Main.SetActive(true);
            Sub.SetActive(false);
            SubCheck = false;
        }
    }
}
