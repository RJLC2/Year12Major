using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerClick : MonoBehaviour
{

    public string playerName;

    void Start() // selects the guessed player
    {
        playerName = gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text;
    }

    public void buttonClick() // player will be guessed once clicked
    {
        print(playerName);
    }
}
