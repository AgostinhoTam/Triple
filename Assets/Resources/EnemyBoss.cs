using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] BoxCollider m_AttackCollider;
    [SerializeField] BoxCollider m_AttackCollider1;
    [SerializeField] float m_AttackCoolDown = 3f;

    DamageSystem m_DamageSystem;

    [Header("References")]
    NavMeshAgent m_Agent;
    Animator m_Animator;
    List<GameObject> m_TargetObjectList;


    float m_LastAttackTime = 0f;
    bool m_IsAttacking = false;
    void Start()
    {
        // 必要なコンポーネントを取得
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_DamageSystem = GetComponent<DamageSystem>();


        if (m_AttackCollider == null)
        {
            Debug.LogError("Attack Collider is not assigned!");
            return;
        }
        if(m_AttackCollider1 == null)
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
        if (m_Agent == null || m_Animator == null || m_AttackCollider == null || m_AttackCollider1 == null) return;
        if (m_TargetObjectList.Count <= 0) return;

        // 最も近いターゲットを追尾
        GameObject closestTarget = GetClosestTarget();
        if (closestTarget != null)
        {
            m_Agent.destination = closestTarget.transform.position;
        }

        if (m_Agent.remainingDistance >= m_Agent.stoppingDistance && m_Agent.velocity.magnitude >= 0.1f)
        {
            m_Animator.SetBool("IsRun", true);
        }
        else
        {
            m_Animator.SetBool("IsRun", false);
        }
        if (m_Agent.remainingDistance <= m_Agent.stoppingDistance+5 && Time.time >= m_LastAttackTime + m_AttackCoolDown)
        {
            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                Debug.Log("Attack");
                Attack();
            }
            else
            {
                Debug.Log("GroundAttack");
                Attack1();
            }
        }

        if (m_DamageSystem.GetHealth() <= 0)
        {
            m_Animator.SetTrigger("Dead");
            enabled = false;
            Destroy(gameObject, 2);
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
    private void EnableAttack1()
    {
        m_AttackCollider1.enabled = true;
    }
    private void DisableAttack1()
    {
        m_AttackCollider1.enabled = false;
    }

    private void Attack()
    {

                if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
                {
                    m_IsAttacking = true;
                    m_Animator.SetTrigger("Attack1");
                    m_LastAttackTime = Time.time;
                }
    }
    private void Attack1()
    {
                if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack02"))
                {
                    m_IsAttacking = true;
                    m_Animator.SetTrigger("Attack2");
                    m_LastAttackTime = Time.time;
               
                }
    }

}
