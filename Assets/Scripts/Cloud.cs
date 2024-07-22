using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    private GameObject cloudSphere;
    [SerializeField]
    private int numOfSpheresMin = 6;
    [SerializeField]
    private int numOfSpheresMax = 10;
    [SerializeField]
    private Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    [SerializeField]
    private Vector2 sphereScaleRangeX = new Vector2(4, 8);
    [SerializeField]
    private Vector2 sphereScaleRangeY = new Vector2(3, 4);
    [SerializeField]
    private Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    [SerializeField]
    private float scaleYMin = 2f;

    private List<GameObject> spheres;

    private void Start()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(numOfSpheresMin, numOfSpheresMax);
        for (int i = 0; i < num; i++)
        {
            //init
            GameObject sp = Instantiate(cloudSphere);
            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(transform);
            //set random pos
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;
            //set random scale
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);

            spTrans.localScale = scale;
        }
    }

    private void Restart()
    {
        foreach (var sp in spheres)
        {
            Destroy(sp);
        }
        Start();
    }
}
