using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RbXdir : MonoBehaviour
{
    public float length = 4.0f; // 移動する振幅
    public float speed = 2.0f;  // 移動するスピード　大きくすると早くなる
    private Vector3 startPos;   // ゲーム開始時のポジションを入れる変数
    public bool negative = false;   // 逆転フラグ    空のチェックボックスが表示されます

    private Rigidbody rB;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position; // ゲーム開始時の位置

        rB = GetComponent<Rigidbody>(); // 自身のRigidbodyを取得
        
        if (negative == true)   // 逆転あり?(Yes)
        {
            speed = -speed; // 逆向きのスピードになるようにする
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // MovePosition()を使うと接触したときにRigidbodyを介して、物理的な挙動を周りのrigidbodyを持つオブジェクトに影響させることができます。
        rB.MovePosition(new Vector3((Mathf.Sin((Time.time) * speed) * length + startPos.x), startPos.y, startPos.z));
    }
}
