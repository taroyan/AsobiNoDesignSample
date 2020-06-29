using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoalDestination : MonoBehaviour
{
    public Transform goal;  // ゴールポジション

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();  // 自身のNavMeshAgentを取得
        agent.destination = goal.position;  // 目的地をゴールポジションへ
    }


}
