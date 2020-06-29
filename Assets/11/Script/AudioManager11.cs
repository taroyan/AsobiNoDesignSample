using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager11 : MonoBehaviour
{
    private AudioSource audio;      // オーディオソース

    public AudioClip sound01;       // オーディオクリップ
    public AudioClip sound02;       // 〃
    public AudioClip sound03;       // 〃

    void Start()
    {
        audio = gameObject.AddComponent<AudioSource>(); // AudioSourceコンポーネントを追加
    }


    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        GameObject gm = GameObject.Find("GameManager");     // ゲームオブジェクト名「GameManager」を取得

        if (gm.GetComponent<GameManager11>().IsInGame())    // gmにアタッチされている「GameManager11」スクリプトのIsInGame()関数の戻り値がTrue?(Yes)
        {

            if (other.gameObject.tag == "Player")    // タグ名が「Player」?(Yes)
            {
                audio.PlayOneShot(sound01);
            }
            else if (other.gameObject.tag == "Target")  // タグ名が「Target」?(Yes)
            {
                audio.PlayOneShot(sound02);
            }
            else
            {
                audio.PlayOneShot(sound03);
            }
        }
    }
}
