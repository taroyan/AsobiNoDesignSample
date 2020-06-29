using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController04 : MonoBehaviour
{
    public float speed = 2.0f;  // スピードの大きさ
    public float brake = 0.1f;  // 減速の大きさ
    private Rigidbody rB;       // Cylinderに割り当てられているRigidbody
    private Vector3 rbVelo;     // 今の速度を入れる変数

    public Text goalText;   // ゴールのテキスト
    public bool goalOn;     // ゴールフラグ
    public ParticleSystem explosion;    // 爆発エフェクト
    public Text failText;   // 失敗テキスト
    private Vector3 height; // 落下判定用高さ

    public float jumpForce = 20.0f; // ジャンプの強さ
    public float turboForce = 2.0f; // 加速の強さ

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>(); // このスクリプトがアタッチされているゲームオブジェクトのRigidbodyを取得
        goalText.enabled = false;   // ゴールテキストを非表示へ
        goalOn = false; // ゴールフラグは初期値はfalse
        failText.enabled = false;   // 失敗テキストを非表示へ
    }

    // Update is called once per frame
    void Update()
    {
        if (goalOn == false)
        {
            rbVelo = Vector3.zero; // 初期化するため毎回(0,0,0)を入れる
            float x = Input.GetAxis("Horizontal"); // 入力の平行を取得
            float z = Input.GetAxis("Vertical"); // 入力の垂直を取得
            rbVelo = rB.velocity; // 今の速度をrbVeloに入れる
            rB.AddForce(x * speed - rbVelo.x * brake, 0, z * speed - rbVelo.z * brake, ForceMode.Impulse);  // 力を与える
        }

        // 永遠に落下しないように対策
        height = this.GetComponent<Transform>().position;
        if (height.y <= -3.0f)  // -3.0f以下?(Yes)
        {
            explosion.transform.position = this.transform.position;    // 爆発のポジションを衝突したオブジェクトのポジションにする
            this.gameObject.SetActive(false);   // このスクリプトがアタッチされているゲームオブジェクトを消す
            failText.enabled = true;    // 失敗テキストを表示
            explosion.Play();   // エフェクトを再生
        }
    }

    /// <summary>
    /// IsTriggerがついているオブジェクトに接触したとき
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal") // 接触したオブジェクトのタグが「Goal」?(Yes)
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);   // 接触先の色変え
            rB.AddForce(-rbVelo.x * 0.8f, 0, -rbVelo.z * 0.8f, ForceMode.Impulse);  // 自身に力を与える
            goalText.enabled = true;    // ゴールテキストを表示
            goalOn = true;  // ゴールフラグをtrueへ
        }

        if (other.gameObject.tag == "Jump") // 接触したオブジェクトのタグが「Jump」?(Yes)
        {
            rB.AddForce(0, jumpForce, 0, ForceMode.Impulse);    // 上方向(Y)へ力を与える
        }
    }

    /// <summary>
    /// IsTriggerがついているゲームオブジェクトに接触し続けているときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Turbo")    // 接触しているゲームオブジェクトのタグが「Turbo」?(Yes)
        {
            Vector3 vel = rB.velocity;  // 速度を取得
            rB.AddForce(vel.x * turboForce,0,vel.z * turboForce,ForceMode.Impulse); // 加速させるための力を与える
        }
    }

    /// <summary>
    /// IsTriggerがついていないオブジェクトに接触したとき
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Kill") // 衝突したオブジェクトのタグが「Kill」?(Yes)
        {
            explosion.transform.position = this.transform.position;    // 爆発のポジションを衝突したオブジェクトのポジションにする
            this.gameObject.SetActive(false);   // このスクリプトがアタッチされているゲームオブジェクトを消す
            failText.enabled = true;    // 失敗テキストを表示
            explosion.Play();   // エフェクトを再生
        }

        if (other.gameObject.tag == "Bounce")   // 衝突したオブジェクトのタグが「Bounce」?(Yes)
        {
            StartCoroutine("WaitKeyInput"); // コルーチンをスタート
        }
    }

    /// <summary>
    /// キー入力を待つコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitKeyInput()
    {
        this.gameObject.GetComponent<PlayerController04>().enabled = false; // このオブジェクトについている「PlayerController」スクリプトを消す
        
        yield return new WaitForSeconds(1.0f);  // 1秒待機

        this.gameObject.GetComponent<PlayerController04>().enabled = true;  // 「PlayerController」を復活させます

    }
}
