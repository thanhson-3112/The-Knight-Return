using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMachine : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 10f;

    private Vector2 initialPosition;
    public GameObject explosionPrefab;

    [Header("Sound")]
    public AudioClip coreExplosion;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // vi tri roi xuong
        initialPosition = transform.position;
        StartCoroutine(MoveUp());
        StartCoroutine(MachineCoreExplosion());
    }

    private IEnumerator MoveUp()
    {

        Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;
        while (true)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, initialPosition) >= 4f)
            {
                break; 
            }

            yield return null;
        }
    }

    private IEnumerator MachineCoreExplosion()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        SoundFxManager.instance.PlaySoundFXClip(coreExplosion, transform, 1);
        Destroy(gameObject);
    }


}
