using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] Transform emptyTransform;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private float shootDelay;
    
    private bool _aim;
    private Transform _player;
    
    public void Aim(Transform player, int direction, Transform spawnedObjHolder)
    {
        transform.parent = spawnedObjHolder;
        
        emptyTransform.parent = spawnedObjHolder;
        emptyTransform.rotation = Quaternion.Euler(0f, 180f, 0f);

        _player = player;
        
        transform.DORotate(new Vector3(0f, 180f,  direction * 30f), 10f * Time.deltaTime);
        
        Shoot();
    }

    private void Shoot()
    {
        StartCoroutine(DelayShot());
    }

    private IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(shootDelay);
        var temp = Instantiate(prefab, muzzlePoint.position, transform.rotation);
        Vector3 difference = _player.position - temp.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        temp.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
    
    private void Update()
    {
        if (_aim)
        {
            var rot = transform.eulerAngles;
            rot.z = Mathf.Lerp(rot.z, emptyTransform.eulerAngles.z, 10f * Time.deltaTime);
            transform.eulerAngles = rot;
        }
    }
}
