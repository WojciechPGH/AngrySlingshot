using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [SerializeField]
    private GameObject cloudPrefab;
    private int numOfClouds = 40;
    private Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    private Vector3 cloudPosMax = new Vector3(150,100,100);
    private float cloudScaleMin = 1;
    private float cloudScaleMax = 3;
    private float cloudSpeed = 0.5f;

    private GameObject[] cloudInstances;

    private void Awake()
    {
        cloudInstances = new GameObject[numOfClouds];
        GameObject cloudParent = GameObject.Find("CloudsParent");
        GameObject cloud;
        for(int i = 0; i < numOfClouds; i++)
        {
            cloud = Instantiate(cloudPrefab);
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU;

            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(cloudParent.transform);
            cloudInstances[i] = cloud;
        }
    }

    private void Update()
    {
        foreach(GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeed;
            if(cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;
        }
    }
}
