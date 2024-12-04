using UnityEngine;
using System;

//游戏状态 暂停/游玩
public enum GameState
{
    Pause, GamePlay
}
//事件枚举
public enum EventType
{
    Test,
    StartGame,
}
//事件触发集合
public class EventHandler
{
    public static event Action<string> ShowDialogueEvent;
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }
    public static event Action<GameState> GameStateChangeEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }
}