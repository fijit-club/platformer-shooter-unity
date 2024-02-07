using System.Collections;
using SpaceEscape;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool enemy;
    
    private Shooting _shooting;
    
    private void Start()
    {
        _shooting = FindObjectOfType<Shooting>();
    }

    private void Update()
    {
        transform.Translate(transform.InverseTransformDirection(transform.right) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player") && enemy)
        {
            col.transform.gameObject.SetActive(false);
            Bridge.GetInstance().VibrateBridge(true);
            GameStateManager.ChangeState(FindObjectOfType<GameOverState>());
            CameraShake.Shake();
            Destroy(gameObject);
        }

        if (enemy) return;
        if (col.transform.CompareTag("Enemy"))
        {
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
            
            GetComponent<Collider2D>().enabled = false;
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

        Destroy(gameObject);
    }

    private void CheckForHeadshot()
    {
        
    }
}
