using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIState : BaseUIState
{
    // Start is called before the first frame update
    protected override void Start()
    {
        gameObject.SetActive(true);
    }

    // Just start the game 5Head
    public void StartGame()
    {
        SceneManagement.EnterScene("GameScene");
    }

    // Use this when we add more to the main scene
    public void NextState()
    {
        
    }
}
