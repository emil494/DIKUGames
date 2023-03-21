using System;
using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga.GalagaStates;

public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;

    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.InitializeGameState();
        }
        return MainMenu.instance;
    }

    private void InitializeGameState(){
        instance.menuButtons = new Text[] {
            new Text ("New Game", new Vec2F(0.3f, 0.1f), new Vec2F(0.5f, 0.5f)), 
            new Text ("Quit", new Vec2F(0.3f, 0.0f), new Vec2F(0.5f, 0.5f))};
        instance.menuButtons[0].SetColor(System.Drawing.Color.Red);
        instance.menuButtons[1].SetColor(System.Drawing.Color.Coral);
        backGroundImage = new Entity(
            new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f,1.0f)),
            new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
        activeMenuButton = 0;
        maxMenuButtons = 1;
    }

    public void ResetState(){
        instance.InitializeGameState();
    }

    public void UpdateState(){}

    public void RenderState(){
        backGroundImage.RenderEntity();
        foreach (Text button in menuButtons){
            button.RenderText();
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
        switch (action){
            case KeyboardAction.KeyPress:
                break;
            case KeyboardAction.KeyRelease:
                switch (key){
                    case KeyboardKey.Up:
                        menuButtons[activeMenuButton].SetColor(System.Drawing.Color.Coral);
                        activeMenuButton = 0;
                        menuButtons[activeMenuButton].SetColor(System.Drawing.Color.Red);
                        break;
                    case KeyboardKey.Down:
                        menuButtons[activeMenuButton].SetColor(System.Drawing.Color.Coral);
                        activeMenuButton = maxMenuButtons;
                        menuButtons[activeMenuButton].SetColor(System.Drawing.Color.Red);
                        break;
                    case KeyboardKey.Enter:
                        if (activeMenuButton == 0){
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_RUNNING"
                                }
                            );
                        } else if (activeMenuButton == maxMenuButtons){
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent {
                                EventType = GameEventType.WindowEvent, 
                                Message = "CLOSE_GAME"});
                        }
                        break;
                }
                break;
        }
    }
    
}