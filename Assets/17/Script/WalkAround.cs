using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkAround : MonoBehaviour
{
    public Transform[] points;      // エネミーの巡回する目標となるポイント
    private int destPoint = 0;      // 巡回するポイントの順番
    private NavMeshAgent agent;     // NavMeshAgent型の変数　agentを用意
    public GameObject target;       // 追いかける「target」となるObjectを入れる変数（プレイヤー）
    private bool inArea = false;    // 索敵範囲（Trigger Collider内）に相手が入っているかのフラグを用意
    public float chaspeed = 0.05f;  // プライヤーを追いかけるときのスピード調整に使う数値を用意
    public Color origColor;         // エネミーキャラ本体の色

    public GameObject muzzlePoint;      // 弾を発射する場所
    public GameObject ball;             // 再セットする弾のオブジェクト
    public float speed = 30f;           // 弾のスピード
    private int attackTime = 0;         // 弾の発射までのカウント
    public int intvalTime = 30;         // 弾の発射する間隔


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   // 自身のNavMeshAgentを取得
        agent.autoBraking = false;              // autoBrakingをオフ
        GotoNextPoint();                        // 次の巡回位置へ
    }


    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f)     // もし目標ポイントまでの距離が0.5より短ければ・・
        {
            GotoNextPoint();    // GotoNextPointメソッドに飛びます
        }

        if (target.activeInHierarchy == false)  // プレイヤーがやられて消えた?(Yes)
        {
            GetComponent<Renderer>().material.color = origColor;    // このスクリプトがアタッチされているゲームオブジェクトのRendererの色をはじめの色（通常状態のキャラ色）を入れて色を戻します。
        }

        if (inArea == true) // 索敵範囲にプレイヤーがいる?(Yes)
        {
            // 弾の発射処理
            attackTime += 1;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * 20.0f);   // ゆっくり回転
            if (attackTime % intvalTime == 0)   // 余りがゼロ?(Yes)
            {
                EneCannonShot();    // 弾発射
            }
        }
    }

    /// <summary>
    /// エネミーの巡回先を決めるメソッド
    /// </summary>
    void GotoNextPoint()
    {
        // エネミーの巡回するポイントが特に設定されていない場合、
        // （個数（Length）が「0個」の場合）はどこにも向かっていきません。＝このメソッドから抜け（return）ます
        if (points.Length == 0)
            return;

        // 次のポイントをセット
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;    // 次の巡回点をセットしていく。（０→１→２・・・と巡回）
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
            inArea = true;  // エリアに入ったフラグオン
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
            GotoNextPoint();    // 次に巡回位置へ
        }
    }
}
