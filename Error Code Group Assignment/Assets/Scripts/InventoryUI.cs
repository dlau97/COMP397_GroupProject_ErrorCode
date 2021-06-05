using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] KeyCode[] toggleInventoryKeys;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if(Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);

                if (inventoryGameObject.activeSelf)
                {
                    ShowMouseCursor();
                }
                else
                {
                    HideMouseCursor();
                }
            }
        }
    }

    public void CloseInventory()
    {
        inventoryGameObject.SetActive(false);
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
