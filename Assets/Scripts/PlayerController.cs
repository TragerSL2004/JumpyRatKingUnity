using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isPlayerOne = true;

    [Space]
    [SerializeField, Tooltip("Player max speed")] private float _maxSpeed;
    [SerializeField, Tooltip("Player Acceleration")] private float _acceleration;
    [SerializeField, Tooltip("Player jump height")] private float _jumpHeight;

    [Space]
    [SerializeField] private Vector3 _groundCheck;
    [SerializeField] private float _groundCheckRadius;

    private Rigidbody _rigidBody;
    private Vector3 _moveDirection;
    private bool _jumpInput = false;
    private bool _isGrounded = false;

    private void Awake()
    {
        //Get a reference to this game object's rigidbody.
        _rigidBody = GetComponent<Rigidbody>();

        //Give an error message in the console if the rigidbody is null.
            Debug.Assert(_rigidBody, "Rigidbody is null");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerOne)
        {
            //Get player one movement input
            _moveDirection = new Vector3(Input.GetAxisRaw("Player1Horizontal"), 0, 0);
            _jumpInput = Input.GetAxisRaw("Player1Jump") != 0;
        }
        else
        {
            //Get player two movement input
            _moveDirection = new Vector3(Input.GetAxisRaw("Player2Horizontal"), 0, 0);
            _jumpInput = Input.GetAxisRaw("Player2Jump") != 0;
        }
    }
    private void FixedUpdate()
    {
        //Ground check
        _isGrounded = Physics.OverlapSphere(transform.position + _groundCheck, _groundCheckRadius).Length > 1;

        //Add movement force
        Vector3 force = _moveDirection * _acceleration * Time.deltaTime;
        _rigidBody.AddForce(force, ForceMode.VelocityChange);

        //Clamp velocity to _maxSpeed
        Vector3 velocity = _rigidBody.velocity;
        float newXSpeed = Mathf.Clamp(_rigidBody.velocity.x, -_maxSpeed, _maxSpeed);
        velocity.x = newXSpeed;
        _rigidBody.velocity = velocity;

        //Add jump force
        if(_jumpInput && _isGrounded)
        {
            //Calculate force needed to reach _jumpHeight
            float jumpForce = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //Draw ground check
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _groundCheck, _groundCheckRadius);
    }
#endif
}
