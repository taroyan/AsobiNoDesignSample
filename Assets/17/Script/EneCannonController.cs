using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneCannonController : MonoBehaviour
{
    public GameObject muzzlePoint;      // 弾を発射する場所
    public GameObject ball;             // 再セットする弾のオブジェクト
    public float speed = 30f;           // 弾のスピード
    private int attackTime = 0;         // 弾の発射までのカウント
    public int intvalTime = 30;         // 弾の発射する間隔


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
        Vector3 mballPos = muzzlePoint.transform.position;  // 発射ポイントをローカル変数に保持
        GameObject newBall = Instantiate(ball, mballPos, transform.rotation);   // muzzlePointの位置に、instantiateで「ball」Prefabオブジェクトを出現させます
        Vector3 dir = newBall.transform.forward;    // 出現したボールのforward(z軸)方向を読み込みます
        newBall.GetComponent<Rigidbody>().AddForce(dir * speed,ForceMode.Impulse);  // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加えます
        newBall.name = ball.name;   // ゲームオブジェクトの名前をセット
        Destroy(newBall,0.8f);  // 0.8秒後にnewBallオブジェクトを消します
    }
}
