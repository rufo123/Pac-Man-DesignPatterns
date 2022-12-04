using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Game;

namespace Pac_Man_DesignPatterns.State.Ghost
{
    public class GhostFrightenedState : GhostStateAbs
    {

        private readonly GameManager aGameManager;

        private Texture2D aAlternativeTexture;

        private readonly string aAlternativeTexturePath;

        public GhostFrightenedState(Entities.MovableEntity.Ghosts.Ghost parGhost, string parFrightenedTexturePath) : base(parGhost)
        {
            aTimerThreshold = 10;
            aGameManager = GameManager.GetInstance();
            aAlternativeTexturePath = parFrightenedTexturePath;
        }

        public override Vector2 GetTargetPos()
        {
            if (Ghost is Red)
            {
                return aGameManager.GetRandomTile(0);
            }
            else if (Ghost is Cyan)
            {
                return aGameManager.GetRandomTile(1);
            }
            else if (Ghost is Orange)
            {
                return aGameManager.GetRandomTile(2);
            }
            else
            {
                return aGameManager.GetRandomTile(3);
            }
        }

        public override void ExecuteLogic()
        {
            if (!IsTimerActive())
            {
                Ghost.ChangeState(GhostStateEnum.Chase);
            }
        }

        public override Texture2D GetAlternativeTexture()
        {
            return aAlternativeTexture;
        }

        public override void LoadAlternativeTexture(ContentManager parContent)
        {
            if (aAlternativeTexturePath is not null)
            {
              aAlternativeTexture = parContent.Load<Texture2D>(aAlternativeTexturePath);
            }
        }


        public override float GetSpeed()
        {
            return Ghost.GetDefaultSpeed() / 2;
        }
    }
}
