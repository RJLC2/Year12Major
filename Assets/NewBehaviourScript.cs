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

    public InputField playerInput;

    public GameObject playerButton;
    public GameObject autoComplete;

    public string url = "https://www.balldontlie.io/api/v1/players"; // API url

    public JSONNode jsonResult; // resulting JSON from an API request

    IEnumerator GetData(string player) // sends an API request - returns a JSON file
    {
        // create the web request and download handler
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();
        
        webReq.url = string.Format("{0}?search={1}", url, player); // build the url and query

        yield return webReq.SendWebRequest(); // send the web request and wait for a returning result

        string rawJson = Encoding.Default.GetString(webReq.downloadHandler.data); // convert the byte array and wait for a returning result


        jsonResult = JSON.Parse(rawJson); // parse the raw string into a json result we can easily read

        JSONNode data = (JSONNode)jsonResult;

        foreach(JSONNode playerObject in data[0])
        {
            print(playerObject["first_name"] + " " + playerObject["last_name"]);
            GameObject newPlayer = Instantiate(playerButton);
            newPlayer.transform.SetParent(autoComplete.transform);
            newPlayer.GetComponent<Button>().GetComponentInChildren<Text>().text = playerObject["first_name"] + " " + playerObject["last_name"];
        }
       
    }

    public void SearchPlayer()
    {
        if(playerInput.text.Length > 2)
        {
            StartCoroutine("GetData", playerInput.text);
        }
    }

    

}