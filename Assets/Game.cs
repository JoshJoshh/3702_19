using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Material Zombie,Goblin,Cow,Golem,Wizard;
    public int Sow,Solem,Sizard,Gun=2,score;
    public GameObject Player, Bullet;
    private KeyCode[] keyCodes = {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5
    };
    public GameObject[] Weapons = new GameObject [6];
    public float lifeTime;
    private float elapsedTime;

    void Update()
    {
        if (Gun == 1)
        {
            elapsedTime = .25f;
        }
        if (Input.GetButton("Fire1"))
        {
            if (elapsedTime <= 0) {
                GameObject instance = Instantiate(Bullet,
                    Player.transform.position + Player.transform.forward + Player.transform.right * .5f,
                    Player.transform.rotation) as GameObject;
                instance.GetComponent<Rigidbody>().AddForce(Player.transform.forward * 4000.0f);
                float[] reload = {
                    0, .15f, .5f, .2f, .8f, 1.5f};
                elapsedTime += reload[Gun];
            }
        }
        if (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
        }
        for (int i = 6; i-- > 1;)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                Weapons[Gun].SetActive(false);
                Gun = i;
                Weapons[Gun].SetActive(true);
            }
        }
    }
}
