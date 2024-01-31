using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _cameraY;

    public void UpdateCamera(float increment)
    {
        _cameraY = increment;
    }
    
    private void Update()
    {
        var pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, _cameraY, 10f * Time.deltaTime);
        transform.position = pos;
    }
}
