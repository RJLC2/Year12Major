using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;
using SimpleJSON;
using UnityEngine.UI;

public class fillStates : MonoBehaviour
{
    public NewBehaviourScript Game;
    public string url = "https://www.balldontlie.io/api/v1/players"; // API url
    public JSONNode jsonResult; // resulting JSON from an API request

    public Text NameTextF;
    public Text NameTextS;
    public Text NameTextL;
    public Text TeamText;
    public Text ConfText;
    public Text DivText;
    public Text PosText;
    public Text HtTextFTNUM;
    public Text HtTextFT;
    public Text HtTextIN;
    public Text HtTextQuote;
    public Text WtTextNum;
    public Text WtText;

    void Start() // the ability for this script to talk to NewBehavioyrScript
    {
        Game = GameObject.Find("InputField").GetComponent<NewBehaviourScript>(); // finding the InputField and Getting the player data from NewBehaviourScript
    }

    public void fill(string playername)
    {
        print(playername);
        StartCoroutine(Getplayerdata(playername));
    }

    IEnumerator Getplayerdata(string player) // sends an API request - returns a JSON file
    {
        // create the web request and download handler
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();

        webReq.url = string.Format("{0}?search={1}", url, player); // build the url and query

        yield return webReq.SendWebRequest(); // send the web request and wait for a returning result

        string rawJson = Encoding.Default.GetString(webReq.downloadHandler.data); // convert the byte array and wait for a returning result

        jsonResult = JSON.Parse(rawJson); // parse the raw string into a json result we can easily read

        JSONNode data = (JSONNode)jsonResult; // transfering data to a usable format

        int count = 0;

        foreach (JSONNode playerObject in data[0]) // The ability to click a player you have searched for then all there information will be inserted into the rows
        {
            if (count < 1) // once user has selected a player the rows will show up with the below information
            {
                NameTextF.text = playerObject["first_name"]; // first name
                NameTextS.text = " "; // space between name
                NameTextL.text = playerObject["last_name"]; // last name
                JSONNode team = playerObject["team"]; // all information under team
                TeamText.text = team["full_name"]; // team name
                ConfText.text = team["conference"]; // conference
                DivText.text = team["division"]; // teams divions
                PosText.text = playerObject["position"]; // players position
                HtTextFTNUM.text = playerObject["height_feet"]; // players height in feet
                HtTextFT.text = "ft"; // players word ft
                HtTextIN.text = playerObject["height_inches"]; // players inches
                HtTextQuote.text = "'"; // players Quotes
                WtTextNum.text = playerObject["weight_pounds"]; // player weight
                WtText.text = "lb"; // players word lb               
            }
            count++;
        }
        CheckCorrect();
    }

    public void CheckCorrect()
    {
        if (NameTextF.text == Game.AllStarName)
        {
            NameTextF.gameObject.GetComponentInParent<Image>().color = Color.green;
        }
        if (TeamText.text == Game.AllStarTeam) // team text will go green if correct
        {
            TeamText.gameObject.GetComponentInParent<Image>().color = Color.green;
        }
        if (ConfText.text == Game.AllStarConf) // conference text will go green if correct
        {
            ConfText.gameObject.GetComponentInParent<Image>().color = Color.green;
        }
        if (DivText.text == Game.AllStarDiv) // divion text will go green if correct
        {
            DivText.gameObject.GetComponentInParent<Image>().color = Color.green;
        }
        if (PosText.text == Game.AllStarPos) // position text will go green if correct
        {
            PosText.gameObject.GetComponentInParent<Image>().color = Color.green;            
        }
        else // the position will go yellow if the randoized player plays one of the positions
        {
            foreach (char c1 in PosText.text)
            {
                foreach (char c2 in Game.AllStarPos)
                {
                    if (c1 == c2)
                    {
                        PosText.gameObject.GetComponentInParent<Image>().color = Color.yellow;
                        break;
                    }
                }
            }
        }
        if (int.Parse(HtTextFTNUM.text) == Game.AllStarHtF) // height text will go green if the ft and inches are correct
        {
            if (int.Parse(HtTextIN.text) == Game.AllStarHtI)
            {
                HtTextFTNUM.gameObject.GetComponentInParent<Image>().color = Color.green;
            }
        }
        if (int.Parse(WtTextNum.text) == Game.AllStarWt) // the weight will go green if correct
        {
            WtTextNum.gameObject.GetComponentInParent<Image>().color = Color.green;
        }
    }

}
