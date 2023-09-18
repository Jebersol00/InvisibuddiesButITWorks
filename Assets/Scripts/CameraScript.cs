using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Camera camera;
    public bool rotate;
    public Plane plane;
    public float decreaseCameraPanSpeed = 10;
    public float cameraUpperHeightBound = 10;
    public float cameraLowerHeightBound = 10;
    private Vector3 cameraStartPosition;
    private Vector3 offset;
    private Transform selectedWallTransform;
    public AudioSource audioSource;

    private void Awake()
    {
        if(camera == null)
        {
            camera = Camera.main;
        }

        cameraStartPosition = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle camera control
        UpdateCameraControl();

        // Handle object selection and movement
        HandleObjectSelectionAndMovement();
    }

    private void UpdateCameraControl()
    {
        //Update Plane
        if (Input.touchCount >= 1)
        {
            plane.SetNormalAndPosition(transform.up, transform.position);
        }

        var Delta1 = (Vector3.zero);
        var Delta2 = (Vector3.zero);

        //Scroll (Pan function)
        if (Input.touchCount >= 1)
        {
            //Get distance camera should travel
            Delta1 = PlanePositionDelta(Input.GetTouch(0)) / decreaseCameraPanSpeed;
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                camera.transform.Translate(Delta1, Space.World);
            }
        }

        if (Input.touchCount >= 2)
        {
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) / Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
            {
                return;
            }

            //move camera amount the mid ray
            Vector3 camPositionBeforeAdjustment = camera.transform.position;
            camera.transform.position = Vector3.LerpUnclamped(pos1, camera.transform.position, 1 / zoom);

            if (camera.transform.position.y > (cameraStartPosition.y + cameraUpperHeightBound))
            {
                camera.transform.position = camPositionBeforeAdjustment;
            }

            if (camera.transform.position.y < (cameraStartPosition.y - cameraLowerHeightBound) || camera.transform.position.y <= 1)
            {
                camera.transform.position = camPositionBeforeAdjustment;
            }

            //rotate
            if (rotate && pos2b != pos2)
            {
                camera.transform.RotateAround(pos1, plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, plane.normal) / decreaseCameraPanSpeed);
            }
        }
    }

    private void HandleObjectSelectionAndMovement()
    {
        // Handle object selection and movement with touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Assuming you want to handle only the first touch

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Raycast to detect which wall is touched
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Moveable"))
                    {
                        // Check if the object touched has a Transform (assuming your walls have Transform components)
                        selectedWallTransform = hit.collider.transform;

                        // Calculate the offset between the object's position and the touch position
                        offset = selectedWallTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, selectedWallTransform.position.z));
                    }
                    else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Enemy"))
                    {
                        // Enemy death script
                        hit.collider.gameObject.SetActive(false);
                        audioSource.Play();
                    }
                    break;

                case TouchPhase.Moved:
                    if (selectedWallTransform != null)
                    {
                        Vector3 curScreenPoint = new Vector3(touch.position.x, touch.position.y, selectedWallTransform.position.z);
                        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

                        // Update the object's position to follow the touch
                        selectedWallTransform.position = curPosition;
                    }
                    break;

                case TouchPhase.Ended:
                    if (selectedWallTransform != null)
                    {
                        selectedWallTransform = null; // Deselect the wall when the touch ends
                    }
                    break;
            }
        }
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if(touch.phase != TouchPhase.Moved)
        {
            return (Vector3.zero);
        }

        //delta
        Ray rayBefore = camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        Ray rayNow = camera.ScreenPointToRay(touch.position);
        if(plane.Raycast(rayBefore, out var enterBefore) && plane.Raycast(rayNow, out var enterNow))
        {
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
        }

        //not on plane
        return (Vector3.zero);
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        Ray rayNow = camera.ScreenPointToRay(screenPos);
        if (plane.Raycast(rayNow, out var enterNow))
        {
            return rayNow.GetPoint(enterNow);
        }
        return (Vector3.zero);
    }
}
