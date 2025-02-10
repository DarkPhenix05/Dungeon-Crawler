using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //public Collider swordCollider;
    private int damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.transform.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
        }
    }
}
