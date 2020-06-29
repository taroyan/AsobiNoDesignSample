using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounce = 10.0f;    // 跳ねる力

    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") // タグ名が「Ball」?(Yes)
        {
            Vector3 norm = other.contacts[0].normal;    // 一回目の接触場所の法線を取得
            Vector3 vel = new Vector3(-norm.x, 0f, -norm.z);    // XとZ方向を逆方向にする
            other.rigidbody.AddForce(vel.normalized * bounce, ForceMode.VelocityChange);    // 衝突物に力を与える
        }
    }
}
