using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerBehaviour : MonoBehaviour
{
    private Transform _playerTransform;
    private Rigidbody2D _playerRigidbody;
    private GameObject _currentCheckPoint;
    
    // Debug
    private float timeDiff = 0f;


    [System.Serializable]
    public struct InputVars
    {
        public bool jump;
    }

    [System.Serializable]
    public struct MovementSettings
    {
        [Tooltip("Modifier for horizontal Movement")]
        public float speed;

        public float groundedDistance;
        public float gravityAccel;
        public float jumpSpeed;
        public float downAccel;
    }

    [System.Serializable]
    public struct Movement
    {
        public float movementSpeed;
        public bool grounded;
        public bool jumping;
        public bool falling;
        public float jumpTimer;
        public float jumpPositionHeight;
    }

    [SerializeField] private InputVars inputVars;
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = transform;
        _playerRigidbody = gameObject.GetComponent<Rigidbody2D>();

        _playerRigidbody.isKinematic = true;
        _playerRigidbody.freezeRotation = true;

        _currentCheckPoint = GameObject.Find("Start");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        tempBackToStart();
        /*GetInput();
        Grounded();
        Move();
        Jump();*/
    }

    private void FixedUpdate()
    {
        //GetInput();
        Grounded();
        Ceilinged();
        Move();
        Jump();
    }

    private void GetInput()
    {
        inputVars.jump = Input.GetKey(KeyCode.Space);
    }

    private void Move()
    {
        _playerTransform.Translate(_playerTransform.right * (movementSettings.speed * Time.fixedDeltaTime));
    }

    private void Jump()
    {
        
        // JumpCheck
        if (inputVars.jump && movement is { grounded: true, jumping: false })
        {
            movement.jumpPositionHeight = _playerTransform.transform.position.y;
            movement.jumpTimer = 0f;
            movement.jumping = true;
            //Debug.Log($"Start: {transform.position.ToString()}");
            timeDiff = Time.fixedTime;
        }

        // Handle Jump
        if (movement.jumping)
        {
            if (movement is { grounded: true, jumpTimer: > 0.1f })
            {
                movement.jumping = false;
                //Debug.Log($"End: {transform.position.ToString()}");
                timeDiff = Time.fixedTime - timeDiff;
                //Debug.Log($"TimeDiff: {timeDiff}");
                return;
            }

            float newHeight = movement.jumpPositionHeight + movementSettings.jumpSpeed * movement.jumpTimer -
                              (movementSettings.gravityAccel / 2.0f * Mathf.Pow(movement.jumpTimer, 2.0f));
            _playerTransform.transform.position =
                new Vector3(transform.position.x, newHeight, transform.position.z);
            movement.jumpTimer += Time.fixedDeltaTime;
        }

        if (movement is { grounded: false, jumping: false, falling: false })
        {
            movement.jumpPositionHeight = _playerTransform.transform.position.y;
            movement.jumpTimer = 0f;
            movement.falling = true;
        }

        // Handle Falling
        if (movement.falling)
        {
            if (movement is { grounded: true })
            {
                movement.falling = false;
                return;
            }

            float newHeight = movement.jumpPositionHeight -
                              (movementSettings.gravityAccel / 2.0f * Mathf.Pow(movement.jumpTimer, 2.0f));
            _playerTransform.transform.position =
                new Vector3(transform.position.x, newHeight, transform.position.z);
            movement.jumpTimer += Time.fixedDeltaTime;
        }
    }

    private void Grounded()
    {
        var hit = Physics2D.Raycast(_playerTransform.position, _playerTransform.up * (-1),
            movementSettings.groundedDistance);
        if (hit.collider != null)
        {
            _playerTransform.position = new Vector3(_playerTransform.position.x,
                hit.point.y + _playerTransform.localScale.y,
                _playerTransform.position.z);
            movement.grounded = true;
            return;
        }

        movement.grounded = false;
    }

    private void Ceilinged()
    {
        if (Physics2D.Raycast(_playerTransform.position, _playerTransform.up,
                movementSettings.groundedDistance))
        {
            movement.jumping = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Hurtbox"))
        {
            transform.position = _currentCheckPoint.transform.position - new Vector3 (0,-3,0);
            return;
        }
        if (col.tag.Equals("Checkpoint"))
        {
            _currentCheckPoint = col.gameObject;
        }

    }

    private void tempBackToStart()
    {
        if (Input.GetKey(KeyCode.B))
        {
            transform.position =  _currentCheckPoint.transform.position;
        }
    }

}