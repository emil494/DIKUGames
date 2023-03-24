namespace galagaTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Galaga;
using Galaga.Squadron;
using DIKUArcade.GUI;

public class SquadronTests{
    [SetUp] 
    public void Setup(){
        blue = ImageStride.CreateStrides (4, 
            Path.Combine("Assets", "Images", "BlueMonster.png"));
        red = ImageStride.CreateStrides (2, 
            Path.Combine("Assets", "Images", "RedMonster.png"));
    }
    private ISquadron? squadron;
    private List<Image> blue;
    private List<Image> red;

    private bool isTrue (bool arg){
        return arg == true;
    }

    [Test]
    public void TestCheckered(){
        squadron = new CheckeredSquadron();
        var expected = new List<Vec2F>();
        var relationx = new List<bool>();
        var relationy = new List<bool>();
        var relation = new List<bool>();
        for (int j = 0; j < 2; j++) {
            for (int i = 0; i < 8; i++) {
                if ((i+j) % 2 == 0) {
                    expected.Add((new Enemy(
                        new DynamicShape(
                            new Vec2F(0.1f + (float)i * 0.1f, 0.9f - (float)j * 0.1f), 
                                new Vec2F(0.1f, 0.1f)),
                            new ImageStride(80, blue),
                            new ImageStride(80, red))).Shape.Position);
                }
            }
        }
        squadron.CreateEnemies(blue, red);
        var rX = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.X == expected[rX].X){
                relationx.Add(true);
                rX++;
            } else {
                relationx.Add(false);
                rX++;
            }
        });
        var rY = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.Y == expected[rY].Y){
                relationy.Add(true);
                rY++;
            } else {
                relationy.Add(false);
                rY++;
            }
        });
        for (var r = 0; r < relationx.Count(); r++){
            if (relationx[r] == relationy[r]){
                relation.Add(true);
            } else {
                relation.Add(false);
            }
        }
        Assert.True(relation.TrueForAll(isTrue));
    }

    [Test]
    public void TestLine(){
        squadron = new LineSquadron();
        var expected = new List<Vec2F>();
        var relationx = new List<bool>();
        var relationy = new List<bool>();
        var relation = new List<bool>();
        for (int i = 0; i <= 8; i++) {
            expected.Add((new Enemy(
                new DynamicShape(
                    new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, blue),
                    new ImageStride(80, red))).Shape.Position);
        }
        squadron.CreateEnemies(blue, red);
        var rX = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.X == expected[rX].X){
                relationx.Add(true);
                rX++;
            } else {
                relationx.Add(false);
                rX++;
            }
        });
        var rY = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.Y == expected[rY].Y){
                relationy.Add(true);
                rY++;
            } else {
                relationy.Add(false);
                rY++;
            }
        });
        for (var r = 0; r < relationx.Count(); r++){
            if (relationx[r] == relationy[r]){
                relation.Add(true);
            } else {
                relation.Add(false);
            }
        }
        Assert.True(relation.TrueForAll(isTrue));
    }

    [Test]
    public void TestPyramid(){
        squadron = new PyramidSquadron();
        var expected = new List<Vec2F>();
        var relationx = new List<bool>();
        var relationy = new List<bool>();
        var relation = new List<bool>();
        for (int i = 1; i < 7; i++) {
                expected.Add((new Enemy(
                    new DynamicShape(
                        new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, blue),
                        new ImageStride(80, red))).Shape.Position);
            }
            for (int i = 2; i < 6; i++) {
                expected.Add((new Enemy(
                    new DynamicShape(
                        new Vec2F(0.1f + (float)i * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, blue),
                        new ImageStride(80, red))).Shape.Position);
            }
            for (int i = 3; i < 5; i++) {
                expected.Add((new Enemy(
                    new DynamicShape(
                        new Vec2F(0.1f + (float)i * 0.1f, 0.7f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, blue),
                        new ImageStride(80, red))).Shape.Position);
            }
        squadron.CreateEnemies(blue, red);
        var rX = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.X == expected[rX].X){
                relationx.Add(true);
                rX++;
            } else {
                relationx.Add(false);
                rX++;
            }
        });
        var rY = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.Y == expected[rY].Y){
                relationy.Add(true);
                rY++;
            } else {
                relationy.Add(false);
                rY++;
            }
        });
        for (var r = 0; r < relationx.Count(); r++){
            if (relationx[r] == relationy[r]){
                relation.Add(true);
            } else {
                relation.Add(false);
            }
        }
        Assert.True(relation.TrueForAll(isTrue));
    }

    [Test]
    public void TestSquare(){
        squadron = new SquareSquadron();
        var expected = new List<Vec2F>();
        var relationx = new List<bool>();
        var relationy = new List<bool>();
        var relation = new List<bool>();
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++){
                expected.Add((new Enemy(
                    new DynamicShape(
                        new Vec2F(0.35f + (float)i * 0.1f, 0.9f - (float)j * 0.1f), 
                        new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, blue),
                        new ImageStride(80, red)
                )).Shape.Position);
            }
        }
        squadron.CreateEnemies(blue, red);
        var rX = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.X == expected[rX].X){
                relationx.Add(true);
                rX++;
            } else {
                relationx.Add(false);
                rX++;
            }
        });
        var rY = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.Y == expected[rY].Y){
                relationy.Add(true);
                rY++;
            } else {
                relationy.Add(false);
                rY++;
            }
        });
        for (var r = 0; r < relationx.Count(); r++){
            if (relationx[r] == relationy[r]){
                relation.Add(true);
            } else {
                relation.Add(false);
            }
        }
        Assert.True(relation.TrueForAll(isTrue));
    }

    [Test]
    public void TestV(){
        squadron = new VSquadron();
        var expected = new List<Vec2F>();
        var relationx = new List<bool>();
        var relationy = new List<bool>();
        var relation = new List<bool>();
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 5; j++){
                if (j == 2 && i == 0 || j == 0 && i == 1 || j == 4 && i == 1){}
                else{
                    expected.Add((new Enemy(
                        new DynamicShape(
                            new Vec2F(0.25f + (float)j * 0.1f, 0.9f - (float)i * 0.1f), 
                            new Vec2F(0.1f, 0.1f)),
                            new ImageStride(80, blue),
                            new ImageStride(80, red)
                    )).Shape.Position);
                }
            }
        }
        squadron.CreateEnemies(blue, red);
        var rX = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.X == expected[rX].X){
                relationx.Add(true);
                rX++;
            } else {
                relationx.Add(false);
                rX++;
            }
        });
        var rY = 0;
        squadron.Enemies.Iterate(enemy => {
            if (enemy.Shape.Position.Y == expected[rY].Y){
                relationy.Add(true);
                rY++;
            } else {
                relationy.Add(false);
                rY++;
            }
        });
        for (var r = 0; r < relationx.Count(); r++){
            if (relationx[r] == relationy[r]){
                relation.Add(true);
            } else {
                relation.Add(false);
            }
        }
        Assert.True(relation.TrueForAll(isTrue));
    }
}