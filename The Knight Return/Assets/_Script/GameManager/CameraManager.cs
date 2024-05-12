using System.Collections;
using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float FollowSpeed = 4f;
    public GameObject player;

    private Transform camTransform;
    private Camera cam;

    public float shakeDuration = 0f;
    public float shakeAmount = 0.1f;
    Vector3 originalPos;

    private float originalSize;
    private Coroutine shrinkCoroutine;

    [Header("Check")]
    public Transform _isCamGround;
    private bool isCamGround;
    public LayerMask Ground;

    private bool bossRoom = false;
    public GameObject camBossRoomPos;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (camTransform == null)
        {
            camTransform = cam.transform;
        }

        player = GameObject.FindGameObjectWithTag("Player");

    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        originalSize = cam.orthographicSize;
    }

    private void Update()
    {
        if (!bossRoom)
        {
            isCamGround = Physics2D.OverlapCircle(_isCamGround.position, 0.2f, Ground);
            Vector3 targetPosition = player.transform.position;
            targetPosition.z = -10;
            Vector3 currentPosition = transform.position;

            Vector3 newPosition;
            if (!isCamGround)
            {
                newPosition = new Vector3(targetPosition.x, targetPosition.y, currentPosition.z);
            }
            else
            {
                newPosition = new Vector3(targetPosition.x, targetPosition.y + 3f, currentPosition.z);
            }

            transform.position = Vector3.Lerp(currentPosition, newPosition, FollowSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = camBossRoomPos.transform.position;
        }

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

    // Thu nho camera
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

    public void CamBossRoom()
    {
        bossRoom = true;
    }

    public void BossDie()
    {
        if (bossRoom)
        {
            bossRoom = false;
        }
    }
}
