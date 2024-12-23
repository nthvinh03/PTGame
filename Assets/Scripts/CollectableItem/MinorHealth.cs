using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorHealth : MonoBehaviour
{
    public float amountHealth = 20f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Player"))
        {
            collision.GetComponent<EnemyHealth>().Heal(amountHealth);
            Destroy(gameObject);
        }
    }
}
