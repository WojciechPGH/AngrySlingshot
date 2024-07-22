using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static GameObject POI;
    private float camZ;
    private float easing = 0.05f;
    private Vector2 minXY = Vector2.zero;
    private Camera mainCamera;
    private void Awake()
    {
        camZ = transform.position.z;
        mainCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        Vector3 destination = Vector3.zero;
        if (POI != null)
        {
            destination = POI.transform.position;
            if (POI.CompareTag("Projectile"))
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }


        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        mainCamera.orthographicSize = destination.y + 10;
    }
}
