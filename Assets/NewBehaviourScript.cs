using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;
using SimpleJSON;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    public List<string> AllStars;
    public InputField playerInput;
    public GameObject playerButton;
    public GameObject autoComplete;
    public GameObject clickedPlayer;
    public GameObject Rows;

    public string AllStarTeam;
    public string AllStarConf;
    public string AllStarDiv;
    public string AllStarPos;
    public int AllStarHtF;
    public int AllStarHtI;
    public int AllStarWt;

    public string url = "https://www.balldontlie.io/api/v1/players"; // API url

    public JSONNode jsonResult; // resulting JSON from an API request

    void Start() // list of players that can be randomized
    {
        AllStars.Add("Stephen Curry");
        AllStars.Add("Luka Doncic");
        AllStars.Add("Devon Booker");
        AllStars.Add("LeBron James");
        AllStars.Add("Nikola Jokic");
        AllStars.Add("Jimmy Butler");
        AllStars.Add("Jayson Tatum");
        AllStars.Add("Kevin Durant");
        AllStars.Add("Giannis Antetokoumpo");
        AllStars.Add("Joel Embiid");
        AllStars.Add("Chris Paul");

        int Randomplayer = UnityEngine.Random.Range(0,AllStars.Count); // randomized player from above list
        AllStar(AllStars[Randomplayer]);
    }

    // the IEnumerator Getdata below is acsessing all the API's data for NBA player's first names and last names while loading in all their information atached to them using SimpleJSON code

    IEnumerator GetData(string player) // sends an API request - returns a JSON file
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

        foreach(JSONNode playerObject in data[0]) // the ability to search the NBA player by first or last name
        {
            if (count < 5) // the dropdown will only have the top 5 names
            {
                GameObject newPlayer = Instantiate(playerButton);
                newPlayer.transform.SetParent(autoComplete.transform);
                newPlayer.GetComponent<Button>().GetComponentInChildren<Text>().text = playerObject["first_name"] + " " + playerObject["last_name"];
                print(count + " - " + playerObject["first_name"] + " " + playerObject["last_name"]);
            }
            count++;
        }
       
    }

    // searchPlayer is the ability to search for a player that is located in the API we are using above if the player does not show up eaither the name is spelt wrong or they have retired
    // only the top 5 names will show up in the text box so if the player you want does not show up keep tryping there name out
    // players will begin to show up after 3 or more letters have been typed if a player with only 2 letters in there name user should try there other name (first name or last name)

    public void SearchPlayer() // searching for NBA player
    {

        foreach (Transform child in autoComplete.transform) // loop though the players on dropdown destoy them
        {
            Destroy(child.gameObject);
        }

        if (playerInput.text.Length > 1) // players name will pop up after 2 + characters have been entered
        {
            StartCoroutine("GetData", playerInput.text);
        }

    }

    // the Getplayerdata below is acsessing the NBA player's data that has been selected
    // e.g. First Name, Last Name, Team, Confrence, Divion, Position, Height, Weight, Age & Jersey Number 

    public void Getplayerdata(string player) // once you have selected a player the row with all their information will show up
    {
        GameObject newPlayer = Instantiate(clickedPlayer);
        newPlayer.transform.SetParent(Rows.transform);
        newPlayer.GetComponent<fillStates>().fill(player);
    }

    public void AllStar(string playername)
    {
        print(playername);
        StartCoroutine(GetAllStar(playername));
    }

    IEnumerator GetAllStar(string player) // sends an API request - returns a JSON file
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
                JSONNode team = playerObject["team"]; // all information under team
                AllStarTeam = team["full_name"]; // team name
                AllStarConf = team["conference"]; // conference
                AllStarDiv = team["division"]; // teams divions
                AllStarPos = playerObject["position"]; // players position
                AllStarHtF = int.Parse(playerObject["height_feet"]); // players height feet
                AllStarHtI = int.Parse(playerObject["height_inches"]); // players height inches
                //AllStarWt = playerObject["weight_pounds"] + " lb"; // players weight
            }
            count++;
        }

    }
}