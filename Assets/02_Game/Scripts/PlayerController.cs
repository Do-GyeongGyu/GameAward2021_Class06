using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
        /* 操作方法
    
        移動      - 十字キー左右
        ジャンプ  - SPACE
    */

    // パラメータ
    public float Speed     = 5.0f;     // キャラクターの移動速度
    public float JumpSpeed = 10.0f;    // ジャンプ力
    public float Gravity   = 20.0f;    // 重力の大きさ

    private CharacterController Controller;        // コンポーネントの取得
    private Vector3 MoveDirection = Vector3.zero;  // キャラクターの移動量
    private float h;                                // キー入力取得用
    private Vector3 CurrentPos = Vector3.zero;
    private Vector3 PosChange = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        // CharactorControllerのコンポーネントを取得
        Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPos = this.transform.position;
        PosChange = new Vector3(CurrentPos.x, CurrentPos.y, 0.0f);
        // キー入力取得
        h = Input.GetAxis("Horizontal");    // 値の範囲(-1.0f~1.0f)

        // キャラクターの移動
        if (Controller.isGrounded)// キャラクターが地面についているとき
        {
            MoveDirection = new Vector3(h, 0.0f, 0.0f);                    // キー入力でx成分のみ移動量に加える
            MoveDirection = transform.TransformDirection(MoveDirection);  // キャラクターの移動に慣性をかける
            MoveDirection *= Speed;                                       // キャラクターの設定スピードを乗算

            if((CurrentPos.z == -1.0f) && Input.GetKey(KeyCode.Z))
            {
                PosChange.z = 1.5f;
                //Controller.transform = PosChange;

               // PosChange.z = 1.5f;
                //CurrentPos.position = PosChange;
            }
            else if((CurrentPos.z == 1.5f) && Input.GetKey(KeyCode.Z))
            {
                PosChange.z = -1.0f;
                Controller.Move(PosChange);

                // PosChange.z = -1.0f;
                // CurrentPos.position = PosChange;
            }    

            // ジャンプ
            if(Input.GetKey(KeyCode.Space))
            {
                MoveDirection.y = JumpSpeed;  // y成分にキャラクターのジャンプ力を加算
            }
        }

        // 重力設定
        MoveDirection.y -= Gravity * Time.deltaTime;
        Controller.Move(MoveDirection * Time.deltaTime);

    }//Update

}
