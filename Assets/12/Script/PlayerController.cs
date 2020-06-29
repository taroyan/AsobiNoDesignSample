using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;  // スピードの大きさ
    public float brake = 0.1f;  // 減速の大きさ
    private Rigidbody rB;       // Cylinderに割り当てられているRigidbody
    private Vector3 rbVelo;     // 今の速度を入れる変数

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>(); // このスクリプトがアタッチされているゲームオブジェクトにアタッチされている「Rigidbody」を取得
    }

    // Update is called once per frame
    void Update()
    {
        rbVelo = Vector3.zero;      // 初期化するため毎回(0,0,0)を入れる
        float x = Input.GetAxis("Horizontal");  // 入力の平行を取得
        float z = Input.GetAxis("Vertical");    // 入力の垂直を取得
        rbVelo = rB.velocity;   // 今の速度をrbVeloに入れる
        rB.AddForce(x * speed - rbVelo.x * brake, 0, z * speed - rbVelo.z * brake, ForceMode.Impulse);  // 力を与える
    }
}
