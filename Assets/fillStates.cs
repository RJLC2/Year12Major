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
    public string url = "https://www.balldontlie.io/api/v1/players"; // API url

    public JSONNode jsonResult; // resulting JSON from an API request

    public void fill(string playername)
    {
        print(playername);
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
            if (count < 1) // only one player can be selected at a time
            {

            }
            count++;
        }

    }

}
