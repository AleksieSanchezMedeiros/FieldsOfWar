using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraDragSpeed = 50f;

    public float zoomSpeed = 500f;

    public float maxX = 100f;
    public float minX = -100f;
    public float maxY = 100f;
    public float minY = -100f;

    public float maxZ = -10f;
    public float minZ = -100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //dragging on x and y axis
        if (Input.GetMouseButton(0))
        {
            float cameraSpeed = cameraDragSpeed * Time.deltaTime;
            Camera.main.transform.position -= new Vector3(Input.GetAxis("Mouse X") * cameraSpeed, Input.GetAxis("Mouse Y") * cameraSpeed, 0);

            //clamping camera position
            Vector3 clampedPosition = Camera.main.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
            Camera.main.transform.position = clampedPosition;
        }


        //zooming with z axis
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float zoomAmount = scroll * zoomSpeed * Time.deltaTime;
            Camera.main.transform.position += new Vector3(0, 0, zoomAmount);

            Vector3 clampedPosition = Camera.main.transform.position;
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
            Camera.main.transform.position = clampedPosition;
        }
    }
    
    void OnDrawGizmos()
    {
        // Draw the camera bounds in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0));
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0));
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(minX, maxY, 0));
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(minX, minY, 0));
    }
}
