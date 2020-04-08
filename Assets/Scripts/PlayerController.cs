using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform levelTransform;
    private Transform maxLeftBankTransform;
    private Transform maxRightBankTransform;
    private Transform maxUpBankTransform;
    private Transform maxDownBankTransform;

    private bool isLevelingHorizontally = false;
    private bool isLevelingVertically = false;
    private float levelingTimer = 0.0f;
    private float verticalLevelingTimer = 0.0f;
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
        maxUpBankTransform = new GameObject().transform;
        maxUpBankTransform.Rotate(45.0f,0.0f,0.0f);
        maxDownBankTransform = new GameObject().transform;
        maxDownBankTransform.Rotate(-45.0f,0.0f,0.0f);
    }


    // Update is called once per frame
    void Update()
    {
        var rotationSpeed = 150*Time.deltaTime;

        // Speed control
        // TODO : make this non-linear
        if (Input.GetKey(KeyCode.A)) {
            currentSpeed += 3.0f*Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Z)) {
            currentSpeed -= 3.0f*Time.deltaTime;
        }
        var minSpeed = 4.0f;
        var maxSpeed = 20.0f;
        currentSpeed = Math.Min(maxSpeed, currentSpeed);
        currentSpeed = Math.Max(minSpeed, currentSpeed);

        // Horizontal banking
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
            if (isLevelingHorizontally == false) {
                isLevelingHorizontally = true;
                levelingTimer = 0.0f;
            }
        }

        // Vertical banking
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isLevelingVertically = false;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, maxUpBankTransform.rotation, rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            isLevelingVertically = false;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, maxDownBankTransform.rotation, rotationSpeed);
        } else {
            if (isLevelingVertically == false) {
                isLevelingVertically = true;
                levelingTimer = 0.0f;
            }
        }


        if (isLevelingHorizontally == true && isLevelingVertically == true) {
            var levelingProgress = Math.Min(1.0f, levelingTimer / levelingDuration);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, levelTransform.rotation, levelingProgress);
            levelingTimer += Time.deltaTime;
        }


        // move the player forward
        var distanceTraveled = currentSpeed*Time.deltaTime;
        var newPosition = transform.position;
        newPosition.z += distanceTraveled;
        transform.position = newPosition;
    }

    void LateUpdate()
    {
        var minX = -4.5f;
        var maxX = 4.5f;
        var minY = 2.0f;
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
