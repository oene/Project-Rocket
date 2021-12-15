using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float mainTrust = 1000f;
    [SerializeField] float rotationTrust = 100f;
    [SerializeField] AudioClip mainBoosterClip;
    [SerializeField] AudioClip trustEngineClip;

    // CACHE
    Rigidbody rocketRigidBody;
    AudioSource audioSource;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftTrustersParticle;
    [SerializeField] ParticleSystem rightTrustersParticle;

    // STATE
    bool isRotation = false;

    // Start / update
    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTrust();
        ProcessRotation();
    }

    // Public methods
    // public int getState() ...

    // methods

    // Handle keyboard input: space
    void ProcessTrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            BoosterPlay();
        }
        else
        {
            BoosterStop();
        }
    }

    void BoosterPlay()
    {
        isRotation = false;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainBoosterClip);
        }
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
        rocketRigidBody.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);
    }
    void BoosterStop()
    {
        if (!isRotation)
        {
            audioSource.Stop();
        }
        mainBoosterParticle.Stop();
    }


    // Handle keyboard input: A and D
    void ProcessRotation()
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

    void RotateLeft()
    {
        isRotation = true;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(trustEngineClip);
        }
        if (!rightTrustersParticle.isPlaying)
        {
            rightTrustersParticle.Play();
        }
        ApplyRotation(rotationTrust);
    }

    void RotateRight()
    {
        isRotation = true;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(trustEngineClip);
        }
        if (!leftTrustersParticle.isPlaying)
        {
            leftTrustersParticle.Play();
        }
        ApplyRotation(-rotationTrust);
    }

    void StopRotation()
    {
        if (isRotation)
        {
            audioSource.Stop();
            isRotation = false;
        }
        leftTrustersParticle.Stop();
        rightTrustersParticle.Stop();
    }

    void ApplyRotation(float trust)
    {
        // Freeze rotation so we can do manually rotation
        // Otherwise the fyscics system can take over
        rocketRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * trust * Time.deltaTime);
        rocketRigidBody.freezeRotation = false;
    }
}
