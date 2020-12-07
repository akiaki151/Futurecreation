using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataBank bank = DataBank.Open();
        Debug.Log("DataBank.Open()");
        Debug.Log($"save path of bank is { bank.SavePath }");

        PlayerData playerData = new PlayerData()
        {
            name = "Tokoroten",
            level = 1,
            statusList = new List<int>
        {
        10, 20, 30, 40, 50
        }
        };
        Debug.Log(playerData);

        bank.Store("player", playerData);
        Debug.Log("bank.Store()");

        bank.SaveAll();
        Debug.Log("bank.SaveAll()");

        playerData = new PlayerData();
        Debug.Log(playerData);

        playerData = bank.Get<PlayerData>("player");
        Debug.Log(playerData);

        bank.Clear();
        Debug.Log("bank.Clear()");

        playerData = bank.Get<PlayerData>("player");
        Debug.Log(playerData);

        bank.Load<PlayerData>("player");
        Debug.Log("bank.Load()");

        playerData = bank.Get<PlayerData>("player");
        Debug.Log(playerData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
