using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    private float m_CurrentHP;
    [SerializeField] float m_MaxHP;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHP = m_MaxHP;
    }

    public void TakeDamage(float damage)
    {
        m_CurrentHP -= damage;
        Debug.Log(m_CurrentHP);
    }

    public float GetHealth()
    {
        return m_CurrentHP;
    }

}
