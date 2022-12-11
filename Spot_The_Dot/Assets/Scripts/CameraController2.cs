using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    [SerializeField]GameObject player;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 2, transform.position.z);
    }
}
