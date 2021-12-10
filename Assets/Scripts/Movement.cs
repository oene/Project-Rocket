using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainTrust = 1000f;
    [SerializeField] float rotationTrust = 100f;
    Rigidbody rocketRigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        rocketRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTrust();
        ProcessRotation();
    }

    // Handle keyboard input: space
    void ProcessTrust() {
        if(Input.GetKey(KeyCode.Space)) {
            if(!audioSource.isPlaying) {
                audioSource.Play();
            }
            rocketRigidBody.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);
        } else {
            audioSource.Stop();
        }
    }

    // Handle keyboard input: A and D
    void ProcessRotation() {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationTrust);
        }
        else if(Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationTrust);
        }
    }

    private void ApplyRotation(float trust)
    {
        // Freeze rotation so we can do manually rotation
        // Otherwise the fyscics system can take over
        rocketRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * trust * Time.deltaTime);
        rocketRigidBody.freezeRotation = false;
    }
}
