using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float countTime;//時間を格納
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countTime -= Time.deltaTime;//経過時間を引いてる

        if (countTime < 0) countTime = 0;//0未満で0を格納

        GetComponent<Text>().text = countTime.ToString("F2");//テキストに表示　F2は小数点第2まで
      
    }
}
