﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField]float rcsThrust = 100f;
    [SerializeField] float mainThrust = 50f;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    void OnCollisionEnter(Collision collision){
        switch(collision.gameObject.tag){
            case "Finish":
                print("finish");
                SceneManager.LoadScene(1);
                break;
            case "Friendly":
                print("ok");
                break;
            case "Fuel":
                print("fuel");
                break;
            default:
                print("Dead");
                break;
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control 
        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward* rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationSpeed);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
                audioSource.Play();

        }
        else
        {
            audioSource.Stop();
        }
    }
}
