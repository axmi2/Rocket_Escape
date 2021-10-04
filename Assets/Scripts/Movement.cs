using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    [SerializeField] float thrust = 1; //Reasonable default? (Change after test)
    [SerializeField] float rotationSpeed = 1; //Reasonable default? (Change after test)
    [SerializeField] AudioClip EngineSound;

    [SerializeField] ParticleSystem Jet1;
    [SerializeField] ParticleSystem Jet2;
    [SerializeField] ParticleSystem Jet3;
    [SerializeField] ParticleSystem Jet4;

    Rigidbody MyRigidbody;
    AudioSource MyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        MyAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
       
    }

    void ProcessRotation()
    {
        //Freeze rotation because we will directly rotate the rocket, not use force
        MyRigidbody.freezeRotation = true;


        //First rotate Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("User turn Right");

            MyRigidbody.transform.Rotate(0,0, -rotationSpeed * Time.deltaTime);
        }
        // Then rotate Left
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MyRigidbody.transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
            Debug.Log("User turn Left");
        }

        //Unfreeze Rotation
        MyRigidbody.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            // Play engine sound
            if (!MyAudioSource.isPlaying)
            {
                MyAudioSource.PlayOneShot(EngineSound);
            }

            //Play engine particle system
            if (!Jet1.isEmitting)
            {
                Jet1.Play();
                Jet2.Play();
                Jet3.Play();
                Jet4.Play();
            }
            

            // Move Rocket
            MyRigidbody.AddRelativeForce(Vector3.up*thrust* Time.deltaTime);
            
            Debug.Log("User pressed Space");
        }
        else { 
            
            //Stop all effects
            MyAudioSource.Stop();
            Jet1.Stop();
            Jet2.Stop();
            Jet3.Stop();
            Jet4.Stop();

        }
        
    }

    
}
