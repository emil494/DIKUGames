namespace TestDIKUArcade.KeyEventsTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;

public class Game : DIKUGame {

    private SortedDictionary<int, string> test;
    private Random ran;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
        test = new SortedDictionary<int, string>();
        ran = new Random();
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Num_1:
                window.SetClearColor(128, 52, 43);
                break;
            case KeyboardKey.Num_2:
                window.SetClearColor(28, 108, 218);
                break;
            case KeyboardKey.I:
                var n = ran.Next();
                if (!test.TryAdd(n, $"insert({n})")) {
                    Console.WriteLine($"Tried to insert a duplicate element.");
                }
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }

    }

    public override void Render() { }

    public override void Update() {
        Console.WriteLine($"Dictionary Size: {test.Count}");
    }
}