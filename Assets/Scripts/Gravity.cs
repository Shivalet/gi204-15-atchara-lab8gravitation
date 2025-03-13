using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;

    private const float G = 0.00667f;
    public static List<Gravity> gravityObjectlist;
    
    //otbit
    [SerializeField] private bool plants = false;
    [SerializeField] private int orbitSpeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityObjectlist == null)
        {
            gravityObjectlist = new List<Gravity>();
        }

        gravityObjectlist.Add(this);
        
        //orbiting
        if (!plants)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectlist)
        {
            //call Attract
            if (obj != this)
                Attract(obj);
        }
    }
    void Attract(Gravity other)
    {
        Rigidbody otherRb = other .rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * otherRb.mass/Mathf.Pow(distance,2));
        Vector3 gavityForce = forceMagnitude * direction.normalized;
        
        otherRb.AddForce(gavityForce);

    }
}
