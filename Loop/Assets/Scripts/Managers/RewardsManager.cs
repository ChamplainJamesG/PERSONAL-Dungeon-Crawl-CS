using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsManager : MonoBehaviour
{
    public static RewardsManager k_instance;

    protected List<PlayerCharacter> _potentialHires;

    // ****TODO: Items for treasure rooms.
    //protected List<Items> 

    //Treasure per battle? 

    private void Awake()
    {
        if (k_instance == null)
            k_instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
