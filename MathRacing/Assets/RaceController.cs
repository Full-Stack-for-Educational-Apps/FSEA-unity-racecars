using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject[] opponentObjects;
    public ParticleSystem particle;
    public GameObject finishLineUI;
    public PlayerCar playerCar { get; private set; }
    public List<NPCCar> opponentCars { get; private set; }

	public float carBaseSpeed = 8;
	public float carBaseBoost = 9;
    public float boostReduce = 0.01f;


	bool playing = false;

	void Start() {
        playerCar = new PlayerCar(playerObject, () => particle.Play());
        opponentCars = new List<NPCCar>();
        foreach(GameObject npc in opponentObjects)
        {
            opponentCars.Add(new NPCCar(npc, .01f, 2f));
        }

		StartRace();
	}

	// Starts the race
	void StartRace() {
		playing = true;
        foreach(NPCCar npc in opponentCars)
        {
            StartCoroutine(BoostChance(npc));
        }
	}

    public void FinishRace()
    {
        Debug.Log("You win!");
        Camera.main.gameObject.GetComponent<CameraFollow>().enabled = false;
        StartCoroutine(FinishRaceAnimation());
    }

    IEnumerator FinishRaceAnimation()
    {
        yield return new WaitForSeconds(2f);
        finishLineUI.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(2f);
        playing = false;
    }

    IEnumerator BoostChance(NPCCar npc)
    {
        while (playing)
        {
            yield return new WaitForSeconds(npc.AnswerDelay);
            npc.Boost(carBaseBoost); 
        }
    }

    void Update() {
		if (playing) {
			// Move the cars towards the finish line if the game has started
        	playerCar.Go.transform.Translate(Vector3.right * Time.deltaTime * (carBaseSpeed + playerCar.BoostSpeed));
            foreach (NPCCar npc in opponentCars)
            {
                npc.Go.transform.Translate(Vector3.right * Time.deltaTime * (carBaseSpeed + npc.BoostSpeed));
            }
		}

		// Slowly decrease the boost after a player gets an answer right
        playerCar.BoostSpeed = Mathf.Max(playerCar.BoostSpeed - boostReduce, 0);
        foreach (NPCCar npc in opponentCars)
        {
            npc.BoostSpeed = Mathf.Max(npc.BoostSpeed - boostReduce, 0);
        }
    }
}
