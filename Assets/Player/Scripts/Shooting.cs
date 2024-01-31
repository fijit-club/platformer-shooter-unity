using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool disableShooting;
    
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] private ClimbUp climbUp;
    [SerializeField] private GameObject ray;
    
    public void Shoot()
    {
        if (disableShooting) return;
        var raycastHit2D = Physics2D.Raycast(gunMuzzle.position, gunMuzzle.right);
        if (!raycastHit2D) return;
        if (raycastHit2D.transform.CompareTag("Enemy"))
        {
            Destroy(raycastHit2D.transform.gameObject);
            ray.SetActive(false);
            climbUp.movable = true;
            disableShooting = true;
        }
    }
}
