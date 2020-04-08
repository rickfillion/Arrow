using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform levelTransform;
    private Transform maxLeftBankTransform;
    private Transform maxRightBankTransform;

    private bool isLevelingHorizontally = false;
    private float horizontalLevelingTimer = 0.0f;
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
            isLevelingHorizontally = false;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, maxLeftBankTransform.rotation, rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            isLevelingHorizontally = false;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, maxRightBankTransform.rotation, rotationSpeed);
        } else {
            if (isLevelingHorizontally == true) {
                horizontalLevelingTimer += Time.deltaTime;
            } else {
                isLevelingHorizontally = true;
                horizontalLevelingTimer = 0.0f;
            }

            var levelingProgress = Math.Min(1.0f, horizontalLevelingTimer / levelingDuration);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, levelTransform.rotation, levelingProgress);
        }


        var distanceTraveled = currentSpeed*Time.deltaTime;
        var newPosition = transform.position;
        newPosition.z += distanceTraveled;
        transform.position = newPosition;
    }

    void LateUpdate()
    {
        var minX = -4.5f;
        var maxX = 4.5f;
        var minY = 1.0f;
        var maxY = 6.0f;
        var newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Debug.Log("localRotation = " + transform.rotation.z);

        // if they aren't pretty damn close to level, push them around
        if (Math.Abs(transform.rotation.z) > 0.02f) {
            var xPosition = newPosition.x;
            xPosition += (transform.rotation.z / - 1.5f);
            xPosition = Math.Max(minX, xPosition);
            xPosition = Math.Min(maxX, xPosition);
            newPosition.x = xPosition;
        }

        if (Math.Abs(transform.rotation.x) > 0.02f) {
            var yPosition = newPosition.y;
            yPosition += (transform.rotation.x / - 1.5f);
            yPosition = Math.Max(minY, yPosition);
            yPosition = Math.Min(maxY, yPosition);
            newPosition.y = yPosition;
        }

        transform.position = newPosition;
    }
}
