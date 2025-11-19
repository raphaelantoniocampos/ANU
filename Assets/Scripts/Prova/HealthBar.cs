using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;

    public Prova prova;
    public Image fillBar;
    public float totalHealth;
    public float health;
    private float damage;

    public bool phaseTwo;
    public bool phaseThree;

    private Animator anim;

    void Start()
    {
        instance = this;

        prova = GetComponent<Prova>();
        anim = GetComponent<Animator>();
        damage = 100 / totalHealth;
        health = totalHealth;
    }
    public void LoseHealth()
    {
        prova.createErrorTime+= 0.5f;
        anim.SetTrigger("hit");
        health --;
        fillBar.fillAmount -= damage / 100;

        if(health <= totalHealth * 0.7)
        {
            phaseTwo = true;
        }

        if(health <= totalHealth * 0.4)
        {
            phaseThree = true;
        }

        if(health <= 0)
        {
            phaseTwo = false;
            phaseThree = false;
            ChangeScene.instance.FinalScoreView();
            // Destroy(gameObject);
        }
    }
    
}
