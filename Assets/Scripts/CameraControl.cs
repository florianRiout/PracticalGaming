using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float rotationSpeed = 100f;
    private float distance = 5.0f;
    private float minDistance = 1.0f;
    private float maxDistance = 20.0f;
    public float zoomStep = 10000f;
    public float xCursor = 0f;
    public float yCursor = 2f;
    private Vector3 distanceVector;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        distanceVector = new Vector3(xCursor, yCursor, -distance);
        Vector2 angles = this.transform.localEulerAngles;
        xCursor = angles.x;
        xCursor = angles.y;

        Rotate(xCursor, yCursor);
    }

    // Update is called once per frame
    void Update()
    {
        //left clic
        if (Input.GetMouseButton(0))
        {
            xCursor += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            yCursor -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            Rotate(xCursor, yCursor);
        }
        //right clic
        if (Input.GetMouseButton(1))
        {
            xCursor += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            yCursor -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            Rotate(xCursor, yCursor);

            player.transform.rotation = Quaternion.Euler(0.0f, xCursor, 0.0f);
        }
        //mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            ZoomOut(Input.GetAxis("Mouse ScrollWheel"));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            ZoomIn(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    /**
     * Transform the cursor mouvement in rotation and in a new position
     * for the camera.
     */
    void Rotate(float x, float y)
    {
        //Transform angle in degree in quaternion form used by Unity for rotation.
        Quaternion rotation = Quaternion.Euler(y, x, 0.0f);

        //The new position is the target position + the distance vector of the camera
        //rotated at the specified angle.
        Vector3 position = rotation * distanceVector + player.transform.position;

        //Update the rotation and position of the camera.
        transform.rotation = rotation;
        transform.position = position;
    }

    /**
     * Reduce the distance from the camera to the target and
     * update the position of the camera (with the Rotate function).
     */
    void ZoomIn(float zoomValue)
    {
        if(distance - zoomStep * zoomValue > minDistance)
        {
            distance -= zoomStep * zoomValue;
            distanceVector = new Vector3(0f, 2f, -distance);
            Rotate(xCursor, yCursor);
        }
    }

    /**
     * Increase the distance from the camera to the target and
     * update the position of the camera (with the Rotate function).
     */
    void ZoomOut(float zoomValue)
    {
        if(distance + zoomStep * -zoomValue < maxDistance)
        {
            distance += zoomStep * -zoomValue;
            distanceVector = new Vector3(0f, 2f, -distance);
            Rotate(xCursor, yCursor);
        }

    }

    /**
     * Assigns the camera to the charcater moving it as the
     * character moves
     */
    internal void Follow(Player player)
    {
        transform.parent = player.transform;
        this.player = player;
    }
}