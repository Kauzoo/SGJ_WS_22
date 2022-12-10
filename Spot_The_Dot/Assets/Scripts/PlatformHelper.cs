using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlatformHelper : MonoBehaviour
{
    [Header("Settings"), SerializeField]
    private float gravityAccel;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float speed;
    [SerializeField] private int iter;
    [SerializeField] private bool toggleAlwaysRay;
    [SerializeField] private bool toggleSelectedRay;

    private Vector3 pos;
    private Vector3 scale;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        scale = transform.localScale;
    }

    private Vector2[] ComputeArc(float x, float y, int iterations)
    {
        var step = Time.fixedDeltaTime;
        var arr = new Vector2[iterations];
        for (var i = 0; i < iterations; i++)
        {
            var currentTime = step * i;
            arr[i].x = x + i * speed * Time.fixedDeltaTime;
            arr[i].y = ((-1.0f) * gravityAccel * Mathf.Pow(currentTime, 2) / 2.0f) + jumpSpeed * currentTime + y;
        }

        return arr;
    }

    private void OnDrawGizmos()
    {
        if(!toggleAlwaysRay) return;
        var topRightCorner = pos + (scale / 2);
        /*var bottomRightCorner = new Vector3(pos.x + scale.x, pos.y - scale.y);
        var bottomLeftCorner = pos - scale;
        var topLeftCorner = new Vector3(pos.x - scale.x, pos.y + scale.y);*/

        Gizmos.color = Color.red;
        var points = ComputeArc(topRightCorner.x, topRightCorner.y, iter);
        for (var i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i+1]);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(!toggleSelectedRay) return;
        var topRightCorner = pos + (scale / 2);
        Gizmos.color = Color.red;
        var points = ComputeArc(topRightCorner.x, topRightCorner.y, iter);
        for (var i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i+1]);
        }
    }
    
}