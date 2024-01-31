using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool disableShooting;
    
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] private ClimbUp climbUp;
    [SerializeField] private GameObject ray;
    [SerializeField] private LayerMask enemyLayer;
    
    public void Shoot()
    {
        if (disableShooting) return;
        var raycastHit2D = Physics2D.Raycast(gunMuzzle.position, gunMuzzle.right, Mathf.Infinity, enemyLayer);
        if (!raycastHit2D) return;
        print(raycastHit2D.transform.name);
        if (raycastHit2D.transform.CompareTag("Enemy"))
        {
            Destroy(raycastHit2D.transform.gameObject);
            ray.SetActive(false);
            climbUp.movable = true;
            disableShooting = true;
        }
    }
}
