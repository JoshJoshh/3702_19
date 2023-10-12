using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject Menu, Player, Bullet, Slash;
    public Material Zombie,Goblin,Cow,Golem,Wizard;
    public int Sow,Solem,Sizard,Gun=2;
    private KeyCode[] keyCodes = {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5
    };
    public GameObject[] Weapons = new GameObject[6];
    private float elapsedTime;
    public int[] values = { 
        100, 0, 0, 30}, max;
    private string[] strings = {
        "Health: ",
        "Ammo: ",
        "Score: ",
        "Remaining: "
    };
    public GameObject[] text = new GameObject[4];
    private int[] ammo = {
        0, 0, 8, 24, 5, 2
    }, current;
    private float[] reload = {
        0, .15f, .5f, .25f, .75f, 1.5f};
    void Start()
    {
        max = (int[])values.Clone();
        current = (int[])ammo.Clone();
    }
    void Update()
    {
        for (int i = 4; i-->0;)
        {
            text[i].GetComponent<TextMeshProUGUI>().text = strings[i] + (i != 2 ? values[i] + "/" + max[i] : values[i]);
        }
        if (Input.GetButton("Cancel"))
        {
            Menu.SetActive(true);
            this.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetButton("Fire1"))
        {
            if (elapsedTime <= 0)
            {
                if (Gun > 1)
                {
                    GameObject instance = Instantiate(Bullet,
                        Player.transform.position + Player.transform.forward +
                        Player.transform.right * .4f + Player.transform.up * .5f,
                        Player.transform.rotation) as GameObject;
                    instance.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 4000.0f);
                    current[Gun] -= current[Gun] < 1 ? -max[1] + 1 : 1;
                    elapsedTime += current[Gun] < 1 ? reload[Gun] * 3 : reload[Gun];
                }
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (Gun < 2)
            {
                GameObject instance = Instantiate(Slash,
                    Player.transform.position + Player.transform.forward * 2f,
                    Player.transform.rotation) as GameObject;
                elapsedTime += reload[Gun];
            }
        }
        if (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
        }
        for (int i = 6; i --> 1;)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                Weapons[Gun].SetActive(false);
                Gun = i;
                Weapons[Gun].SetActive(true);
            }
        }
        max[1] = ammo[Gun];
        values[1] = current[Gun];
    }
}
