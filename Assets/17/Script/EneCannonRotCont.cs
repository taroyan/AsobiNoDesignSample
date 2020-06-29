using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneCannonRotCont : MonoBehaviour
{
    public GameObject muzzlePoint;      // 弾を発射する場所
    public GameObject ball;             // 再セットする弾のオブジェクト
    public float speed = 30f;           // 弾のスピード
    private int attackTime = 0;         // 弾の発射までのカウント
    public int intvalTime = 30;         // 弾の発射する間隔
    public GameObject target;           // 目標となるオブジェクト（つまりプレイヤー）を入れます
    private bool inArea = false;        // 索敵範囲内にいる、いないのフラグ
    public Color origColor;             // もとの色を用意します


    // Update is called once per frame
    void Update()
    {
        // 弾の発射処理
        attackTime += 1;
        if (attackTime % intvalTime == 0)   // 余りがゼロ?(Yes)
        {
            EneCannonShot();    // 弾発射
        }
    }

    /// <summary>
    /// 弾発射
    /// </summary>
    public void EneCannonShot()
    {
        if (target.activeInHierarchy == false)  // プレイヤーが非表示＝やられた状態?(Yes)
        {
            GetComponent<Renderer>().material.color = origColor;    // 元の色に戻す
            inArea = false; // 索敵範囲内にプレイヤーがいない
        }

        if (inArea == true) // 索敵範囲内にプレイヤーがいる?(Yes)
        {
            Vector3 mballPos = muzzlePoint.transform.position; // 発射ポイントをローカル変数に保持
            GameObject
                newBall = Instantiate(ball, mballPos,
                    transform.rotation); // muzzlePointの位置に、instantiateで「ball」Prefabオブジェクトを出現させます
            Vector3 dir = newBall.transform.forward; // 出現したボールのforward(z軸)方向を読み込みます
            newBall.GetComponent<Rigidbody>()
                .AddForce(dir * speed, ForceMode.Impulse); // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加えます
            newBall.name = ball.name; // ゲームオブジェクトの名前をセット
            Destroy(newBall, 0.8f); // 0.8秒後にnewBallオブジェクトを消します

        }
    }

    /// <summary>
    /// 索敵範囲内にプレイヤーが居続けている間ずっと呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   // タグが「Player」?(Yes)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * 3.0f);    // ゆっくり回転

            inArea = true;  // エリアに入ったフラグをオン
            target = other.gameObject;  // 接触したゲームオブジェクトをターゲットにする

            GetComponent<Renderer>().material.color = new Color(255f / 255f, 65f / 255f, 26f / 255f, 255f / 255f);  // 戦闘時の色に変更
        }
    }

    /// <summary>
    /// 索敵範囲内からプレイヤーが外に出たときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")   // タグが「Player」?(Yes)
        {
            inArea = false; // 索敵範囲にいないとフラグを立てる
            GetComponent<Renderer>().material.color = origColor;    // 色をもとの色に戻します
        }
    }
}
