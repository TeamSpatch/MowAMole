﻿using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour
{
    public float unit;
    public Material[] materials;

    MeshRenderer rend;

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            if (value < 0) {
                level = 0;
            } else {
                level = value % 5;
            }
            if (rend == null) {
                rend = transform.GetComponent<MeshRenderer>();
            }
            rend.material = materials[level];
            if (level == 0) {
                transform.localScale = new Vector3(transform.localScale.x, unit, transform.localScale.z);
            } else if (level == 1) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 2, transform.localScale.z);
            } else if (level == 2) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 3, transform.localScale.z);
            } else if (level == 3) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 4, transform.localScale.z);
            } else if (level == 4) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 5, transform.localScale.z);
            }
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2f, transform.position.z);
        }
    }

    int level = 0;
}
