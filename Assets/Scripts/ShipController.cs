using UnityEngine;
using System;

/// <summary>
/// Implements player control of tanks, as well as collision detection.
/// </summary>
public class ShipController : MonoBehaviour {
    /// <summary>
    /// How fast to drive
    /// </summary>
    public float SpeedIncrease = 1f;
    public float NormalSpeed = 1f;
    private float ForwardForce;
    //public float ForwardSpeed = 1f;
    /// <summary>
    /// How fast to turn
    /// </summary>
    public float TurnTorque = 1f;

    public event Action RaiseSail = delegate {
     };

    /// <summary>
    /// Keyboard controls for the player.
    /// </summary>
    private Rigidbody2D boatRb;
    public KeyCode ForwardKey, LeftKey, RightKey, BackKey, SpeedUpKey;

    /// <summary>
    /// Current rotation of the tank (in degrees).
    /// We need this because Unity's 2D system is built on top of its 3D system and so they don't
    /// give you a method for finding the rotation that doesn't require you to know what a quaternion
    /// is and what Euler angles are.  We haven't talked about those yet.
    /// </summary>

    internal void Start() {
        boatRb = GetComponent<Rigidbody2D>();
        ForwardForce = NormalSpeed;
        RaiseSail += SpeedUp;
    }

    public void SpeedUp() {
        ForwardForce = NormalSpeed + SpeedIncrease;
    }

    internal void FixedUpdate() {
        if (Input.GetKeyDown(SpeedUpKey)) {
            RaiseSail();
        } else if (Input.GetKeyUp(SpeedUpKey)) {
            ForwardForce = NormalSpeed;
            boatRb.AddForce(-transform.up * Vector2.Dot(boatRb.velocity, transform.up) * Time.fixedDeltaTime);
        }
        //Debug.Log(boatRb.velocity.magnitude);
        //transform.position += (transform.up * velocity * Time.deltaTime);
        var s_right = Vector2.Dot(boatRb.velocity, transform.right);
        boatRb.AddForce(-1f * s_right * transform.right);
        if(Input.GetKey(ForwardKey)) {
            boatRb.AddForce(transform.up * ForwardForce * Time.fixedDeltaTime);
        }
        if (Input.GetKey(BackKey)) {
            //boatRb.AddForce(transform.up * -ForwardForce * Time.fixedDeltaTime);
        }
        if (Input.GetKey(LeftKey)) {
            boatRb.AddTorque(TurnTorque*Time.fixedDeltaTime);
        }
        if (Input.GetKey(RightKey)) {
            boatRb.AddTorque(-TurnTorque*Time.fixedDeltaTime);
        }
    }

    internal void OnTriggerEnter2D(Collider2D other) {
        
    }
}
