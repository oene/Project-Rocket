using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float mainTrust = 1000f;
    [SerializeField] float rotationTrust = 100f;
    [SerializeField] AudioClip mainEngine;

    // CACHE
    Rigidbody rocketRigidBody;
    AudioSource audioSource;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftTrustersParticle;
    [SerializeField] ParticleSystem rightTrustersParticle;

    // STATE
    // bool isAlive;

    // Start / update
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

    // Public methods
    // public int getState() ...

    // methods

    // Handle keyboard input: space
    void ProcessTrust() {
        if(Input.GetKey(KeyCode.Space))
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
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
        rocketRigidBody.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);
    }
    void BoosterStop()
    {
        audioSource.Stop();
        mainBoosterParticle.Stop();
    }

    // Handle keyboard input: A and D
    void ProcessRotation() {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
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
        if (!rightTrustersParticle.isPlaying)
        {
            rightTrustersParticle.Play();
        }
        ApplyRotation(rotationTrust);
    }

    void RotateRight()
    {
        if (!leftTrustersParticle.isPlaying)
        {
            leftTrustersParticle.Play();
        }
        ApplyRotation(-rotationTrust);
    }

    void StopRotation()
    {
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
