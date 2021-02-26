using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGearMouse : MonoBehaviour
{
    private Image gearImage;
    private Transform cachedTf;

    public void SetupAndEnable(Color color)
    {
        if (gearImage == null)
            gearImage = GetComponent<Image>();

        if (cachedTf == null)
            cachedTf = transform;

        gearImage.color = color;

        Toogle(true);
    }

    public Transform GetTf()
    {
        return cachedTf;
    }


    public void Toogle(bool toogle)
    {
        gearImage.enabled = toogle;
    }
}
