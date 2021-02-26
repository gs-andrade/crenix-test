using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldSlot : MonoBehaviour, ISlot
{
    public Vector3 RotationSpeed;
    private SpriteRenderer gearRenderer;
    private Transform gearTf;
    public void Setup()
    {
        if (gearRenderer == null)
        {
            gearTf = transform.GetChild(0);
            gearRenderer = gearTf.GetComponent<SpriteRenderer>();
        }
    }

    public void InsertGear(Color color)
    {
        gearRenderer.color = color;
        Toogle(true);
    }

    public bool IsSlotFull()
    {
        return gearRenderer.enabled;
    }

    public void Toogle(bool toogle)
    {
        gearRenderer.enabled = toogle;
    }

    public Color GetColor()
    {
        return gearRenderer.color;
    }

    public GearType GetGearType()
    {
        return GearType.World;
    }

    public void RotateGear()
    {
        if (gearTf != null)
            gearTf.Rotate(RotationSpeed);
    }
}
