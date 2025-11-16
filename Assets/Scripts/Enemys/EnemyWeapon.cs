using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject enemyBullet;
    public float fireRate;
    private float time;
    private float fireRotation;

    void Start()
    {
        time = fireRate / 2;
        fireRotation = firePoint.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= fireRate)
        {
            Shoot();
            if (fireRate > 0.5f)
            {
                fireRate -= 0.1f;
            }
            time = 0;
        }
    }

    void Shoot()
    {
        var randomRotate = Random.Range(-15, 15);
        firePoint.transform.Rotate(0, 0, randomRotate, Space.Self);
        Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
        firePoint.transform.Rotate(0, 0, -randomRotate, Space.Self);
    }
}
