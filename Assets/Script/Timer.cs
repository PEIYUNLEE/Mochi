﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    int count;

    // Use this for initialization
    void Start()
    {
		count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Count()
    {
        count++;
    }
}