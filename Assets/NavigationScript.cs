using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] BoxCollider m_AttackCollider;
    [SerializeField] float m_AttackCoolDown = 3f;

    [Header("References")]
    NavMeshAgent m_Agent;
    Animator m_Animator;
    List<GameObject> m_TargetObjectList;

    float m_LastAttackTime = 0f;

    void Start()
    {
        // 必要なコンポーネントを取得
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        if (m_AttackCollider == null)
        {
            Debug.LogError("Attack Collider is not assigned!");
            return;
        }
        m_AttackCollider.isTrigger = true;
        m_AttackCollider.enabled = false;
        // プレイヤーを取得
        m_TargetObjectList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        if (m_TargetObjectList.Count == 0)
        {
            Debug.LogWarning("No players found!");
        }
    }

    void Update()
    {
        if (m_Agent == null || m_Animator == null || m_AttackCollider == null) return;

        if (m_TargetObjectList.Count <= 0) return;

        // 最も近いターゲットを追尾
        GameObject closestTarget = GetClosestTarget();
        if (closestTarget != null)
        {
            m_Agent.destination = closestTarget.transform.position;
        }
        if (m_Agent.remainingDistance >= m_Agent.stoppingDistance)
        {
            m_Animator.SetBool("IsRun", true);
        }
        // 攻撃ロジック
        else if (m_Agent.remainingDistance <= m_Agent.stoppingDistance && Time.time >= m_LastAttackTime + m_AttackCoolDown)
        {
            m_Animator.SetBool("IsRun", false);
            Attack();
        }
    }

    private GameObject GetClosestTarget()
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in m_TargetObjectList)
        {
            if (target == null) continue;

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }
        return closestTarget;
    }


    private void EnableAttack()
    {
       m_AttackCollider.enabled = true;
    }
    private void DisableAttack()
    {
        m_AttackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("HitPlayer");
        }
    }
    private void Attack()
    {
        if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            m_Animator.SetTrigger("Attack");
            m_LastAttackTime = Time.time;
        }
    }
}
