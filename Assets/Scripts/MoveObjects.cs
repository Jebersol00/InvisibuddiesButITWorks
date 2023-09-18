using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    private Vector3 offset;
    private Transform selectedWallTransform;
    public AudioSource audioSource;


    private void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to detect which wall is clicked
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Moveable"))
            {
                // Check if the object clicked has a Transform (assuming your walls have Transform components)
                selectedWallTransform = hit.collider.transform;

                // Calculate the offset between the object's position and the mouse position
                offset = selectedWallTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectedWallTransform.position.z));
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Enemy"))
            {
                //Enemy death script
                hit.collider.gameObject.SetActive(false);
                audioSource.Play();
            }
        }
        else if (Input.GetMouseButton(0) && selectedWallTransform != null)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectedWallTransform.position.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // Update the object's position to follow the mouse
            selectedWallTransform.position = curPosition;
        }
        else if (Input.GetMouseButtonUp(0) && selectedWallTransform != null)
        {
            selectedWallTransform = null; // Deselect the wall when the mouse button is released
            Debug.Log("Ur Safe");
        }
    }
}
