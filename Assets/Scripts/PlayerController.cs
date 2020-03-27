using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform levelTransform;
    private Transform maxLeftBankTransform;
    private Transform maxRightBankTransform;

    private bool isLeveling = false;
    private float levelingTimer = 0.0f;
    private float levelingDuration = 2.0f;

    private float currentSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        levelTransform = new GameObject().transform;
        maxLeftBankTransform = new GameObject().transform;
        maxLeftBankTransform.Rotate(0.0f,0.0f,45.0f);
        maxRightBankTransform = new GameObject().transform;
        maxRightBankTransform.Rotate(0.0f,0.0f,-45.0f);
    }


    // Update is called once per frame
    void Update()
    {
        var rotationSpeed = 150*Time.deltaTime;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isLeveling = false;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, maxLeftBankTransform.rotation, rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            isLeveling = false;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, maxRightBankTransform.rotation, rotationSpeed);
        } else {
            if (isLeveling == true) {
                levelingTimer += Time.deltaTime;
            } else {
                isLeveling = true;
                levelingTimer = 0.0f;
            }

            var levelingProgress = Math.Min(1.0f, levelingTimer / levelingDuration);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, levelTransform.rotation, levelingProgress);
        }


        var distanceTraveled = currentSpeed*Time.deltaTime;
        var newPosition = transform.position;
        newPosition.z += distanceTraveled;
        transform.position = newPosition;
    }

    void LateUpdate()
    {
        var xPosition = transform.localPosition.x;
        var minX = -4.5f;
        var maxX = 4.5f;

        // Debug.Log("localRotation = " + transform.rotation.z);

        // if they aren't pretty damn close to level, push them around
        if (Math.Abs(transform.rotation.z) > 0.02f) {
            xPosition += (transform.rotation.z / - 1.5f);
            xPosition = Math.Max(minX, xPosition);
            xPosition = Math.Min(maxX, xPosition);
            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
        }
    }
}
