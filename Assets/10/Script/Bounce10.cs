using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce10 : MonoBehaviour
{
    public float bounce = 5.0f; // 跳ねる力
    public int scorepoint = 10; // スコアポイント

    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") // タグ名が「Ball」?(Yes)
        {
            Vector3 norm = other.contacts[0].normal;    // 一回目の接触場所の法線を取得
            Vector3 vel = -other.rigidbody.velocity;    // 方向を逆方向へ（マイナスをかけて反転）
            vel += new Vector3(-norm.x * bounce,0f,-norm.z * bounce);   // X,Zの法線方向の速度を+=によって加算している
            other.rigidbody.AddForce(vel,ForceMode.VelocityChange); // 衝突物に力を与える
            GameObject gm = GameObject.Find("GameManager");         // 「GamaManager」オブジェクトを取得
            gm.GetComponent<GameManager10>().AddScore(scorepoint);  // gmのゲームオブジェクトにアタッチされている「GameManager08」スクリプト内のAddScore()関数を呼び出し
        }
    }
}
