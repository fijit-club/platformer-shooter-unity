using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool move;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform enemyLocation;
    
    private bool Approximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
    
    private void Update()
    {
        if (!move) return;
        if (Approximation(transform.localPosition.x, enemyLocation.localPosition.x, .1f)) return;
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
}
