using UnityEngine;

public class CameraWithinBackground : MonoBehaviour
{
    public Transform background; // ��� ������Ʈ�� Transform ������Ʈ

    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();

        if (background != null)
        {
            // ����� ��� ���
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
            // ī�޶� ��ġ�� ��� ������ ����
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

            transform.position = clampedPosition;
        }
    }
}

