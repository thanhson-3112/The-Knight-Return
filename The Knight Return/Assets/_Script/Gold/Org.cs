using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Org : MonoBehaviour, IDamageable
{
    [SerializeField] protected float org = 4f;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {

    }

    public virtual void TakePlayerDamage(float _damageDone)
    {
        org -= _damageDone;

        // Trigger shake effect
        StartCoroutine(Shake());

        GetComponent<GoldSpawner>().InstantiateLoot(transform.position);

        if (org <= 0)
        {
            OrgDestroyed();
        }
    }

    public void OrgDestroyed()
    {
        Destroy(gameObject, 0.5f);

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        for (int i = 0; i < 5; i++)
        {
            GetComponent<GoldSpawner>().InstantiateLoot(transform.position);
        }
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 newPos = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.position = newPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}
