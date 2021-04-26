using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* 操作方法
    
        移動      - 十字キー左右
        ジャンプ  - SPACE
    */

    // ステート
    public enum CharactorState
    {
        STATE_NORMAL, // 通常状態
        STATE_WARP,   // ワープ状態
    };

    // パラメータ
    public float _Speed         = 0.0f;                             // キャラクターの移動速度
    public float _MaxSpeed      = 5.0f;
    public float _JumpPower     = 10.0f;                            // ジャンプ力
    public float _Rate          = 0.1f;
    public float _Gravity       = 20.0f;                            // 重力の大きさ
    public Vector3 _DefaultPos  = new Vector3(0.0f, 0.0f, 0.0f);    // プレイヤーの初期位置
    public bool _IsFront        = true;                             // 表ステージかどうか

    private CharacterController _Controller;         // コンポーネントの取得
    private Vector3 _MoveDirection = Vector3.zero;   // キャラクターの移動量
    private float _H;                                // キー入力取得用
    private CharactorState _CharactorState;          // キャラクターステート
    private GameObject _TMPAerial;
    

    // メンバ関数
    //************************************************
    //  キャラクターのステートセットする関数
    //  引数には列挙型を入れる
    //
    //  public enum CharactorState
    //  {
    //      STATE_NORMAL, // 通常状態
    //      STATE_WARP,   // ワープ状態
    //  };
    //************************************************
    public void SetState(CharactorState s)
    {
        // 現在のステートを変更
        this._CharactorState = s;

        // 各状態ごとの初期処理
        if(s == CharactorState.STATE_NORMAL)// 通常状態
        {
            _Controller.enabled = true;      // CharactorController有効化
        }
        else if(s == CharactorState.STATE_WARP)// ワープ状態
        {
            _Controller.enabled = false;     // CharactorController無効化
            _MoveDirection = Vector3.zero;   // 移動量をゼロにする
        }
    }

    //************************************************
    //  表ステージかどうかの変数を変える関数
    //  引数にはbool型を入れる
    //************************************************
    public void SetIsFront(bool f)
    {
        _IsFront = f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // CharactorControllerのコンポーネントを取得
        _Controller = GetComponent<CharacterController>();

        // CharactorStateの初期化
        _CharactorState = CharactorState.STATE_NORMAL;   // 初期値を表に設定

        _TMPAerial = GameObject.Find("Aerial");
    }

    // Update is called once per frame
    void Update()
    {
        if(_CharactorState == CharactorState.STATE_NORMAL)// 通常状態
        {
            // キー入力取得
            _H = Input.GetAxis("Horizontal");    // 値の範囲(-1.0f~1.0f)
                                                 //if (Input.GetKey(KeyCode.A))
                                                 //{
                                                 //    _Speed -= _Rate * Time.deltaTime;
                                                 //    if (Mathf.Abs(_Speed) > _MaxSpeed)
                                                 //        _Speed = -_MaxSpeed;
                                                 //}
                                                 //else if (Input.GetKey(KeyCode.D))
                                                 //{
                                                 //    _Speed += (_MaxSpeed / _Rate) * Time.deltaTime;
                                                 //    if (_Speed > _MaxSpeed)
                                                 //        _Speed = _MaxSpeed;
                                                 //}




            // キャラクターの移動
            if (_Controller.isGrounded)// キャラクターが地面についているとき
            {
                _MoveDirection = new Vector3(_H, 0.0f, 0.0f);                   // キー入力でx成分のみ移動量に加える
                //_MoveDirection = transform.TransformDirection(_MoveDirection);
                _MoveDirection *= _Speed;                                       // キャラクターの設定スピードを乗算

                // ジャンプ
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))// SPACEキーが押されたとき
                {
                    _MoveDirection.y = _JumpPower;  // y成分にキャラクターのジャンプ力を加算
                }

                // 表と裏の変更
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (GameObject.Find("WorldMgr").GetComponent<WorldMgr>().GetWorldState() == WorldMgr.WorldState.STATE_FRONT)
                    {
                        GameObject.Find("WorldMgr").GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_BACK);
                    }
                    else
                    {
                        GameObject.Find("WorldMgr").GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_FRONT);
                    }
                }

                // 足場の変更（簡易実装）
                if(Input.GetKeyDown(KeyCode.C))
                {
                    if (GameObject.Find("WorldMgr").GetComponent<WorldMgr>().GetWorldState() == WorldMgr.WorldState.STATE_FRONT)
                    {
                        if(GameObject.Find("AerialMgr").GetComponent<TMPAerialController>().GetAerialState() == TMPAerialController.AerialState.STATE_FRONT)
                        {
                            GameObject.Find("AerialMgr").GetComponent<TMPAerialController>().SetAerialState(TMPAerialController.AerialState.STATE_BACK);
                        }
                    }
                    else
                    {
                        if(GameObject.Find("AerialMgr").GetComponent<TMPAerialController>().GetAerialState() == TMPAerialController.AerialState.STATE_BACK)
                        {
                            GameObject.Find("AerialMgr").GetComponent<TMPAerialController>().SetAerialState(TMPAerialController.AerialState.STATE_FRONT);
                        }
                    }
                }
            }
            else if(!_Controller.isGrounded)
            {
                _MoveDirection.x = _H;                                          // キー入力でx成分のみ移動量に加える
                //_MoveDirection = transform.TransformDirection(_MoveDirection);
                _MoveDirection.x *= _Speed;                                     // キャラクターの設定スピードを乗算

                Debug.Log("a");
            }

            // 慣性
            //_MoveDirection.x -= _Rate * Time.deltaTime;

            // 重力設定
            _MoveDirection.y -= _Gravity * Time.deltaTime;
            _Controller.Move(_MoveDirection * Time.deltaTime);
        }
        else if(_CharactorState == CharactorState.STATE_WARP)    // ワープ状態
        {
            transform.position = _DefaultPos;       // 初期座標に移動
            SetState(CharactorState.STATE_NORMAL);  // 通常状態に移行
        }

        
        

    }//Update

}
