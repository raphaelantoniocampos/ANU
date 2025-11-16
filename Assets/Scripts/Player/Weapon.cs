using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float fireRate;
    private float time;
    private PauseGame pauseGame;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && time >= fireRate)
        {
            if (!GameObject.FindGameObjectWithTag("PauseGame").GetComponent<PauseGame>().gamePaused)
            {
                Shoot();
                time = 0;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        FindObjectOfType<AudioManager>().Play("fire");
    }
}
