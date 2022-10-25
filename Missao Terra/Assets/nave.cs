using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nave : MonoBehaviour
{

    Rigidbody2D rb;
    public float movespeed;
    public float rotateAmount;
    float rotate;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            print(mousePos.x);
            if (mousePos.x < 0.5)
            {
                rotate = rotateAmount;
            }
            if (mousePos.x > 0.5)
            {
                rotate = -rotateAmount;
            }
            transform.Rotate(0, 0, rotate);
        }
        rb.velocity = transform.up * movespeed;
    }

    private void Update()
    {
       
    }
}
