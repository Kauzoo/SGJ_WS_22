using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlatformHelper : MonoBehaviour
{
    [Header("Settings"), SerializeField]
    private float maxJumpDistance;

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

    private void OnDrawGizmos()
    {
        var topRightCorner = pos + scale / 2;
        var bottomRightCorner = new Vector3(pos.x + scale.x, pos.y - scale.y);
        var bottomLeftCorner = pos - scale;
        var topLeftCorner = new Vector3(pos.x - scale.x, pos.y + scale.y);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(topRightCorner, Vector3.right * maxJumpDistance);
    }
}
