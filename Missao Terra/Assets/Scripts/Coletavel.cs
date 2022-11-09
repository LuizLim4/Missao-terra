using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coletavel : MonoBehaviour
{
    public GameObject points;
    public GameObject pointParticle;
    public nave nave;
    private ParticleSystem particula;
    
    
    
    public float lifetime = 2.0f;
    public float _lifetime;
    
    // Start is called before the first frame update
    void Awake()
    {
        _lifetime = lifetime;
        nave = FindObjectOfType<nave>();
        particula = pointParticle.GetComponent<ParticleSystem>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(points);
            
            Vector2 particulaPosition = gameObject.transform.position;
            Instantiate(particula, particulaPosition, Quaternion.identity);
            Destroy(gameObject);
            //pointParticle.SetActive(true);
        }
        if (collision.gameObject.tag == "inimigo")
        {
            Debug.Log("teste");
            nave.spaw_colision();
            Destroy(gameObject);
        }
    }

    private void particulaConf()
    {
        var main = particula.main;
        if (!particula.IsAlive())
        {
            Debug.Log("estou funcionando?");
            //Destroy(pointParticle);
            Destroy(this.gameObject);
        }
    }
}
