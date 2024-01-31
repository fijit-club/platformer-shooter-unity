using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool disableShooting;
    
    [SerializeField] private ClimbUp climbUp;
    [SerializeField] private GunAim gunAim;
    
    [SerializeField] private GameObject ray;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    
    public void Shoot()
    {
        if (disableShooting) return;
        Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
        ray.SetActive(false);
        gunAim.stopAiming = true;
        disableShooting = true;
    }

    public void EnemyHit()
    {
        climbUp.movable = true;
    }
}
