using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed;
    Vector3 target, start;

    // Start is called before the first frame update
    void Start()
    {
        start = new Vector3(0.6f, -1, 0);
        target = new Vector3(81, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        if(transform.position == target)
        {
            transform.position = start;
        }
    }
}
