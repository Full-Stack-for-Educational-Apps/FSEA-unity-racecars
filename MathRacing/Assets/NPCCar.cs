using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCar : ICar
{
    public GameObject Go { get; private set; }
    public float BoostSpeed { get; set; }
    public string Name { get; set; }

    public float AnswerChance { get; private set; }
    public float AnswerDelay { get; private set; }

    private float boostAmount;

    public NPCCar(GameObject go, float answerChance, float answerDelay)
    {
        Go = go;
        AnswerChance = answerChance;
        AnswerDelay = answerDelay;
        Name = "Gary";
    }

    public void Boost(float boostAmount)
    {
		var rand = Random.Range(0f, 1f);
		if (rand < AnswerChance)
        {
			BoostSpeed += boostAmount;
        }
    }

    public void Boost()
    {
        Boost(boostAmount);
    }
}
