using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public Color[] GearColors;
    public UIGearMouse UIGearMouse;
    public LayerMask WorldGearLayer;
    public Transform UISlotHolder;
    public Text CharText;

    private bool rotateWorldGears;

    private List<ISlot> slots = new List<ISlot>();

    private DragState state = DragState.WaitingInput;

    private ISlot draggedWorldSlot;

    private ISlot targetSlot;
    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        CacheSlots(transform);
        CacheSlots(UISlotHolder);

        void CacheSlots(Transform parent)
        {
            var newSlots = parent.GetComponentsInChildren<ISlot>();

            for (int i = 0; i < newSlots.Length; i++)
            {
                var slot = newSlots[i];
                slot.Setup();
                slots.Add(slot);
            }
        }

        ResetGame();
    }

    public void ResetGame()
    {
        int colorIndex = 0;

        rotateWorldGears = false;

        CharText.text = "Encaixe as engrenagens em qualquer ordem";

        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];

            if (slot.GetGearType() == GearType.World)
                slot.Toogle(false);
            else
            {
                slot.InsertGear(GearColors[colorIndex]);
                colorIndex++;
            }
        }
    }


    public bool TryInsertUIGearInWorldSlot(Color color)
    {
        var slot = GetSlotInMousePosition();

        if (slot == null || slot.IsSlotFull())
            return false;

        slot.InsertGear(color);

        return true;
    }

    public ISlot GetSlotInMousePosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, WorldGearLayer);

        if (hit.collider != null)
        {
            return hit.collider.gameObject.GetComponent<ISlot>();
        }
        else
            return null;
    }

    private void Update()
    {
        switch (state)
        {
            case DragState.WaitingInput:
                {
                    if (rotateWorldGears)
                        RotateWorldGear();

                    if (Input.GetMouseButtonDown(0))
                    {
                        draggedWorldSlot = GetSlotInMousePosition();

                        if (draggedWorldSlot == null)
                            draggedWorldSlot = UIRaycaster.Instance.GetUISlotInMousePosition();

                        if (draggedWorldSlot != null && draggedWorldSlot.IsSlotFull())
                        {
                            state = DragState.Dragging;
                            UIGearMouse.SetupAndEnable(draggedWorldSlot.GetColor());
                            UIGearMouse.GetTf().position = Input.mousePosition;
                            draggedWorldSlot.Toogle(false);
                        }

                    }
                    break;
                }

            case DragState.Dragging:
                {
                    if (Input.GetMouseButton(0))
                    {
                        UIGearMouse.GetTf().position = Input.mousePosition;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        targetSlot = null;

                        targetSlot = GetSlotInMousePosition();
                        
                        if(targetSlot == null)
                            targetSlot = UIRaycaster.Instance.GetUISlotInMousePosition();
                            
                        if(targetSlot != null && !targetSlot.IsSlotFull())
                        {
                            targetSlot.InsertGear(draggedWorldSlot.GetColor());
                            draggedWorldSlot.Toogle(false);
                            CheckIfAllWorldGearAreFull();
                        }
                        else
                            draggedWorldSlot.Toogle(true);

                        UIGearMouse.Toogle(false);
                        state = DragState.WaitingInput;
                    }
                    break;
                }
        }

    }

    private void RotateWorldGear()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            if (slot.GetGearType() == GearType.World)
            {
                (slot as WorldSlot).RotateGear();
            }
        }
    }
    private void CheckIfAllWorldGearAreFull()
    {
        int insertedWorldGearCount = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            if (slot.GetGearType() == GearType.World && slot.IsSlotFull())
                insertedWorldGearCount++;
        }

        rotateWorldGears = insertedWorldGearCount >= 5;
        
        if(rotateWorldGears)
            CharText.text = "Yay, parabéns. Task concluída!";
        else
            CharText.text = "Encaixe as engrenagens em qualquer ordem";

    }

    public enum DragState
    {
        WaitingInput,
        Dragging,
    }



}

public enum GearType
{
    World,
    UI
}