using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : MonoBehaviour, IDropHandler
{
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped; 
    public class OnItemDroppedEventArgs : EventArgs
    {
        public GameObject item;
    }

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //GameObject item = DragHandler.itemBeingDragged;
        //Debug.Log(item);

        if (!item)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            //ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
            OnItemDropped?.Invoke(OnItemDropped, new OnItemDroppedEventArgs { item = item });
            Debug.Log(item.name);
        }
    }

}
