using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft;
        private float moveRight;
        private const float MOVEMENT_SPEED = 0.01f;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            moveLeft = 0.0f;
            moveRight = 0.0f;
        }
        public void Render() {
            entity.RenderEntity();
        }

        public void Move() {
            if (shape.Direction != -1 || shape.Direction != 1){
                shape.Move();
            }
        }

        public void SetMoveLeft(bool val){
            if (val){
                moveLeft += MOVEMENT_SPEED;
                UpdateDirection();
            }
            else {
                moveLeft = 0.0f;
                UpdateDirection();
            }
        }

        public void SetMoveRight(bool val){
            if (val){
                moveRight += MOVEMENT_SPEED;
                UpdateDirection();
            }
            else {
                moveRight = 0.0f;
                UpdateDirection();
            }
        }

        private void UpdateDirection(){
            shape.Direction = (moveLeft + moveRight,0);
        }
    }
}