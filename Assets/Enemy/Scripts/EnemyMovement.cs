using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool move;
    public Transform spawnedObjHolder;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform enemyLocation;
    [SerializeField] private Collider2D col;
    [SerializeField] private EnemyGun enemyGun;
    [SerializeField] private float shootDelay;
    
    private Transform _player;
    private Vector3 _initPosition;
    private Vector3 _initGunPosition;
    private Quaternion _initGunRotation;
    private bool _shotBullet;

    private void Start()
    {
        _initPosition = transform.position;
        _initGunPosition = enemyGun.transform.position;
        _initGunRotation = enemyGun.transform.rotation;
    }

    public void ResetEnemy()
    {
        transform.position = _initPosition;
        enemyGun.transform.position = _initGunPosition;
        enemyGun.transform.rotation = _initGunRotation;
        _shotBullet = false;
    }

    private bool Approximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
    
    private void Update()
    {
        if (!move) return;

        if (Approximation(transform.localPosition.x, enemyLocation.localPosition.x, .1f))
        {
            col.enabled = true;
            return;
        }
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }

    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(shootDelay);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (transform.position.x < 0f)
            enemyGun.Aim(_player, 1, spawnedObjHolder);
        else
            enemyGun.Aim(_player, -1, spawnedObjHolder);
    }

    public void Shoot()
    {
        if (_shotBullet) return;
        _shotBullet = true;
        StartCoroutine(DelayedShoot());
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
