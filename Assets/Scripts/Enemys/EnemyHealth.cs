using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health;
    public GameObject deathEffect;

    public void Start()
    {
        var mod = Random.Range(0,99);
        if (mod > 50){
            health++;
        }
        if (mod > 90){
            health++;
        }
        if (mod >= 99){
            health++;
        }
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Espinho")
        {
            TakeDamage();
        }

    }
}
