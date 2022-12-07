using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class start_screen : MonoBehaviour
{

    public GameObject option_menu;
    public GameObject start_menu;
    public AudioSource music;
    int menu_trigger = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(music);
        music = AudioSource.FindObjectOfType<AudioSource>();
    }

    public void play_game ()
    {
        Debug.Log("teste");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1) ;
    }

  public void quit_game ()
    {
        Application.Quit();
    }

    public void mute()
    {
        music.mute = !music.mute;
    }

    public void show_menu()
    {
        
        Debug.Log(menu_trigger);
        if (menu_trigger == 0)
        {
            option_menu.SetActive(true);
            start_menu.SetActive(false);
            menu_trigger = 1;
        }
        else
        {
            option_menu.SetActive(false);
            start_menu.SetActive(true);
            menu_trigger = 0;
        }
        
    }

    public void show_menu_in_game()
    {

        Debug.Log(menu_trigger);
        if (menu_trigger == 0)
        {
            option_menu.SetActive(true);
            Time.timeScale = 0;
            menu_trigger = 1;
        }
        else
        {
            option_menu.SetActive(false);
            Time.timeScale = 1;
            menu_trigger = 0;
        }

    }
}
