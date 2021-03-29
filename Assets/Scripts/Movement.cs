using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] int thrust = 1000;
    [SerializeField] int rotate = 150;
    [SerializeField] AudioClip engineThrusters;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem mainEngineThrusterParticles;

    Rigidbody rbody;
    AudioSource audSource;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audSource = GetComponent<AudioSource>();

        rbody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                ;
                break;
            case "Finish":
                rbody.constraints = RigidbodyConstraints.None;
                DisableParticles();
                break;
            default:
                rbody.constraints = RigidbodyConstraints.None;
                DisableParticles();
                break;
        }
    }

    void DisableParticles()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
        mainEngineThrusterParticles.Stop();
    }

    // Processes user input
    void ProcessThrust()
    {
        // Thrust forward
        if (Input.GetKey(KeyCode.Space))
        {
            BeginThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void BeginThrusting()
    {
        rbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audSource.isPlaying)
        {
            audSource.PlayOneShot(engineThrusters);
        }
        if (!mainEngineThrusterParticles.isPlaying)
        {
            mainEngineThrusterParticles.Play();
        }
    }

    void StopThrusting()
    {
        audSource.Stop();
        mainEngineThrusterParticles.Stop();
    }

    void ProcessRotation()
    {
        // Rotate left
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        // Rotate right
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateLeft()
    {
        RotateObject(rotate);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void RotateRight()
    {
        RotateObject(-1 * rotate);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    void RotateObject(int rotationValue)
    {
        rbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rbody.freezeRotation = false;
    }
}
