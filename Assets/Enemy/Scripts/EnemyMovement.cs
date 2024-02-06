using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool move;
    public Transform spawnedObjHolder;
    public GameObject blood;
    [SerializeField] private bool first;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform enemyLocation;
    [SerializeField] private Collider2D col;
    [SerializeField] private EnemyGun enemyGun;
    [SerializeField] private float shootDelay;
    [SerializeField] private Collider2D head;

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

    private void OnEnable()
    {
        if (first)
            Move();
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
    
    public void Move()
    { {
            var pos = new Vector3(enemyLocation.position.x, transform.position.y, transform.position.z);
            if (transform != null)
                transform.DOMoveX(enemyLocation.position.x, 50f * Time.deltaTime).SetEase(Ease.Linear);
            col.enabled = true;
            head.enabled = true;
            move = false;
            return;
        }
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
        if (!Application.isPlaying) return;
    }
}
