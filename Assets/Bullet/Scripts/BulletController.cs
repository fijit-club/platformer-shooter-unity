using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

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
        if (col.transform.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            _shooting.EnemyHit();
            Destroy(gameObject);
        }
    }
}
