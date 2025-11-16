using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStone : MonoBehaviour
{
    Collider2D stoneFire;
    public float time = 0;
    public float activateTime;
    bool fire;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        stoneFire = this.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activateTime == 0)
        {
            OnFire();
        }
        else
        {
            time += Time.deltaTime;
            if (time >= activateTime && !fire)
            {
                OnFire();
                time = 0;

            }
            if (time >= activateTime && fire)
            {
                OffFire();
                time = 0;
            }
        }
    }
    public void OnFire()
    {
        //stoneFire.enabled = true;
        fire = true;
        anim.SetBool("Fire", true);
    }
    public void OffFire()
    {
        //stoneFire.enabled = false;
        fire = false;
        anim.SetBool("Fire", false);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && fire)
        {
            GameController.instance.LoseLife();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && fire)
        {
            GameController.instance.LoseLife();

        }

    }
}

