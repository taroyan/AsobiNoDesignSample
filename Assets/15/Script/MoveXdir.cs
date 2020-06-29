using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveXdir : MonoBehaviour
{
    public float length = 4.0f; // 移動する振幅
    public float speed = 2.0f;  // 移動するスピード　大きくすると早くなる
    private Vector3 startPos;   // ゲーム開始時のポジションを入れる変数
    public bool negative = false;   // 逆転フラグ　空のチェックボックスが表示されます

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position; // ゲーム開始時の位置
        if (negative == true)   // 逆転フラグあり?(Yes)
        {
            speed = -speed; // 逆向きのスピードになるようにする
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Mathf.Sin()でフレームごとの角度変化による値を出します。それに振幅をかけた値をstartPosのx座標に足します
        transform.position = new Vector3((Mathf.Sin((Time.time) * speed) * length + startPos.x), startPos.y, startPos.z);
    }
}
