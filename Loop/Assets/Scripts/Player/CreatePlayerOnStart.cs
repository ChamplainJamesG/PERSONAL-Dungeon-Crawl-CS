using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerGenerator.GenerateInitialCharacter();   
    }
}
