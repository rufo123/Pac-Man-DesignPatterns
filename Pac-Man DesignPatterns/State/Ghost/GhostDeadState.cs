using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pac_Man_DesignPatterns.State.Ghost
{
    public class GhostDeadState : GhostStateAbs
    {
        private readonly Vector2 aGhostHousePos;

        private bool aReachedHome;

        private Texture2D aAlternativeTexture;

        private readonly string aAlternativeTexturePath;

        public override Vector2 GetTargetPos()
        {
            return aGhostHousePos;
        }

        public override void ExecuteLogic()
        {
            Ghost.ChangeState(GhostStateEnum.Home);
            Ghost.Position = aGhostHousePos;
        }

        public GhostDeadState(Entities.MovableEntity.Ghosts.Ghost parGhost, Vector2 parGhostHousePos, string parAlternativeTexturePath) : base(parGhost)
        {
            this.aGhostHousePos = parGhostHousePos;
            aAlternativeTexturePath = parAlternativeTexturePath;

        }

        public override void Update(GameTime parGameTime)
        {

            if (Ghost.GetRectangleHitBox().Intersects(new Rectangle((int)this.aGhostHousePos.X, (int)this.aGhostHousePos.Y, (int)this.aGhostHousePos.X * Ghost.Size, (int)this.aGhostHousePos.Y * Ghost.Size)))
            {
                aReachedHome = true;
                Timer = TimerThreshold;
            }

            if (aReachedHome)
            {
                base.Update(parGameTime);
            }
        }

        public override void LoadAlternativeTexture(ContentManager parContent)
        {
            if (aAlternativeTexturePath is not null)
            {
                aAlternativeTexture = parContent.Load<Texture2D>(aAlternativeTexturePath);
            }
        }

        public override Texture2D GetAlternativeTexture()
        {
            return aAlternativeTexture;
        }

        public override float GetSpeed()
        {
            if (Ghost.IsBlocked)
            {
                return Ghost.GetDefaultSpeed();
            }

            return Ghost.GetDefaultSpeed() * 1.5f;


        }
    }
}
