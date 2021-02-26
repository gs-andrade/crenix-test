using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour, ISlot
{
    private Image gearImage;

    private Color currentColor;
    private Color alphaColor = new Color(0, 0, 0, 0);

    public Color GetColor()
    {
        return currentColor;
    }

    public void InsertGear(Color color)
    {
        currentColor = color;
        Toogle(true);
    }

    public bool IsSlotFull()
    {
        return gearImage.color != alphaColor;
    }

    public void Setup()
    {
        if (gearImage == null)
        {
            gearImage = GetComponent<Image>();
        }
    }

    public void Toogle(bool toogle)
    {
        if (!toogle)
            gearImage.color = alphaColor;
        else
            gearImage.color = currentColor;
    }

    public GearType GetGearType()
    {
        return GearType.UI;
    }

   

}
