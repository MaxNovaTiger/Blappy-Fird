using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -30;

    // Update is called once per frame
    void Update()
    {
        // Move pipe left on interval at set moveSpeed
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

        // Destroy pipe upon moving out of set deadZone
        if (transform.position.x < deadZone)
        {
            Debug.Log("Pipe Destroyed");
            Destroy(gameObject);
        }
    }
}
