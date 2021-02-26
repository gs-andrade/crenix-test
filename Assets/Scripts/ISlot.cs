using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot 
{
    void Setup();

    void Toogle(bool toogle);

    bool IsSlotFull();

    Color GetColor();

    void InsertGear(Color color);

    GearType GetGearType();
}
