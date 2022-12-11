using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pac_Man_DesignPatterns.Entities.MovableEntity.Ghosts;
using Pac_Man_DesignPatterns.Entities.TileEntity;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.State.PacMan;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity
{
    public class PacMan : MovableEntity, IObservable
    {

        private readonly IList<IObserver> aListObservers;

        private readonly CollisionDetector aCollisionDetector;

        private PacManStateAbs aPacManState;
        private PacManStateAbs aPacManAliveState;
        private PacManStateAbs aPacManDeadState;

        public new bool IsHidden
        {
            get => aIsHidden;
            set => aIsHidden = value;
        }


        public CollisionDetector CollisionDetector => aCollisionDetector;


        public PacMan(string parTexturePath, int parPositionX, int parPositionY, int parSize, CollisionDetector parCollisionDetector, Color parColor) : base(parTexturePath, parPositionX, parPositionY, parSize, true, parColor)
        {
            aListObservers = new List<IObserver>();
            aCollisionDetector = parCollisionDetector;
            InitStates();
        }

        private void InitStates()
        {
            aPacManAliveState = new PacManAliveState(this);
            aPacManDeadState = new PacManDeadState(this);

            aPacManState = aPacManAliveState;
        }

        public override void Update(GameTime parGameTime)
        {
            aPacManState.Update(parGameTime);

            base.Update(parGameTime);
        }


        public void GhostCollision(Ghost parGhost)
        {
            foreach (var itemObserver in aListObservers)
            {
                itemObserver.Update(new Message(MessageCodes.GhostCollision, parGhost));
            }
        }

        public void FoodEaten(Food parFood)
        {
            foreach (var itemObserver in aListObservers)
            {
                itemObserver.Update(new Message(MessageCodes.CookieEaten, parFood));
            }
        }

        public void RegisterObserver(IObserver parObserver)
        {
            aListObservers.Add(parObserver);
        }

        public void RemoveObserver(IObserver parObserver)
        {
            aListObservers.Remove(parObserver);
        }

        public void ChangeState(PacManStateEnum parPacManStateEnum)
        {
            switch (parPacManStateEnum)
            {
                case PacManStateEnum.Alive:
                    aPacManState = aPacManAliveState;
                    break;
                case PacManStateEnum.Dead:
                    aPacManState = aPacManDeadState;
                    break;
            }

            aPacManState.ResetTimer();
        }

        public PacManStateEnum GetState()
        {
            if (aPacManState == aPacManAliveState)
            {
                return PacManStateEnum.Alive;
            }

            if (aPacManState == aPacManDeadState)
            {
                return PacManStateEnum.Dead;
            }

            // Cannot happen...
            return PacManStateEnum.Alive;
        }
    }
}
