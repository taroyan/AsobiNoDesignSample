using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;  // オーディオソース

    public AudioClip sound01;   // オーディオクリップ
    public AudioClip sound02;   // 〃
    public AudioClip sound03;   // 〃

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
        if (other.gameObject.tag == "Blue") // タグ名が「Blue」?(Yes)
        {
            audio.PlayOneShot(sound01);
        }
        else if (other.gameObject.tag == "Block")   // タグ名が「Block」?(Yes)
        {
            audio.PlayOneShot(sound02);
        }
        else
        {
            audio.PlayOneShot(sound03);
        }
    }
}
