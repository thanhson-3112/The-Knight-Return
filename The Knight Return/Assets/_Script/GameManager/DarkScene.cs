using System.Collections;
using UnityEngine;

public class DarkScene : MonoBehaviour
{
    public IEnumerator ActivateDarkScene()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(true);
    }

    public IEnumerator DeactivateDarkScene()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
