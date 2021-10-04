using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period;
    float movementFactor;
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon ) { return; }
        float cycles = Time.time / period;
        const float taf = Mathf.PI * 2;

        movementFactor = Mathf.Abs(Mathf.Sin(cycles * taf));

        Vector3 offset = movementVector * movementFactor ;

        transform.position = startingPos + offset;
    }
}
