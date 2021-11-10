using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICar
{
    GameObject Go { get; }
    float BoostSpeed { get; }
    string Name { get; set; }


    void Boost(float boostAmount);
}
