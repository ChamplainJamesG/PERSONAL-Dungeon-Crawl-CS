using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public const int MAX_PARTY_SIZE = 4;
    public List<PlayerCharacter> CurrentParty = new List<PlayerCharacter>();

    private static PlayerMaster km_instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (km_instance == null)
            km_instance = this;

        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static PlayerMaster GetInstance()
    {
        return km_instance;
    }

    public void AddPlayer(PlayerCharacter pc)
    {
        CurrentParty.Add(pc);
    }    
}
