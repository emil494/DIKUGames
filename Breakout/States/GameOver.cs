using System;
using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;

namespace Breakout.States;

public class GameOver : IGameState {
    private static GameOver instance = null;
    private Entity backGroundImage;
    private Text text;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;

    /// <summary>
    /// Returns instance of itself and creates itself if null
    /// </summary>
    /// <returns> Itself </returns>
    public static GameOver GetInstance() {
        if (GameOver.instance == null) {
            GameOver.instance = new GameOver();
        }
        GameOver.instance.InitializeGameState();
        return GameOver.instance;
    }

    /// <summary>
    /// Initializes the state
    /// </summary>
    private void InitializeGameState(){
        text = new Text ("Game Over", new Vec2F(0.15f, 0.1f), new Vec2F(0.8f, 0.8f));
        text.SetColor(System.Drawing.Color.Gray);
        menuButtons = new Text[] {
            new Text ("Main Menu", new Vec2F(0.3f, 0.06f), new Vec2F(0.5f, 0.5f)), 
            new Text ("Quit", new Vec2F(0.3f, -0.04f), new Vec2F(0.5f, 0.5f))};
        menuButtons[0].SetColor(System.Drawing.Color.Red);
        menuButtons[1].SetColor(System.Drawing.Color.Coral);
        backGroundImage = new Entity(
            new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f,1.0f)),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        activeMenuButton = 0;
        maxMenuButtons = 1;
    }

    /// <summary>
    /// Resets the state
    /// </summary>
    public void ResetState() {
        InitializeGameState();
    }

    /// <summary>
    /// Updates selected objects in the state
    /// </summary>
    public void UpdateState() {}
    
    /// <summary>
    /// Renders all objects in the state
    /// </summary>
    public void RenderState() {
        backGroundImage.RenderEntity();
        text.RenderText();
        foreach (Text button in menuButtons){
            button.RenderText();
        }
    }

    /// <summary>
    /// Handles key inputs
    /// </summary>
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
                                    EventType = GameEventType.WindowEvent,
                                    Message = "CLOSE_GAME"
                                }
                            );
                        } else {
                            EventBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "MAIN_MENU"
                                }
                            );
                        }
                        break;
                }
            break;
        }
    }

    //For testing purposes
    public int GetActiveMenuButton(){
        return activeMenuButton;
    }
}