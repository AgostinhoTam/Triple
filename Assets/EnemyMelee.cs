using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    Rigidbody RB;
    GameObject TargetObject;
    [SerializeField] float MovementSpeed;
    bool InAttackRange;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();

        TargetObject = GameObject.FindGameObjectWithTag("Player");
        if (TargetObject)
        {
            Debug.Log("Targeted Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!InAttackRange)
        {
            Vector3 direction = TargetObject.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

            transform.position = Vector3.MoveTowards(transform.position, TargetObject.transform.position, MovementSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InAttackRange = true;
            Debug.Log("InAttackRange");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InAttackRange = false;
            Debug.Log("OutAttackRange");
            
        }
    }
}
