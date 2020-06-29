using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")   // タグが「Player」?(Yes)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);    // 自身の色変え
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")   // タグが「Player」?(Yes)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);    // 自身の色変え
        }
    }
}
