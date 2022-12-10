using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Disclaimer: Reading this code might permanently damage your eyes
public class CameraController : MonoBehaviour
{
    [SerializeField] private float lerpDuration = 0.1f;
    [SerializeField] private float screenEdgeDistance = 0.3f; //in percent
    GameObject _player;
    private Camera _cam;
    private Vector3 _playerPos;
    private Vector3 _screenWorldPos;
    private bool _coroutineRunning;

    // Start is called before the first frame update
    void Start()
    {
        _cam = gameObject.GetComponent<Camera>();
        _player = GameObject.FindWithTag("Player");
        _playerPos = _player.transform.position;
        
        _screenWorldPos = _cam
            .ScreenToWorldPoint(new Vector3(Screen.width * screenEdgeDistance, Screen.height * screenEdgeDistance, _playerPos.z));
        
        transform.position = new Vector3(_playerPos.x - _screenWorldPos.x,
        _playerPos.y - _screenWorldPos.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = _player.transform.position;
        transform.position = new Vector3(_playerPos.x - _screenWorldPos.x, transform.position.y,transform.position.z);
        float vertViewportPos = _cam.WorldToViewportPoint(_playerPos).y;
        if (!_coroutineRunning)
        {
            if (vertViewportPos >= 1 - screenEdgeDistance)
            {
                StartCoroutine(AdjustCameraHeight(CamMoveDir.Up));
            } else if (vertViewportPos <= 0 + screenEdgeDistance)
            {
                StartCoroutine(AdjustCameraHeight(CamMoveDir.Down));
            }
        }
    }

    private IEnumerator AdjustCameraHeight(CamMoveDir camDir)
    {
        _coroutineRunning = true;
        float startTime = 0;
        float startPos = transform.position.y;
        float vertViewportPos;
        do
        {
            vertViewportPos = _cam.WorldToViewportPoint(_player.transform.position).y;
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(startPos, _player.transform.position.y - _screenWorldPos.y * (float)camDir, (startTime += Time.deltaTime) / lerpDuration), transform.position.z);    
            yield return null;
        } while (vertViewportPos > 1.1 - screenEdgeDistance || vertViewportPos < -0.1 + screenEdgeDistance);

        _coroutineRunning = false;
    }

    enum CamMoveDir : short
    {
        Up = -1,
        Down = 1,
        Left = -1,
        Right = 1
    }
}
