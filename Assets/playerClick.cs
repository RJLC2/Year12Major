using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerClick : MonoBehaviour
{
    public NewBehaviourScript Game;
    public string playerName;

    void Start() // selects the guessed player
    {
        Game = GameObject.Find("InputField").GetComponent < NewBehaviourScript >(); // finding the InputField and Getting the player data from NewBehaviourScript
        playerName = gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text;
    }

    public void buttonClick() // player will be guessed once clicked
    {
        Game.Getplayerdata(playerName);
    }
}
