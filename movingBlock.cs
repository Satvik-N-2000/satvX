using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlock : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Vector3 moveVector = new Vector3(10f,10f,10f);
    [Range(0, 1)]
    [SerializeField] float moveFactor;
    [SerializeField] float period = 2f;

    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //moving the moveFactor on its own
        float cycles = Time.time / period;
        float tau = Mathf.PI * 2f;
        float rawSinwave = Mathf.Sin(cycles * tau);
        moveFactor = rawSinwave / 2f + 0.5f;
        Vector3 offset = moveFactor * moveVector;
        transform.position = startingPos + offset;
        
    }
}
