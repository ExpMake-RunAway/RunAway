using UnityEngine;

public class CameraWithinBackground : MonoBehaviour
{
    public Transform background; // 배경 오브젝트의 Transform 컴포넌트

    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();

        if (background != null)
        {
            // 배경의 경계 계산
            float vertExtent = mainCamera.orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;

            minX = background.position.x - horzExtent + mainCamera.orthographicSize * mainCamera.aspect;
            maxX = background.position.x + horzExtent - mainCamera.orthographicSize * mainCamera.aspect;
            minY = background.position.y - vertExtent + mainCamera.orthographicSize;
            maxY = background.position.y + vertExtent - mainCamera.orthographicSize;
        }
    }

    private void LateUpdate()
    {
        if (background != null)
        {
            // 카메라 위치를 배경 내에서 제한
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

            transform.position = clampedPosition;
        }
    }
}

