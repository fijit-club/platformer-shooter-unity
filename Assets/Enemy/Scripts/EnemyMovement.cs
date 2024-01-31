using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool move;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform enemyLocation;
    [SerializeField] private Collider2D col;
    [SerializeField] private EnemyGun enemyGun;
    
    private bool _initiatedTimer;
    private Transform _player;
    
    private bool Approximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
    
    private void Update()
    {
        if (!move) return;

        if (!_initiatedTimer)
        {
            StartCoroutine(ShootPlayer());
            _initiatedTimer = true;
        }

        if (Approximation(transform.localPosition.x, enemyLocation.localPosition.x, .1f))
        {
            col.enabled = true;
            return;
        }
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }

    private IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (transform.position.x < 0f)
            enemyGun.Aim(_player, 1);
        else
            enemyGun.Aim(_player, -1);
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
