using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController3 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private float lerpDuration = 0.1f;
    [SerializeField] private float screenEdgeDistance = 0.3f;
    private bool _coroutineRunning;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_coroutineRunning)
        {
            StartCoroutine(AdjustCameraHeight());
        }
    }

    private IEnumerator AdjustCameraHeight()
    {
        _coroutineRunning = true;
        float startTime = 0;
        float startPos = transform.position.y;
        do
        {
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(startPos, (player.transform.position.y + 2), (startTime += Time.deltaTime) / lerpDuration), transform.position.z);
            yield return null;
        } while (Mathf.Abs(player.transform.position.y - transform.position.y) < 0.1f);

        _coroutineRunning = false;
    }
}
