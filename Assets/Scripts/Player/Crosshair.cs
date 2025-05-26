using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public LayerMask layerMask;

    public Transform Cube;
    public Camera mainCamera;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
        {
            Cube.position = hitInfo.point;
            Vector3 lookAtPos = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);

            Quaternion lookRotation = Quaternion.LookRotation(lookAtPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1);
        }
    }

}
