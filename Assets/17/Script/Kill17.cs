using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill17 : MonoBehaviour
{
    public ParticleSystem explosion;    // 爆発エフェクト

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")   // タグが「Player」?(Yes)
        {
            explosion.transform.position = other.transform.position;    // 爆発のポジションを衝突したオブジェクトのポジションにする
            explosion.Play();   // エフェクトを再生

            other.gameObject.SetActive(false);  // 衝突したゲームオブジェクト（プレイヤー）を非表示へ
        }
    }
}
