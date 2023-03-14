using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    Vector3 startingPosition;
    public float maxDistanceFromStart;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startingPosition + Vector3.up * Mathf.Sin(Time.realtimeSinceStartup) * maxDistanceFromStart;
    }
}
