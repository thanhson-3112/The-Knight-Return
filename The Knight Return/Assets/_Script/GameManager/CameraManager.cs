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
    private Vector3 originalPos;

    private float originalSize;
    private Coroutine shrinkCoroutine;

    [Header("Check")]
    public Transform _isCamGround;
    public Transform _isGround;
    private bool isCamGround;
    private bool playerGround;
    public LayerMask Ground;

    private float upArrowHoldTime = 0f;
    private float downArrowHoldTime = 0f;
    private const float holdThreshold = 1f;

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
        playerGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        isCamGround = Physics2D.OverlapCircle(_isCamGround.position, 5f, Ground);

        Vector3 targetPosition = player.transform.position;
        targetPosition.z = -10;
        Vector3 currentPosition = transform.position;

        // rung camera
        Vector3 shakeOffset = Vector3.zero;
        if (shakeDuration > 0)
        {
            shakeOffset = Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime;
        }

        targetPosition += shakeOffset;

        Vector3 newPosition;
        if (!isCamGround && playerGround)
        {
            newPosition = new Vector3(targetPosition.x, targetPosition.y, currentPosition.z);
        }
        else
        {
            newPosition = new Vector3(targetPosition.x, targetPosition.y + 4f, currentPosition.z);
        }

        if (playerGround)
        {
            bool otherKeyPressed = false;
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode) && keyCode != KeyCode.UpArrow && keyCode != KeyCode.DownArrow)
                {
                    otherKeyPressed = true;
                    break;
                }
            }

            if (Input.GetKey(KeyCode.UpArrow) && !otherKeyPressed)
            {
                upArrowHoldTime += Time.deltaTime;
                if (upArrowHoldTime >= holdThreshold)
                {
                    newPosition = new Vector3(targetPosition.x, targetPosition.y + 10f, currentPosition.z);
                }
            }
            else
            {
                upArrowHoldTime = 0f;
            }

            if (Input.GetKey(KeyCode.DownArrow) && !otherKeyPressed)
            {
                downArrowHoldTime += Time.deltaTime;
                if (downArrowHoldTime >= holdThreshold)
                {
                    newPosition = new Vector3(targetPosition.x, targetPosition.y - 7f, currentPosition.z);
                }
            }
            else
            {
                downArrowHoldTime = 0f;
            }
        }

        transform.position = Vector3.Lerp(currentPosition, newPosition, FollowSpeed * Time.deltaTime);
    }

    public void ShakeCamera()
    {
        originalPos = camTransform.localPosition;
        shakeDuration = 0.5f;
    }

    // thu nho camera
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

    public void StopShrinkCamera()
    {
        if (shrinkCoroutine != null)
            StopCoroutine(shrinkCoroutine);

        cam.orthographicSize = originalSize;
    }
}
