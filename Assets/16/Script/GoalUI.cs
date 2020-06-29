using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalUI : MonoBehaviour
{
    public Text goalText;   // ゴールテキスト
    public Text textLose;   // 負けテキスト
    private bool gameOn;    // ゲーム中フラグ


    // Start is called before the first frame update
    void Start()
    {
        goalText.enabled = false;   // ゴールテキスト非表示
        textLose.enabled = false;   // 負けテキスト非表示
        gameOn = true;              // ゲーム中フラグオン
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rival" && gameOn == true)  // ライバルが接触　かつ　gameOnフラグがtrue?(Yes)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0,1);
            GameObject.Find("Player").GetComponent<PlayerController16>().goalOn = true;
            textLose.enabled = true;
            gameOn = false;
        }

        if (other.gameObject.tag == "Player" && gameOn == true) // プレイヤーが接触　かつ　gameOnフラグがtrue?(Yes)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1); // 色変え
            goalText.enabled = true;    // ゴールテキストを表示
            gameOn = false;             // ゲーム中フラグオフ
        }
    }
}
