using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Rigidbody2D body;

    public Camera mainCamera;
    private Vector2 relativePos;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        relativePos = body.transform.position - mainCamera.transform.position;

        if (relativePos.y < -5.0f + 2.0f)
            mainCamera.transform.position += new Vector3(0, -6.5f, 0);
    }
}
