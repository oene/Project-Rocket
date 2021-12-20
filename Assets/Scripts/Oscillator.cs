using UnityEngine;

public class Oscillator : MonoBehaviour
{
    const float TAU = Mathf.PI * 2f;  // Constant pi * 2 ~= 6.283
    float movementFactor;
    Vector3 startingPostition;

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPostition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent NaN exception
        // Mathf.Epsilon is the smallest possible float
        // We should compare period to this
        // Comparing to 0f is not safe!
        if(period <= Mathf.Epsilon) { return; }

        // Deterime speed - time grows over time
        float cycles = Time.time / period;

        // Result is value between -1 and 1
        float rawSinWave = Mathf.Sin(cycles * TAU);

        // Correct factor by adding 1, now range is between 0 and 2
        // And divide it by 2, giving rangen between 0 and 1
        // Multiply with 10 to make a wider range
        // Raw   -1.00  -0.50   0.00   0.50   1.00
        // +  1   0.00   0.50   1.00   1.50   2.00
        // /  2   0.00   0.25   0.50   0.75   1.00
        // * 10   0.00   2.50   5.00   7.50  10.00     
        movementFactor = (rawSinWave + 1f) / 2f;
        movementFactor = movementFactor * 10f;

        Vector3 offset = movementVector * movementFactor;

        // offet.x       transform.position.x
        // -----------  ----------------------
        // 0,046441560  -98,85356        +/+
        // 0,030112270  -98,86989
        // 0,017492770  -98,88251
        // 0,007905960  -98,89209
        // 0,002253056  -98,89775
        // 0,000000000  -98,89999        0/0  
        // 0,001046658  -98,89896
        // 0,006961823  -98,89304
        // 0,015625950  -98,88437
        // 0,026962760  -98,87303
        // 0,040147300  -98,85985        -/-
        //
        transform.position = startingPostition + offset;
    }
}
 