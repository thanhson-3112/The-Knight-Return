using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class AssignCameraToCanvas : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(AssignCameraWhenAvailable());
    }

    private IEnumerator AssignCameraWhenAvailable()
    {
        Canvas canvas = GetComponent<Canvas>();

        // Ch? ??n khi DontDestroy v� Camera ch�nh ?� s?n s�ng
        while (DontDestroy.instance == null || DontDestroy.instance.mainCamera == null)
        {
            yield return null;
        }

        Camera mainCamera = Resources.Load<Camera>("MainCamera");
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            canvas.worldCamera = mainCamera;
            Debug.Log("Camera ?� ???c g�n cho Canvas: " + mainCamera.name);
        }
        else
        {
            Debug.LogWarning("Canvas kh�ng ? ch? ?? Screen Space - Camera.");
        }
    }
}
