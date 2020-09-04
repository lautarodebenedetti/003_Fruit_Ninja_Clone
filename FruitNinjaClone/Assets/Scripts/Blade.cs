﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float minVelocity = 0.1f;

    private Rigidbody2D rb;
    private Vector3 lastMousePos;
    private Vector3 mouseVelocity;
    private Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        col.enabled = IsMouseMoving();
        SetBladeToMouse();
    }

    private void SetBladeToMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10; //distance of 10 units from the camera
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
    private bool IsMouseMoving()
    {
        Vector3 curMousePos = transform.position;
        float traveled = (lastMousePos - curMousePos).magnitude;
        lastMousePos = curMousePos;

        if(traveled > minVelocity)
        {
            return true;
        } else
        {
            return false;
        }
    }
}