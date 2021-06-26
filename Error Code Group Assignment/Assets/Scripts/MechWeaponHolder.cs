using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechWeaponHolder : MonoBehaviour
{
    
    private WeaponSlot leftHolder;
    private WeaponSlot rightHolder;

    [Header("Left Gun")]
    public GameObject leftM107;
    public GameObject leftM107Spawn;
    public GameObject leftM4;
    public GameObject leftM4Spawn;
    public GameObject leftM249;
    public GameObject leftM249Spawn;
    public GameObject leftRPG7;
    public GameObject leftRPG7Spawn;

    [Header("Right Gun")]
    public GameObject rightM107;
    public GameObject rightM107Spawn;
    public GameObject rightM4;
    public GameObject rightM4Spawn;
    public GameObject rightM249;
    public GameObject rightM249Spawn;
    public GameObject rightRPG7;
    public GameObject rightRPG7Spawn;


    private void Awake()
    {
        leftHolder = transform.Find("LeftHolder").GetComponent<WeaponSlot>();
        rightHolder = transform.Find("RightHolder").GetComponent<WeaponSlot>();

        leftHolder.OnItemDropped += leftHolder_OnItemDropped;
        rightHolder.OnItemDropped += rightHolder_OnItemDropped;

    }

    private void rightHolder_OnItemDropped(object sender, WeaponSlot.OnItemDroppedEventArgs e)
    {
        Debug.Log("Right weapon: " + e.item);

        if (e.item.name == "M107")
        {
            rightM107.SetActive(true);
            rightM107Spawn.SetActive(true);
            rightM4.SetActive(false);
            rightM4Spawn.SetActive(false);
            rightM249.SetActive(false);
            rightM249Spawn.SetActive(false);
            rightRPG7.SetActive(false);
            rightRPG7Spawn.SetActive(false);
        }
        else if (e.item.name == "M249")
        {
            rightM107.SetActive(false);
            rightM107Spawn.SetActive(false);
            rightM4.SetActive(false);
            rightM4Spawn.SetActive(false);
            rightM249.SetActive(true);
            rightM249Spawn.SetActive(true);
            rightRPG7.SetActive(false);
            rightRPG7Spawn.SetActive(false);
        }
        else if (e.item.name == "M4")
        {
            rightM107.SetActive(false);
            rightM107Spawn.SetActive(false);
            rightM4.SetActive(true);
            rightM4Spawn.SetActive(true);
            rightM249.SetActive(false);
            rightM249Spawn.SetActive(false);
            rightRPG7.SetActive(false);
            rightRPG7Spawn.SetActive(false);
        }
        else if (e.item.name == "RPG7")
        {
            rightM107.SetActive(false);
            rightM107Spawn.SetActive(false);
            rightM4.SetActive(false);
            rightM4Spawn.SetActive(false);
            rightM249.SetActive(false);
            rightM249Spawn.SetActive(false);
            rightRPG7.SetActive(true);
            rightRPG7Spawn.SetActive(true);
        }
        else if (e.item == null)
        {
            rightM107.SetActive(false);
            rightM107Spawn.SetActive(false);
            rightM4.SetActive(false);
            rightM4Spawn.SetActive(false);
            rightM249.SetActive(false);
            rightM249Spawn.SetActive(false);
            rightRPG7.SetActive(false);
            rightRPG7Spawn.SetActive(false);
        }
    }

    private void leftHolder_OnItemDropped(object sender, WeaponSlot.OnItemDroppedEventArgs e)
    {
        Debug.Log("Left weapon: " + e.item);

        if (e.item.name == "M107")
        {
            leftM107.SetActive(true);
            leftM107Spawn.SetActive(true);
            leftM4.SetActive(false);
            leftM4Spawn.SetActive(false);
            leftM249.SetActive(false);
            leftM249Spawn.SetActive(false);
            leftRPG7.SetActive(false);
            leftRPG7Spawn.SetActive(false);
        }
        else if (e.item.name == "M249")
        {
            leftM107.SetActive(false);
            leftM107Spawn.SetActive(false);
            leftM4.SetActive(false);
            leftM4Spawn.SetActive(false);
            leftM249.SetActive(true);
            leftM249Spawn.SetActive(true);
            leftRPG7.SetActive(false);
            leftRPG7Spawn.SetActive(false);
        }
        else if (e.item.name == "M4")
        {
            leftM107.SetActive(false);
            leftM107Spawn.SetActive(false);
            leftM4.SetActive(true);
            leftM4Spawn.SetActive(true);
            leftM249.SetActive(false);
            leftM249Spawn.SetActive(false);
            leftRPG7.SetActive(false);
            leftRPG7Spawn.SetActive(false);
        }
        else if (e.item.name == "RPG7")
        {
            leftM107.SetActive(false);
            leftM107Spawn.SetActive(false);
            leftM4.SetActive(false);
            leftM4Spawn.SetActive(false);
            leftM249.SetActive(false);
            leftM249Spawn.SetActive(false);
            leftRPG7.SetActive(true);
            leftRPG7Spawn.SetActive(true);
        }
        else if (e.item == null)
        {
            leftM107.SetActive(false);
            leftM107Spawn.SetActive(false);
            leftM4.SetActive(false);
            leftM4Spawn.SetActive(false);
            leftM249.SetActive(false);
            leftM249Spawn.SetActive(false);
            leftRPG7.SetActive(false);
            leftRPG7Spawn.SetActive(false);
        }
    }


}
