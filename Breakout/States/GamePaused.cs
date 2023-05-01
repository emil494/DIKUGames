using System;
using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.States;

public class GamePaused : IGameState {
    private static GamePaused instance = null;
    private IGameState backGroundImage;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;

    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.InitializeGameState();
        }
        return GamePaused.instance;
    }

    private void InitializeGameState(){
        menuButtons = new Text[] {
            new Text ("Continue", new Vec2F(0.3f, 0.1f), new Vec2F(0.5f, 0.5f)), 
            new Text ("Main Menu", new Vec2F(0.3f, 0.0f), new Vec2F(0.5f, 0.5f))};
        menuButtons[0].SetColor(System.Drawing.Color.Red);
        menuButtons[1].SetColor(System.Drawing.Color.Coral);
        backGroundImage = GameRunning.GetInstance();
        activeMenuButton = 0;
        maxMenuButtons = 1;
    }

    public void ResetState(){
        InitializeGameState();
    }

    public void UpdateState(){
    }
    
    public void RenderState(){
        backGroundImage.RenderState();
        foreach (Text button in menuButtons){
            button.RenderText();
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
        switch(action){
            case KeyboardAction.KeyPress:
                break;
            case KeyboardAction.KeyRelease:
                switch(key){
                    case KeyboardKey.Up:
                        if (activeMenuButton == 0){
                            activeMenuButton += 1;
                            menuButtons[0].SetColor(System.Drawing.Color.Coral);
                            menuButtons[1].SetColor(System.Drawing.Color.Red);
                        } else {
                            activeMenuButton -= 1;
                            menuButtons[0].SetColor(System.Drawing.Color.Red);
                            menuButtons[1].SetColor(System.Drawing.Color.Coral);
                        }
                        break;
                    case KeyboardKey.Down:
                        if (activeMenuButton == 1){
                            activeMenuButton -= 1;
                            menuButtons[0].SetColor(System.Drawing.Color.Red);
                            menuButtons[1].SetColor(System.Drawing.Color.Coral);
                        } else {
                            activeMenuButton += 1;
                            menuButtons[0].SetColor(System.Drawing.Color.Coral);
                            menuButtons[1].SetColor(System.Drawing.Color.Red);
                        }
                        break;
                    case KeyboardKey.Enter:
                        if (activeMenuButton == maxMenuButtons){
                            EventBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "MAIN_MENU"
                                }
                            );
                        } else {
                            EventBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_RUNNING"
                                }
                            );
                        }
                        break;
                }
            break;
        }
    }
}