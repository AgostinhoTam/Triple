using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int m_SpawnNumber;
    [SerializeField] GameObject m_SpawnObject;
    [SerializeField] float m_SpawnRadius;
    [SerializeField] Transform m_SpawnPoint;

    bool m_HasSpawned = false;


   private Vector3 GetRandomPosition()
    {
        float angle = Random.Range(0, 360);
        float radius = Random.Range(0,m_SpawnRadius);

        Vector3 offset = new Vector3(Mathf.Cos(angle*Mathf.Deg2Rad)*radius,0f,Mathf.Sin(angle*Mathf.Deg2Rad)*radius);
        return m_SpawnPoint.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (!m_HasSpawned && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_HasSpawned =true;
            SpawnEnemies();
        }

    }

    private void SpawnEnemies()
    {
        if (m_SpawnObject == null) return;
        for (int i = 0; i < m_SpawnNumber; ++i)
        {
            Vector3 spawnPosition = GetRandomPosition();
            Instantiate(m_SpawnObject, spawnPosition, Quaternion.identity);
        }
    }
}
