using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pac_Man_DesignPatterns.Game;
using Pac_Man_DesignPatterns.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_Man_DesignPatterns.Entities.TileEntity;

namespace Pac_Man_DesignPatterns.Entities.MovableEntity
{
    public class PacMan : MovableEntity, IObservable
    {

        private IList<IObserver> aListObservers;

        private CollisionDetector aCollisionDetector;

        public Utils.IObservable Implementation
        {
            get => default;
            set
            {
            }
        }

        public IObserver IObserver { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public GameManager GameManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UIManager UIManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PacMan(Texture2D parTexture,int parPositionX, int parPositionY, int parSize, CollisionDetector parCollisionDetector) : base( parTexture,parPositionX, parPositionY, parSize, true)
        {
            aListObservers = new List<IObserver>();
            aCollisionDetector = parCollisionDetector;
        }

        public override void Update(GameTime parGameTime)
        {

            if (this.EnqueuedDirection != Direction.NOTHING)
            {

                bool tmpCollided = aCollisionDetector.DetectCollision(this.PredictRectangleNextPos(parGameTime, this.EnqueuedDirection), out Entity tmpEntityCollidedWith);

                if (!tmpCollided || tmpEntityCollidedWith is not Wall)
                {
                    this.ChangeEnqueuedDirectionToDirection();
                }
            }

            bool tmpIsCollisionDetected = aCollisionDetector.DetectCollision(this.PredictRectangleNextPos(parGameTime, this.Direction), out Entity tmpEntityCollidedWithNext);

            if (tmpEntityCollidedWithNext is Wall)
            {
                this.IsBlocked = true;
            }
            else
            {
                this.IsBlocked = false;
            }



            //Debug.WriteLine(tmpIsCollisionDetected);
            Move(parGameTime);

            base.Update(parGameTime);
        }

        public void FoodEaten()
        {
            foreach (var itemObserver in aListObservers)
            {
                itemObserver.Update(new Message(-1));
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

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
