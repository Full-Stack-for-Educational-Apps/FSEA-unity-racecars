using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishUI: MonoBehaviour
{
    public GameObject finishLineBackground;
    public GameObject placementUI;
    public GameObject placementUITextbox;

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FillPlacementUI(IEnumerable<ICar> rankings)
    {
        int index = 1;
        foreach(ICar c in rankings)
        {
            var newPlacementText = Instantiate(placementUITextbox, Vector3.zero, Quaternion.identity);
            newPlacementText.transform.parent = placementUI.transform;
            newPlacementText.GetComponent<TextMeshProUGUI>().text = index + ". " + c.Name;
            index++;
        }
    }

    public IEnumerator FinishRace(IEnumerable<ICar> rankings, Action callback)
    {

        yield return new WaitForSeconds(2f);
        finishLineBackground.SetActive(true);
        yield return new WaitForSeconds(2f);
        FillPlacementUI(rankings);

        // Make sure this is last. Finishes up anything the RaceController wants done
        callback();
    }

}
