using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem thrustParticlesCenter;
    [SerializeField] ParticleSystem thrustParticlesRight;
    [SerializeField] ParticleSystem thrustParticlesLeft;

    Rigidbody rb;
    AudioSource audioSource;
    PlayerInput playerInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Thrust();
        Rotation();
    }

    private void Rotation()
    {
        var rleft = playerInput.actions["RLeft"];
        var rright = playerInput.actions["RRight"];
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        
        if (rleft.IsPressed() || move.x < 0)
        {
            RotateLeft();
        }
        else if (rright.IsPressed() || move.x > 0)
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
        /*
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
        */
    }

    private void RotateLeft()
    {
        if (!thrustParticlesRight.isPlaying)
        {
            thrustParticlesRight.Play();
        }

        ApplyRotation(rotationSpeed);
    }

    private void RotateRight()
    {
        if (!thrustParticlesLeft.isPlaying)
        {
            thrustParticlesLeft.Play();
        }

        ApplyRotation(-rotationSpeed);
    }

    private void StopRotation()
    {
        thrustParticlesRight.Stop();
        thrustParticlesLeft.Stop();
    }

    private void ApplyRotation(float speed)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(speed * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

    private void Thrust()
    {
        var thrust = playerInput.actions["Thrust"];
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

        /*
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
        */
    }
    
    private void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
        }

        if (!thrustParticlesCenter.isPlaying)
        {
            thrustParticlesCenter.Play();
        }

        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
    }

    private void StopThrusting()
    {
        thrustParticlesCenter.Stop();
        audioSource.Stop();
    }
}
