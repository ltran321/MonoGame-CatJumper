using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace CatJumper
{
    public class CollisionManager2 : GameComponent
    {
        private FlyingEnemy flyEnemy;
        private Cat cat;
        private SoundEffect hitSound;
        private bool isHit = false;

        public CollisionManager2(Game game,
            Cat cat,
            FlyingEnemy flyEnemy,
            SoundEffect hitSound) : base(game)
        {
            this.cat = cat;
            this.flyEnemy = flyEnemy;
            this.hitSound = hitSound;
        }

        public bool IsHit { get => isHit; set => isHit = value; }

        public override void Update(GameTime gameTime)
        {
            Rectangle catRect = cat.getBounds();
            Rectangle flyEnRect = flyEnemy.getBounds();

            if (catRect.Intersects(flyEnRect))
            {
                isHit = true;
            }

            if (isHit == true)
            {
                isHit = false;
                cat.Hit = true;
                cat.Updated = false;
                hitSound.Play();
            }
            base.Update(gameTime);
        }
    }
}
