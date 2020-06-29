using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") // 衝突したゲームオブジェクトのタグ名が「Ball」?(Yes)
        {
            Destroy(gameObject, 0.2f);  // 0.2秒後にこのスクリプトがアタッチされているゲームオブジェクトを破棄
        }
    }
}
