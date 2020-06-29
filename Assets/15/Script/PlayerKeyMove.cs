using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKeyMove : MonoBehaviour
{
    public float speed = 3.0f;  // スピードの大きさ
    public Text goalText;   // ゴールのテキスト
    public bool goalOn;     // ゴールフラグ
    public ParticleSystem explosion;    // 爆発エフェクト
    public Text failText;   // 失敗テキスト
    private Vector3 height; // 落下判定用高さ

    private Rigidbody rB;   // リジッドボディ

    // Start is called before the first frame update
    void Start()
    {
        goalText.enabled = false;   // ゴールテキストを非表示へ
        goalOn = false; // ゴールフラグは初期値はfalse
        failText.enabled = false;   // 失敗テキストを非表示へ

        rB = GetComponent<Rigidbody>(); // このスクリプトがアタッチされているゲームオブジェクトのRigidbodyを取得
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goalOn == false)    // ゴールフラグがfalse?(Yes)
        {
            var veloX = speed * Input.GetAxis("Horizontal");    // 水平方向のInputを取得
            var veloZ = speed * Input.GetAxis("Vertical");      // 垂直方向のInputを取得
            rB.MovePosition(new Vector3(veloX, 0f, veloZ) + rB.position);   // MovePositionでプレイヤーを動かす
        }
    }


    /// <summary>
    /// IsTriggerがついているオブジェクトに接触したとき
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal") // 接触したオブジェクトのタグが「Goal」?(Yes)
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);   // 色変え
            goalText.enabled = true;    // ゴールテキスト表示
            goalOn = true;  // ゴールフラグオン
        }
    }

    /// <summary>
    /// IsTriggerがついていないオブジェクトに接触したとき
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Kill") // 衝突したオブジェクトのタグが「Kill」?(Yes)
        {
            explosion.transform.position = this.transform.position;    // 爆発のポジションを衝突したオブジェクトのポジションにする
            this.gameObject.SetActive(false);   // このスクリプトがアタッチされているゲームオブジェクトを消す
            failText.enabled = true;    // 失敗テキストを表示
            explosion.Play();   // エフェクトを再生
        }
    }
}
