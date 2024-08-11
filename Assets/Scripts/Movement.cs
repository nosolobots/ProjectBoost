using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Thrust();
        Rotation();
    }

    private void Rotation()
    {
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
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
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
