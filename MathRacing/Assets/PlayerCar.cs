using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : ICar
{
    public GameObject Go { get; private set; }
    public float BoostSpeed { get; set; }
    public Action BoostCallback { get; set; }

    public PlayerCar(GameObject go, Action callback)
    {
        Go = go;
        BoostCallback = callback;
    }

    public void Boost(float boostAmount)
    {
        BoostSpeed += boostAmount;
        BoostCallback();
    }
}
