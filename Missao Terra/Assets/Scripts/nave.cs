using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nave : MonoBehaviour
{

    private Rigidbody2D rb;
    private GameObject timoteo;
    private Animator an;
    public float movespeed;
    public float rotateAmount;
    public float gainSpeed;
    public float time = 2f;
    public bool lose = false;
    public Text CaixaScore;
    public RuntimeAnimatorController animacao_2;
    public RuntimeAnimatorController animacao_1;
    public Sprite timoteo_dead;
    public Sprite timoteo_feliz;
    public Sprite timoteo_idle;
    public Animator sheep_animator;
    public AudioSource som_pontos;
    public AudioSource som_explosao;
    public screenBounce screenBounce;
    public GameObject jogarNovamente;
    public GameObject sair;
    public GameObject coletavel;
    public TrailRenderer trailL;
    public TrailRenderer trailR;
    public ParticleSystem turbina;
    private bool trail = true;
    private float trailTimeout = 0.2f;
    private float _trailTimeout = 0.2f;
    private float timoteoRostoTimeout = 2f;
    private bool timoteoFeliz;

    //pode descomentar o [HideInSpector] depois, por enquanto está no editor para debug. Não colocar como 'private' 
    //[HideInInspector]
    private int score = 0;
    
    private float rotate;

    public static int nave_score = 0;
    public static int naveStart = 0;

    //public GameObject points;

    public GameObject enemy;
    


    private void Awake()
    {
       
        rb = GetComponent<Rigidbody2D>();
        timoteo = GameObject.FindGameObjectWithTag("timoteo");
        an = this.GetComponent<Animator>();
        change_nave();
        //Planet_spawn();
    }
    // Start is called before the first frame update
    void change_nave()
    {
        if (naveStart == 0)
        {
            an.runtimeAnimatorController = animacao_1;
        }
        if (naveStart == 1)
        {
            an.runtimeAnimatorController = animacao_2;
        }
    }
    void Start()
    {
        
        jogarNovamente.SetActive(false);
        StartCoroutine(spaw());
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
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
        CaixaScore.text = "POINTS: " + score.ToString();

        if(!trail)
        {
            _trailTimeout -= Time.deltaTime; 
            if(_trailTimeout <= 0)
            {
                trailL.emitting = true;
                trailR.emitting = true;
                trail = true;
            }
            
        }
        if(score >= 50)
        {
            nave_score = 1;
        }if(score >= 100)
        {
            nave_score = 2;
        }

        if (timoteoFeliz)
        {
            timoteoRostoTimeout -= Time.deltaTime;
            if (timoteoRostoTimeout <= 0)
            {
                timoteo.GetComponent<SpriteRenderer>().sprite = timoteo_idle;
                timoteoFeliz = false;
                timoteoRostoTimeout = 2f;
            }
        }
        //Debug.Log(trailL.emitting);
    }

    IEnumerator spaw()
    {
        yield return null;
        while (true)
        {
            if (lose == true)
            {
                print("parar pontos");
                yield return null;
                break;
            }
            else
            {
                float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                Vector2 spawnPosition = new Vector2(spawnX, spawnY);
                Instantiate(coletavel, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(time);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "inimigo")
        {
            jogarNovamente.SetActive(true);
            sair.SetActive(true);
            movespeed = 0;
            som_explosao.Play();
            sheep_animator.Play("explosion");
            Destroy(turbina);
            timoteo.GetComponent<SpriteRenderer>().sprite = timoteo_dead;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coletavel")
        {
            movespeed += gainSpeed;
            score += 10;
            CaixaScore.text = "POINTS: " + score.ToString();
            som_pontos.Play();
            timoteo.GetComponent<SpriteRenderer>().sprite = timoteo_feliz;
            timoteoFeliz = true;
        }
    }
    public void tentarNovamente() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Sair()
    {
        Application.Quit();
    }

    public void spaw_colision()
    {
        float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(coletavel, spawnPosition, Quaternion.identity);
    }

    //public void Planet_spawn()
    //{
    //    for (int i = 0; i<3; i++)
    //    {
    //        float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
    //        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
    //        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
    //        float scaleRandom = Random.Range(0.5f, 3);
    //        enemy.transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
    //        Instantiate(enemy, spawnPosition, Quaternion.identity);
    //    }
        

    //}
}
