using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.AI;

[System.Serializable]
public class Save
{
    [System.Serializable]
    public class Player
    {
        public int currentHealth;
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

        static public EnemyController.Enemy TypeFromTypeName(string type)
        {
            switch (type)
            {
                case "Static":
                    return EnemyController.Enemy.Static;
                case "Dynamic":
                    return EnemyController.Enemy.Dynamic;
                case "Suicide":
                    return EnemyController.Enemy.Suicide;
                default:
                    return EnemyController.Enemy.Static;
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

    public static Save FromSaved ()
    {
        string path = Application.persistentDataPath + "/save.json";
        string json = File.ReadAllText(path);
        Debug.Log(json);
        return JsonUtility.FromJson<Save>(json);
    }

    public void Persist()
    {
        string json = JsonUtility.ToJson(this, true);
        string path = Application.persistentDataPath + "/save.json";
        File.WriteAllText(path, json);
        Debug.Log("Saving to "+path);
    }

    public void Load(PlayerBehaviour playerBehaviour, List<EnemyController> enemyControllers, GunsController gunController)
    {
        playerBehaviour.currentHealth = this.player.currentHealth;
        playerBehaviour.flightFuel = this.player.fuelLevel;
        playerBehaviour.gameObject.GetComponent<CharacterController>().enabled = false;
        playerBehaviour.gameObject.transform.position = this.player.position;
        playerBehaviour.gameObject.GetComponent<CharacterController>().enabled = true;

        for (int i = 0; i < this.enemies.Count; i++)
        {
            EnemyController enemyController = enemyControllers[i];
            if (enemyController != null)
            {
                enemyController.enemyType = Enemy.TypeFromTypeName(this.enemies[i].typeName);
                enemyController.health = this.enemies[i].health;
                enemyController.gameObject.transform.position = this.enemies[i].position;

            }
        }

        foreach (Gun gun in this.guns)
        {
            if (gun.name == "M249")
            {
                gunController.m249CurrentClipLeft = gun.leftClips;
                gunController.m249CurrentClipRight = gun.rightClips;
                gunController.m249Ammo = gun.ammo;
            }
            else if (gun.name == "Bennelli M4")
            {
                gunController.bennelliM4CurrentClipLeft = gun.leftClips;
                gunController.bennelliM4CurrentClipRight = gun.rightClips;
                gunController.bennelliM4Ammo = gun.ammo;
            }
            else if (gun.name == "RPG7")
            {
                gunController.rPG7CurrentClipLeft = gun.leftClips;
                gunController.rPG7CurrentClipRight = gun.rightClips;
                gunController.rPG7Ammo = gun.ammo;
            }
        }
    }
}
