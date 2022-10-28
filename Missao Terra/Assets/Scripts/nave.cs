using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nave : MonoBehaviour
{

    private Rigidbody2D rb;
    public float movespeed;
    public float rotateAmount;
    public float gainSpeed;

    //pode descomentar o [HideInSpector] depois, por enquanto está no editor para debug. Não colocar como 'private' 
    //[HideInInspector]
    public int score = 0;
    
    private float rotate;

    //public GameObject points;
    
    public GameObject enemy;
    


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if(collision.gameObject.tag == "ponto")
        {
            Destroy(collision.gameObject);

            pointParticle.SetActive(true);

            movespeed += gainSpeed;
        }
        */

        if(collision.gameObject.tag == "inimigo")
        {
            movespeed = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
