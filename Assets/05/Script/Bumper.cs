using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bounce = 10f;  // はねる力

    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") // 衝突したゲームオブジェクトのタグ名が「Ball」?(Yes)
        {
            // 衝突したゲームオブジェクトに力を加える
            other.rigidbody.AddForce(0f,bounce/6,bounce,ForceMode.Impulse);
        }
    }
}
