using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class music : MonoBehaviour
{

    [SerializeField] private AudioSource music_bg;
    [SerializeField] private Slider sound_slider;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //music_bg = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public void volume_change()
    {
        music_bg.volume = sound_slider.value;
    }
}
