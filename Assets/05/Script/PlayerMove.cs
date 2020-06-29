using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 15f;   // 移動スピード

    private void FixedUpdate()
    {
        var velox = speed * Input.GetAxisRaw("Horizontal"); // 水平方向の入力値を取得（キーボードの左右矢印キーなど）
        GetComponent<Rigidbody>().velocity = new Vector3(velox,0f,0f);  // このスクリプトがアタッチされているゲームオブジェクトに速度を与える
    }
}
