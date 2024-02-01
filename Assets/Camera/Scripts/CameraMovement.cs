using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _cameraY;
    private Vector3 _initPos;

    private void Start()
    {
        _initPos = transform.position;
    }

    public void UpdateCamera(float increment)
    {
        _cameraY = increment;
    }

    public void ResetCameraPosition()
    {
        _cameraY = 0f;
        transform.position = _initPos;
    }
    
    private void Update()
    {
        var pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, _cameraY, 10f * Time.deltaTime);
        transform.position = pos;
    }
}
