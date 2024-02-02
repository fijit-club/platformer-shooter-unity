using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    public bool disableShooting;
    
    public bool DisableShooting
    {
        set => disableShooting = value;
    }

    [SerializeField] private ClimbUp climbUp;
    [SerializeField] private GunAim gunAim;
    [SerializeField] private StairSpawner stairSpawner;
    
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
        
        var currentEnemy = stairSpawner.currentEnemy;
        currentEnemy.Shoot();
    }

    public void EnemyHit(bool headshot)
    {
        climbUp.movable = true;
        climbUp.headshot = headshot;
    }
}
