using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject bladeTrailPrefab;
    public float minCuttingVelocity = 0.001f;

    Vector2 previousPosition;

    bool isCutting;
    Rigidbody2D rb;
    Camera cam;

    CircleCollider2D circleCollider;

    GameObject currentBladetrail;
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
    }

    private void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;

        if(velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }
        else
        {
            circleCollider.enabled = false;
        }
        previousPosition = newPosition;
    }

    void StartCutting()
    { 
        isCutting = true;
        currentBladetrail = Instantiate(bladeTrailPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = false;
    }
    void StopCutting()
    {
        isCutting = false;
        currentBladetrail.transform.SetParent(null);
        Destroy(currentBladetrail, 2.0f);
        circleCollider.enabled = false;
    }
}
