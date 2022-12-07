using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nave_tutorial : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed;
    public float rotateAmount;
    public float gainSpeed;
    public float time = 2f;
    public SpriteRenderer rotate_tutorial_D;
    public SpriteRenderer rotate_tutorial_E;
    public screenBounce screenBounce;
    public TrailRenderer trailL;
    public TrailRenderer trailR;
    private bool trail = true;
    private float trailTimeout = 0.2f;
    private float _trailTimeout = 0.2f;

    //pode descomentar o [HideInSpector] depois, por enquanto está no editor para debug. Não colocar como 'private' 
    //[HideInInspector]

    private float rotate;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rotate_tutorial_D = rotate_tutorial_D.GetComponent<SpriteRenderer>();
        rotate_tutorial_E = rotate_tutorial_E.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Color transp_D = rotate_tutorial_D.color;
        Color transp_E = rotate_tutorial_E.color;
        if (Input.GetMouseButton(0))
        {  
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (mousePos.x < 0.5)
            {
                rotate = rotateAmount;
                transp_E.a = 1f;
                rotate_tutorial_E.color = transp_E;
            }
            if (mousePos.x > 0.5)
            {
                rotate = -rotateAmount;
                transp_D.a = 1f;
                rotate_tutorial_D.color = transp_D;
            }
            transform.Rotate(0, 0, rotate);
        }
        else 
        {
            transp_D.a = 0.3f;
            transp_E.a = 0.3f;
            rotate_tutorial_E.color = transp_E;
            rotate_tutorial_D.color = transp_D;
        }
        if (screenBounce.ImOutOfBounds(rb.position))
        {
            //desabilitar o trail aqui


            trailL.emitting = false;
            trailR.emitting = false;
            trail = false;
            _trailTimeout = trailTimeout;

            Vector2 newPosition = screenBounce.calculatePosition(rb.position);
            transform.position = newPosition;



        }
        else
        {
            rb.velocity = transform.up * movespeed;
        }


    }

    private void Update()
    {
      
        if (!trail)
        {
            _trailTimeout -= Time.deltaTime;
            if (_trailTimeout <= 0)
            {
                trailL.emitting = true;
                trailR.emitting = true;
                trail = true;
            }

        }

    }


}
