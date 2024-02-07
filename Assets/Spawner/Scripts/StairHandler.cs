using UnityEngine;

public class StairHandler : MonoBehaviour
{
    public EnemyMovement enemy;
    public float yIncrement;
    
    [SerializeField] private Collider2D[] colliders;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Color initialColor;
    [SerializeField] private Transform spawnedParent;
    [SerializeField] private bool first;
    
    private Color _newColor;

    private void Start()
    {
        if (first)
            _newColor = initialColor;
    }

    public void MoveToSpawned()
    {
        transform.parent = spawnedParent;
    }
    
    public void SetLayerOrder(int index)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = index;
        }
    }
    
    public void TurnOnColliders()
    {
        foreach (var col in colliders)
        {
            col.enabled = true;
        }
    }

    public void SetColor(float factor)
    {
        _newColor = initialColor;
        _newColor *= factor;
        _newColor.a = 1f;
    }

    private void Update()
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, _newColor, 3f * Time.deltaTime);
        }
    }
}
