using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    
    private static float shakeDuration = 0f;
	
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
	
    private Vector3 _originalPos;
    private Transform _camTransform;

    public static void Shake()
    {
        shakeDuration = .1f;
    }
    
    private void Awake()
    {
        if (_camTransform == null)
        {
            if (Camera.main != null) _camTransform = Camera.main.transform;
        }
    }
	
    private void OnEnable()
    {
        _originalPos = _camTransform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            _camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
			
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            _camTransform.localPosition = _originalPos;
        }
    }
}