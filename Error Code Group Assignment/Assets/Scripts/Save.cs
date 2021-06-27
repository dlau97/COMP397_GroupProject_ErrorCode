using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Text;

[System.Serializable]
public class Save
{
    [System.Serializable]
    public class Player
    {
        public float currentHealth;
        public float fuelLevel;
        public Vector3 position;

        public Player(PlayerBehaviour playerBehaviour)
        {
            this.currentHealth = playerBehaviour.currentHealth;
            this.fuelLevel = playerBehaviour.flightFuel;
            this.position = playerBehaviour.transform.position;
        }
    }

    [System.Serializable]
    public class Enemy
    {
        public string typeName;
        public float health;
        public Vector3 position;

        static string TypeNameFromType(EnemyController.Enemy type)
        {
            switch(type)
            {
                case EnemyController.Enemy.Static:
                    return "Static";
                case EnemyController.Enemy.Dynamic:
                    return "Dynamic";
                case EnemyController.Enemy.Suicide:
                    return "Suicide";
                default:
                    return "Unknownn";
            }
        }

        public Enemy(EnemyController enemyController)
        {
            this.typeName = TypeNameFromType(enemyController.enemyType);
            this.health = enemyController.health;
            this.position = enemyController.transform.position;
        }
    }

    [System.Serializable]
    public class Gun
    {
        public string name;
        public int leftClips;
        public int rightClips;
        public int ammo;

        Gun(string name, int leftClips, int rightClips, int ammo)
        {
            this.name = name;
            this.leftClips = leftClips;
            this.rightClips = rightClips;
            this.ammo = ammo;
        }

        public static List<Gun> ManyFromGunController(GunsController gunController)
        {
            Gun m249 = new Gun(
                "M249",
                gunController.m249CurrentClipLeft,
                gunController.m249CurrentClipRight,
                gunController.m249Ammo
            );
            Gun benM4 = new Gun(
                "Bennelli M4",
                gunController.bennelliM4CurrentClipLeft,
                gunController.bennelliM4CurrentClipRight,
                gunController.bennelliM4Ammo
            );
            Gun rpg7 = new Gun(
                "RPG7",
                gunController.rPG7CurrentClipLeft,
                gunController.rPG7CurrentClipRight,
                gunController.rPG7Ammo
            );

            return new List<Gun> { m249, benM4, rpg7 };
        }
    }
    public Player player;
    public List<Enemy> enemies;
    public List<Gun> guns;

    Save(Player player, List<Enemy> enemies, List<Gun> guns)
    {
        this.player = player;
        this.enemies = enemies;
        this.guns = guns;
    }

    public static Save Build(PlayerBehaviour playerBehaviour, List<EnemyController> enemyControllers, GunsController gunController)
    {
        Player player = new Player(playerBehaviour);
        List<Enemy> enemies = enemyControllers.Select(ctrl => new Enemy(ctrl)).ToList();
        List<Gun> guns = Gun.ManyFromGunController(gunController);

        return new Save(player, enemies, guns);
    }

    public void Persist()
    {
        string json = JsonUtility.ToJson(this, true);
        string path = Application.persistentDataPath + "/save.json";
        File.WriteAllText(path, json);
        Debug.Log("Saving to "+path);
    }
}
