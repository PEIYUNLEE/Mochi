﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerFloorDetect : MonoBehaviour
{

    public bool isOnFloor { get { return m_isOnFloor; } }

    [SerializeField]
    private bool m_isOnFloor;

    public GameObject parents;
    public bool m_isJump;



    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.tag == "ground" && other.gameObject.tag != parents.tag)
        if (other.gameObject.tag != parents.tag)
        {
            m_isJump = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // if (other.gameObject.tag == "ground" && other.gameObject.tag != parents.tag)
        if (other.gameObject.tag != parents.tag)
        {
            m_isOnFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != parents.tag)
        {
            m_isOnFloor = false;
        }
    }
}
