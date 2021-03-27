using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rbody;
    AudioSource audSource;
    [SerializeField] int thrust = 1000;
    [SerializeField] int rotate = 150;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // Processes user input
    void ProcessThrust()
    {
        // Thrust forward
        if(Input.GetKey(KeyCode.Space))
        {
            rbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if(!audSource.isPlaying)
            {
                audSource.Play();
            }
        }
        else
        {
            audSource.Stop();
        }
    }

    void ProcessRotation()
    {
        // Rotate left
        if(Input.GetKey(KeyCode.A))
        {
            RotateObject(rotate);   
        }
        // Rotate right
        else if(Input.GetKey(KeyCode.D))
        {
            RotateObject(-1 * rotate);
        }
    }

    void RotateObject(int rotationValue)
    {
        rbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rbody.freezeRotation = false;
    }
}
