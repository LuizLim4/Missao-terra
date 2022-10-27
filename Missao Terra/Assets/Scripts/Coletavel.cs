using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    public nave player;
    public GameObject points;
    public GameObject pointParticle;
    
    public float lifetime = 2.0f;
    public float _lifetime;
    private bool destroy = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("nave").GetComponent<nave>();
        _lifetime = lifetime;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(destroy == true)
        {
            _lifetime -= Time.deltaTime;
        }
        if(_lifetime <= 0)
        {
                
                Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && destroy == false)
        {
            player.score += 1;
            player.movespeed += player.gainSpeed;
            Destroy(points);
            pointParticle.SetActive(true);
            destroy = true;        
            
            
            
        }
    }
}
