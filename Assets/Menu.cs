using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Game,Main,Credits,Help;
    public void onPlay()
    {
        Game.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void onHelp()
    {
        Help.SetActive(true);
        Main.SetActive(false);
    }
    public void onCredits()
    {
        Credits.SetActive(true);
        Main.SetActive(false);
    }
    public void onQuit()
    {
        Application.Quit();
    }
    public void onBack()
    {
        Main.SetActive(true);
        Credits.SetActive(false);
        Help.SetActive(false);
    }
    public void Start()
    {
        Game.SetActive(false);
    }
}
