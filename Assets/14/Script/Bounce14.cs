using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce14 : MonoBehaviour
{
    public float bounce = 10.0f;    // 跳ねる力

    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")   // タグ名が「Ball」?(Yes)
        {
            Vector3 norm = other.contacts[0].normal;    // 一回目の接触場所の法線を取得
            Vector3 vel = other.rigidbody.velocity.normalized;  // 衝突物の速度を規格化（ノーマライズ）
            vel += new Vector3(-norm.x * 2, 0f, -norm.z * 2);   // X,Zの法線方向の速度を+=によって加算している
            other.rigidbody.AddForce(vel * bounce, ForceMode.Impulse);  // 衝突物に力を与える
        }

    }
}
