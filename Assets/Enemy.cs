using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject Game, Player, Renemy;
    private NavMeshAgent nav;
    public int Ai, i=0, health;
    private int[] damage = {
        0, 45, 25, 8, 75, 110};
    private int[][] score = {
        new int[] {5, 10, 35, 20, 72},
        new int[] {5, 10, 35, 20, 72},
        new int[] {4, 12, 25, 15, 50},
        new int[] {6, 13, 40, 25, 48},
        new int[] {10, 20, 50, 37, 80},
        new int[] {15, 25, 0, 65, 120}};

    //Loops over the current enemies and looks for an enemy with a specified Ai
    private Vector3 enemyCheck(int a)
    {
        while (true)
        {
            if (this.gameObject.transform.parent.GetChild(i).gameObject.GetComponent<Enemy>().Ai == a)
            {
                Renemy = this.gameObject.transform.parent.GetChild(i).gameObject; break;
            }
            i += i == 9 ? -9 : 1;
        }
        return (Renemy.transform.position - Player.transform.position).normalized;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.name == "Bullet(Clone)")
        {
            health -= damage[Game.GetComponent<Game>().Gun];
            if (health < 1)
            {
                Game.GetComponent<Game>().score += score[Game.GetComponent<Game>().Gun][Ai];
                Debug.Log(Game.GetComponent<Game>().score);
                float angle = Random.Range(0, Mathf.PI);
                transform.position = new Vector3(
                    Player.transform.position.x + Mathf.Cos(angle) * 20, 47f,
                    Player.transform.position.z + Mathf.Sin(angle) * 20);
                if (Ai == 2)
                {
                    Game.GetComponent<Game>().Sow = 0;
                }
                if (Ai < 3)
                {
                    Ai = Random.Range(0, Game.GetComponent<Game>().Sow == 0 ? 3 : 2);
                }
                switch (Ai)
                {
                    case 0:
                        GetComponent<Renderer>().material = Game.GetComponent<Game>().Zombie;
                        health = 50; break;
                    case 1:
                        GetComponent<Renderer>().material = Game.GetComponent<Game>().Goblin;
                        health = 80; break;
                    case 2:
                        Game.GetComponent<Game>().Sow = 1;
                        GetComponent<Renderer>().material = Game.GetComponent<Game>().Cow;
                        health = 150; break;
                    case 3:
                        health = 200; break;
                    case 4:
                        health = 60; break;
                }
            }
            Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        Vector3 direction = (transform.position - Player.transform.position).normalized;
        switch (Ai)
        {
            case 0:
                nav.SetDestination(Player.transform.position + direction * 2.5f); break;
            case 1:
                nav.SetDestination(Player.transform.position + direction * 2.5f);
                if (Game.GetComponent<Game>().Sow == 1)
                {
                    _ = enemyCheck(2);
                    if (Vector3.Distance(Player.transform.position, Player.transform.position) < 7.5f)
                    {
                        nav.SetDestination(Player.transform.position + direction * 12.5f);
                    }
                } break;
            case 2:
                nav.SetDestination(Player.transform.position + direction * 2.5f); break;
            case 3:
                direction = enemyCheck(4);
                nav.SetDestination(Player.transform.position + direction * 7.5f); break;
            case 4:
                direction = enemyCheck(3);
                nav.SetDestination(Player.transform.position + direction * 12.5f); break;
        }
    }
    private void Start()
    {
        Player = GameObject.Find("FPSController");
        Game = GameObject.Find("Game");
        Ai = Random.Range(0, Game.GetComponent<Game>().Sow == 0 ? 3 : 2);
        if (Game.GetComponent<Game>().Solem == 0)
        {
            Ai = 3;
            Game.GetComponent<Game>().Solem = 1;
        } else
        if (Game.GetComponent<Game>().Sizard == 0)
        {
            Ai = 4;
            Game.GetComponent<Game>().Sizard = 1;
        }
        if (Game.GetComponent<Game>().Sow == 0 &
            Ai == 2)
        {
            Game.GetComponent<Game>().Sow = 1;
        }

        switch (Ai)
        {
            case 0:
                nav = GetComponent<NavMeshAgent>();
                GetComponent<Renderer>().material = Game.GetComponent<Game>().Zombie;
                health = 50; break;
            case 1:
                nav = GetComponent<NavMeshAgent>();
                GetComponent<Renderer>().material = Game.GetComponent<Game>().Goblin;
                health = 80; break;
            case 2:
                nav = GetComponent<NavMeshAgent>();
                GetComponent<Renderer>().material = Game.GetComponent<Game>().Cow;
                health = 120; break;
            case 3:
                transform.localScale += new Vector3(1, 1, 1);
                nav = GetComponent<NavMeshAgent>();
                GetComponent<Renderer>().material = Game.GetComponent<Game>().Golem;
                health = 200; break;
            case 4:
                nav = GetComponent<NavMeshAgent>();
                GetComponent<Renderer>().material = Game.GetComponent<Game>().Wizard;
                health = 60; break;
        }
    }
}