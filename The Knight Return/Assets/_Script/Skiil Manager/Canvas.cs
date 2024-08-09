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

        // Ch? ??n khi DontDestroy và Camera chính ?ã s?n sàng
        while (DontDestroy.instance == null || DontDestroy.instance.mainCamera == null)
        {
            yield return null;
        }

        Camera mainCamera = Resources.Load<Camera>("MainCamera");
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            canvas.worldCamera = mainCamera;
            Debug.Log("Camera ?ã ???c gán cho Canvas: " + mainCamera.name);
        }
        else
        {
            Debug.LogWarning("Canvas không ? ch? ?? Screen Space - Camera.");
        }
    }
}
