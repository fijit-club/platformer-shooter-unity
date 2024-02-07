using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform spawnedObjHolder;
    public GameObject blood;
    public bool first;
    public Transform enemyLocation;
    public Transform enemyHeadLocation;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private Collider2D col;
    [SerializeField] private EnemyGun enemyGun;
    [SerializeField] private float shootDelay;
    [SerializeField] private float time;
    
    private Transform _player;
    private Vector3 _initPosition;
    private Vector3 _initGunPosition;
    private Quaternion _initGunRotation;
    private bool _shotBullet;
    private bool _move;

    private void Start()
    {
        _initPosition = transform.position;
        _initGunPosition = enemyGun.transform.position;
        _initGunRotation = enemyGun.transform.rotation;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        if (first) Move();
    }

    public void ResetEnemy()
    {
        //ResetTween();
        transform.position = _initPosition;
        enemyGun.transform.position = _initGunPosition;
        enemyGun.transform.rotation = _initGunRotation;
        _shotBullet = false;
        _move = false;
    }

    [ContextMenu("Move")]
    public void Move()
    {
        if (transform != null)
        {
            _move = true;
        }
        col.enabled = true;
    }

    private void Update()
    {
        if (_move)
        {
            var pos = new Vector3(enemyLocation.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, pos, movementSpeed * Time.deltaTime);

            if (transform.position.x == pos.x && !first)
            {
                print("MOVED");
                _player.GetComponent<ClimbUp>().ReachedTop();
                _move = false;
            }
        }
    }

    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(shootDelay);
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
