using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaceController : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject[] opponentObjects;
    public ParticleSystem particle;
    public GameObject mathUI;
    public FinishUI finishUI;
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
            opponentCars.Add(new NPCCar(npc, .5f, 2f));
        }

        GetRankings();
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
        mathUI.gameObject.SetActive(false);
        StartCoroutine(finishUI.FinishRace(GetRankings(), () => playing = false));
    }

    IEnumerator BoostChance(NPCCar npc)
    {
        while (playing)
        {
            yield return new WaitForSeconds(npc.AnswerDelay);
            npc.Boost(carBaseBoost); 
        }
    }

    private IEnumerable<ICar> GetRankings()
    {
        List<ICar> rankings = new List<ICar>();
        foreach (ICar c in opponentCars)
        {
            rankings.Add(c);
        }
        rankings.Add(playerCar);

        return rankings.OrderByDescending(x => x.Go.transform.position.z);
    }

    void Update() {
		if (playing) {
			// Move the cars towards the finish line if the game has started
        	playerCar.Go.transform.Translate(Vector3.right * Time.deltaTime * (carBaseSpeed + playerCar.BoostSpeed));
            foreach (NPCCar npc in opponentCars)
            {
                npc.Go.transform.Translate(Vector3.right * Time.deltaTime * (carBaseSpeed + npc.BoostSpeed));
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                mathUI.GetComponent<MathGen>().CheckAnswer();
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
