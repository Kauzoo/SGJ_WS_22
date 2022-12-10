using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerBehaviour : MonoBehaviour
{
    private Transform _playerTransform;
    private Rigidbody2D _playerRigidbody;


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
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Grounded();
        Move();
        Jump();
    }

    private void GetInput()
    {
        inputVars.jump = Input.GetKeyDown(KeyCode.Space);
    }

    private void Move()
    {
        //_playerRigidbody.MovePosition(_playerTransform.right * movementSettings.speed);
        _playerTransform.Translate(_playerTransform.right * movementSettings.speed);
    }

    private void Jump()
    {
        if (inputVars.jump && movement.grounded)
        {
            movement.jumpPositionHeight = _playerTransform.transform.position.y;
            movement.jumpTimer = 0f;
            movement.jumping = true;
        }

        if (movement.jumping)
        {
            if (movement is { grounded: true, jumpTimer: > 0.1f})
            {
                movement.jumping = false;
            }
            else
            {
                /**
                float upVelocity = movementSettings.jumpSpeed * movement.jumpTimer;
                float downVelocity = movementSettings.gravityAccel / 2 * Mathf.Pow(movement.jumpTimer, 2.0f);
                if (upVelocity < downVelocity)
                {
                    downVelocity = movementSettings.gravityAccel + movementSettings.downAccel / 2 * Mathf.Pow(movement.jumpTimer, 2.0f);
                }
                
                */
                float newHeight = movement.jumpPositionHeight + movementSettings.jumpSpeed * movement.jumpTimer - (movementSettings.gravityAccel / 2 * Mathf.Pow(movement.jumpTimer, 2.0f));
                _playerTransform.transform.position =
                    new Vector3(transform.position.x, newHeight, transform.position.z);
                movement.jumpTimer += Time.deltaTime;
            }
        }
    }

    private void Grounded()
    {
        movement.grounded = Physics2D.Raycast(_playerTransform.position, _playerTransform.up * (-1),
            movementSettings.groundedDistance);
    }

    private void Gravity()
    {
        if (!movement.grounded)
        {
        }
    }
}