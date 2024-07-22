using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    private static ProjectileLine _instance;
    public static ProjectileLine Instance => _instance;
    [SerializeField]
    private float _minDist = 0.1f;
    private LineRenderer _lineRenderer;
    private GameObject _poi;
    private List<Vector3> _points = new List<Vector3>();

    public GameObject Poi
    {
        get { return _poi; }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                _points.Clear();
                AddPoint();
            }
        }
    }

    public Vector3 LastPoint
    {
        get
        {
            if (_points == null) return Vector3.zero;
            return _points[^1];
        }
    }

    private void Awake()
    {
        _instance = this;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if (Poi != null)
        {
            AddPoint();
            if (FollowCam.POI == null)
            {
                Poi = null;
            }
        }
    }

    public void Clear()
    {
        _poi = null;
        _lineRenderer.enabled = false;
        _points.Clear();
    }

    private void AddPoint()
    {
        Vector3 point = _poi.transform.position;
        if (_points.Count > 0 && (point - LastPoint).magnitude < _minDist)
        {
            return;
        }
        if (_points.Count == 0)
        {
            Vector3 launchPosDiff = point - Slingshot.LAUNCH_POS;
            _points.Add(point + launchPosDiff);
            _points.Add(point);
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _points[0]);
            _lineRenderer.SetPosition(1, _points[1]);
        }
        else
        {
            _points.Add(point);
            _lineRenderer.positionCount = _points.Count;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, LastPoint);
        }
        _lineRenderer.enabled = true;
    }
}
