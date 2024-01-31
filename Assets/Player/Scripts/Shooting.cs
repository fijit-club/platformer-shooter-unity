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

    public GameObject test;
    
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SceneManager.LoadScene(0);
    }
}
