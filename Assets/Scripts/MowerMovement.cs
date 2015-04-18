﻿using UnityEngine;
using System.Collections;

public class MowerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float speedLevelMod;
    public float turningRate;

    Vector3 destination;
    Vector3 direction;
    float speed;
    Gardener gardener;

    void Start()
    {
        gardener = GameObject.Find("Garden").GetComponent<Gardener>();
        destination = transform.position;
        destination.y = 0;
    }

    void FixedUpdate()
    {
        if (destination.x == transform.position.x && destination.z == transform.position.z) {
            float x = transform.position.x + gardener.tileSize * gardener.voxelSize * Input.GetAxisRaw("Horizontal");
            float z = transform.position.z + gardener.tileSize * gardener.voxelSize * Input.GetAxisRaw("Vertical");
            if (Input.GetAxisRaw("Horizontal") != 0 && x >= 0 && x <= gardener.voxelSize * gardener.tileSize * gardener.gardenSize.x) {
                destination.x = x;
                UpdateMovement();
            } else if (Input.GetAxisRaw("Vertical") != 0 && z >= 0 && z <= gardener.voxelSize * gardener.tileSize * gardener.gardenSize.y) {
                destination.z = z;
                UpdateMovement();
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * speed);
        }
        float y = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningRate * Time.fixedDeltaTime);
    }

    void UpdateMovement()
    {
        direction = destination - transform.position;
        speed = moveSpeed;
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Grass");
        foreach (GameObject grass in tiles) {
            if (destination.x >= grass.transform.position.x && destination.x < grass.transform.position.x + gardener.voxelSize) {
                if (destination.z >= grass.transform.position.z && destination.z < grass.transform.position.z + gardener.voxelSize) {
                    speed = speed - speedLevelMod * grass.GetComponent<Grass>().Level;
                }
            }
        }
    }
}
