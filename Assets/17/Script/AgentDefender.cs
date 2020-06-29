using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentDefender : MonoBehaviour
{
    public Transform[] points;      // エネミーの巡回する目標となるポイント
    private int destPoint = 0;      // 巡回するポイントの順番
    private NavMeshAgent agent;     // NavMeshAgent型の変数　agentを用意
    public GameObject target;       // 追いかける「target」となるObjectを入れる変数（プレイヤー）
    private bool inArea = false;    // 索敵範囲（Trigger Collider内）に相手が入っているかのフラグを用意
    public float chaspeed = 0.05f;  // プライヤーを追いかけるときのスピード調整に使う数値を用意
    public Color origColor;         // エネミーキャラ本体の色


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   // 「NavMeshAgent」をagentへ読み込みます
        agent.autoBraking = false;              // autoBrakingは目的地でピタッと止まるか？止まらずに行き過ぎるなら、「false」にします
        GotoNextPoint();                        // GotoNextPoint()メソッドへ飛びます
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

        if (inArea == true && target.activeInHierarchy == true) // 索敵範囲にプレイヤーがいて、かつプレイヤーも存在しているときは、Agentの向かう方向にプレイヤーの位置を入れます
        {
            agent.destination = target.transform.position;  // ターゲット（プレイヤー）の位置を目標にセット
            EneChasing();   // EneChasing()メソッドに飛びます
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")   // 索敵範囲にプレイヤーが入った?(Yes)
        {
            inArea = true;  // 索敵範囲（Trigger Collider内）に相手が入っているかのフラグをTrue
            target = other.gameObject;  // Targetにプレイヤーの位置を入れます。これで向かう対象がプレイヤーになります
            GetComponent<Renderer>().material.color = new Color(255f/255f,65f/255f,26f/255f,255f/255f); // 本体の色を赤に（攻撃中）
            EneChasing();   // 追いかけるメソッド「EneChasing()」に飛びます
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")   // 索敵範囲からプレイヤーが出た?(Yes)
        {
            inArea = false; // 索敵範囲（Trigger Collider内）に相手が入っているかのフラグをFalse
            GetComponent<Renderer>().material.color = origColor;    // 色を戻す
            GotoNextPoint();    // 次のポイントへ向かわせます
        }
    }

    public void EneChasing()
    {
        // 現在位置に進行方向の単位ベクトルを足していくことで「目標」に近づいていきます。
        transform.position += transform.forward * chaspeed;
    }
}
