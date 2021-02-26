using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRaycaster : MonoBehaviour
{

    public static UIRaycaster Instance { private set; get; }

    public GraphicRaycaster GraphicRayCaster;
    public EventSystem EventSystem;

    private PointerEventData pointEventData;


    private void Awake()
    {
        Instance = this;
        pointEventData = new PointerEventData(EventSystem);
    }

    public ISlot GetUISlotInMousePosition()
    {
        pointEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRayCaster.Raycast(pointEventData, results);

        for(int i = 0; i < results.Count; i++)
        {
            var obj = results[i];

            if(obj.gameObject != null)
            {
                var slot = obj.gameObject.GetComponent<ISlot>();

                if (slot != null)
                {
                    return slot;
                }
            }
        }

        return null;
    }
}
