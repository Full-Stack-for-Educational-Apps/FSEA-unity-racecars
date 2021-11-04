using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathGen : MonoBehaviour
{
    RaceController rc;
    public GameObject equationBox;
    public Button enterButton;
    public InputField playerInput;

    int num1;
    int num2;

    void Awake()
    {
        GameObject racer = GameObject.Find("RaceController");
        rc = racer.GetComponent<RaceController>();
    }

    void Start()
    {
        equationBox.GetComponent<Text>().text = "";
        NewEquation();
        enterButton.onClick.AddListener(CheckAnswer);
    }
    public void NewEquation()
    {
        num1 = Random.Range(1, 10);
        num2 = Random.Range(1, 10);

        equationBox.GetComponent<Text>().text = num1 + " + " + num2 + " = ";

        playerInput.text = "";
    }

    public void CheckAnswer()
    {
        int sum = num1 + num2;
        if(sum.ToString() == playerInput.text.ToString())
        {
            NewEquation();
            rc.playerCar.Boost(rc.carBaseBoost);
        }
    }
}
