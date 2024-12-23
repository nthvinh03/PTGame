using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public event EventHandler OnHealthChanged;
    private float health;

    public GameObject minor;
    public EnemyHealth(float h)
    {
        this.maxHealth = h;
        this.health = maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDame(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        Debug.Log("Enemy health left: " + health);
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        if (health <= 0) Die();
    }
    
    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        Debug.Log("Enemy health left: " + health);
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }

    public float GetCurrHealth()
    {
        return health;
    }
    public void Die()
    {
        Debug.Log("Enemy die");
        gameObject.SetActive(false);
        Instantiate(minor, transform.position - new Vector3(0,1,0), Quaternion.identity);
    }
}
