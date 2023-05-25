using Breakout;

namespace Breakout.Powers;

public interface IEffect{
    void Move(){}
    void Apply(){}
    void PlayerCollision(Player player){}
}