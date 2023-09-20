using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public enum GameState
    {
        Explore,
        Combat,
    }
    private GameState gameState;
    private void Awake()
    {
        Instance = this;
    }
    public void ChangeGameState(GameState gameState)
    {
        if (this.gameState == gameState) return;
        this.gameState = gameState;
    }
    public GameState GetGameState()
    {
        return gameState;
    }
}
