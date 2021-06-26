using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MechInventory : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;

    void Start()
    {
        HasChanged();
    }

    public void HasChanged()
    {
        //System.Text.StringBuilder builder = new System.Text.StringBuilder();
        //builder.Append(" - ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                Debug.Log(item.name);
                //builder.Append(item.name);
                //builder.Append(" - ");
            }
            
        }
        //inventoryText.text = builder.ToString();
    }



}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged:IEventSystemHandler
    {
        void HasChanged();
    }
}
