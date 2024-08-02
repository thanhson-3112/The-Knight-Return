using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    public float scaleSpeed = 3f;
    public CameraManager cam;

    void Start()
    {
        Destroy(gameObject, 2f);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    void Update()
    {
        transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0) * Time.deltaTime;
        cam.ShakeCamera();
    }
}
