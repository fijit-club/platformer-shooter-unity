using SpaceEscape;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private Shooting _shooting;
    
    
    private void Start()
    {
        _shooting = FindObjectOfType<Shooting>();
        print("another test");
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Enemy"))
        {
            print("test");
            Destroy(col.transform.parent.gameObject);

            var enemyMovement = col.transform.parent.GetComponent<EnemyMovement>();

            if (enemyMovement.enemyHeadLocation.position.y < col.GetContact(0).point.y)
            {
                Bridge.GetInstance().VibrateBridge(false);
                _shooting.EnemyHit(true);
            }
            else
            {
                _shooting.EnemyHit(false);
            }
        }
        else if (col.transform.CompareTag("First Enemy"))
        {
            col.transform.parent.gameObject.SetActive(false);
            
            var enemyMovement = col.transform.parent.GetComponent<EnemyMovement>();

            if (enemyMovement.enemyHeadLocation.position.y < col.GetContact(0).point.y)
            {
                Bridge.GetInstance().VibrateBridge(false);
                _shooting.EnemyHit(true);
            }
            else
            {
                _shooting.EnemyHit(false);
            }
        }
        if (col.transform.CompareTag("Enemy") || col.transform.CompareTag("First Enemy"))
        {
            col.transform.parent.GetComponent<EnemyMovement>().blood.transform.parent =
                col.transform.parent.GetComponent<EnemyMovement>().spawnedObjHolder;
            col.transform.parent.GetComponent<EnemyMovement>().blood.SetActive(true);
        }
    }
}
