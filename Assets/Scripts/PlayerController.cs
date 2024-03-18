using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 100), Tooltip("Player max speed")] private float _maxSpeed;
    [SerializeField, Range(0,100), Tooltip("Player Acceleration")] private float _acceleration;

    private Rigidbody _rigidBody;
    private Vector3 _moveDirection;

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
        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
    }
    private void FixedUpdate()
    {
        _rigidBody.AddForce(_moveDirection * _acceleration * Time.deltaTime, ForceMode.VelocityChange);

        if(_rigidBody.velocity.magnitude > _maxSpeed)
            _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
    }
}
