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
            Destroy(gameObject);
        }

        if (enemy) return;
        if (col.transform.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            _shooting.EnemyHit(false);
        }
        else if (col.transform.CompareTag("First Enemy"))
        {
            col.gameObject.SetActive(false);
            _shooting.EnemyHit(false);
        }
        else if (col.transform.CompareTag("Enemy Head"))
        {
            if (!col.transform.parent.CompareTag("First Enemy"))
                Destroy(col.transform.parent.gameObject);
            else
                col.transform.parent.gameObject.SetActive(false);
                
            Bridge.GetInstance().VibrateBridge(false);
            _shooting.EnemyHit(true);
        }
        Destroy(gameObject);
    }
}
