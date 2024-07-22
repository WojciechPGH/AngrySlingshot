using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabProjectile;
    [SerializeField]
    private GameObject launchPoint;
    private Vector3 launchPos;
    private GameObject projectile;
    private bool aimMode;
    private float velocity = 8f;
    private Rigidbody projectileRigidbody;
    private Camera mainCamera;
    private static Slingshot instance;

    private void Awake()
    {
        instance = this;
        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;
        mainCamera = Camera.main;
    }

    private void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);

    }

    private void OnMouseDown()
    {
        aimMode = true;
        projectile = Instantiate(prefabProjectile);
        projectile.transform.position = launchPos;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (aimMode)
        {
            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -mainCamera.transform.position.z;
            Vector3 mousePos3D = mainCamera.ScreenToWorldPoint(mousePos2D);
            Vector3 mouseDelta = mousePos3D - launchPos;
            float maxMagnitude = GetComponent<SphereCollider>().radius;
            if (mouseDelta.magnitude > maxMagnitude)
            {
                mouseDelta = mouseDelta.normalized * maxMagnitude;
            }
            Vector3 projectilePos = launchPos + mouseDelta;
            projectile.transform.position = projectilePos;

            if (Input.GetMouseButtonUp(0))
            {
                aimMode = false;
                projectileRigidbody.isKinematic = false;
                projectileRigidbody.velocity = -mouseDelta * velocity;
                FollowCam.POI = projectile;
                ProjectileLine.Instance.Poi = projectile;
                projectile = null;
                GameManager.Instance.ShotFired();
            }
        }
    }


    public static Vector3 LAUNCH_POS
    {
        get
        {
            if (instance == null) return Vector3.zero;
            return instance.launchPos;
        }
    }
}
