using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{

    private NetworkVariable<int> _playersInGame = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get
        {
            return _playersInGame.Value;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                //Logger.Instance.LogInfo($"{id} just connected...");
                _playersInGame.Value++;
            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                _playersInGame.Value--;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
