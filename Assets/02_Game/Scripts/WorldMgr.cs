using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMgr : MonoBehaviour
{
    // ステート
    public enum WorldState
    {
        STATE_FRONT,    // 表ステージ
        STATE_BACK,     // 裏ステージ
    };

    // パラメータ
    public GameObject _Front, _Back;  // オブジェクト取得用
    public WorldState _WorldState;    // ステート

    // メンバ関数
    public void SetWorldState(WorldState ws)
    {
        // 現在のステートを変更
        _WorldState = ws;

        // 各ステートごとの初期処理
        if(ws == WorldState.STATE_FRONT)
        {
            _Front.SetActive(true);
            _Back.SetActive(false);
        }
        else if(ws == WorldState.STATE_BACK)
        {
            _Front.SetActive(false);
            _Back.SetActive(true);
        }
    }


    public WorldState GetWorldState()
    {
        return _WorldState;
    }

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクト取得
        _Front  = GameObject.Find("front1");    // 表ステージを取得
        _Back   = GameObject.Find("back1");     // 裏ステージを取得

        // ステージ初期化
        _Front.SetActive(true);
        _Back.SetActive(false);

        // ステート初期化
        _WorldState = WorldState.STATE_FRONT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
