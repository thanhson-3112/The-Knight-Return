using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    private Transform camTransform;
    private Camera cam;

    public float shakeDuration = 0f;
    public float shakeAmount = 0.1f;
    Vector3 originalPos;

    private float originalSize;
    private Coroutine shrinkCoroutine;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (camTransform == null)
        {
            camTransform = cam.transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        originalSize = cam.orthographicSize;
    }

    private void Update()
    {
        Vector3 targetPosition = Target.position;
        targetPosition.z = -10;
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y + 2f, currentPosition.z);

        transform.position = Vector3.Lerp(currentPosition, newPosition, FollowSpeed * Time.deltaTime);

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime;
        }
    }

    // rung camera
    public void ShakeCamera()
    {
        originalPos = camTransform.localPosition;
        shakeDuration = 0.5f;
    }

    // Thu nh? camera
    public void StartShrinkCamera(float shrinkSize, float duration)
    {
        if (shrinkCoroutine != null)
            StopCoroutine(shrinkCoroutine);

        shrinkCoroutine = StartCoroutine(ShrinkCoroutine(shrinkSize, duration));
    }

    IEnumerator ShrinkCoroutine(float shrinkSize, float duration)
    {
        float elapsedTime = 0f;
        float startSize = cam.orthographicSize;

        while (elapsedTime < duration)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, shrinkSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cam.orthographicSize = shrinkSize;
    }

    // Dung thu nho camera
    public void StopShrinkCamera()
    {
        if (shrinkCoroutine != null)
            StopCoroutine(shrinkCoroutine);

        cam.orthographicSize = originalSize;
    }
}
