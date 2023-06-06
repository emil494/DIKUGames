using System;
using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.States;

public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;

    /// <summary>
    /// Returns instance of itself and creates itself if null
    /// </summary>
    /// <returns> Itself </returns>
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
        }
        MainMenu.instance.InitializeGameState();
        return MainMenu.instance;
    }

    /// <summary>
    /// Initializes the state
    /// </summary>
    private void InitializeGameState(){
        menuButtons = new Text[] {
            new Text ("New Game", new Vec2F(0.3f, 0.1f), new Vec2F(0.5f, 0.5f)), 
            new Text ("Quit", new Vec2F(0.3f, 0.0f), new Vec2F(0.5f, 0.5f))};
        menuButtons[0].SetColor(System.Drawing.Color.Red);
        menuButtons[1].SetColor(System.Drawing.Color.Coral);
        backGroundImage = new Entity(
            new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f,1.0f)),
            new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
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
                                    StringArg1 = "GAME_RUNNING"
                                }
                            );
                        }
                        break;
                }
            break;
        }
    }

    
    /// <summary>
    /// Public getter for the Active Menu Buttons
    /// </summary>
    /// <returns>What number the active menu button is</returns>
    public int GetActiveMenuButton(){
        return activeMenuButton;
    }
}