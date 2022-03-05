using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace CatJumper
{
    public class CollisionManager : GameComponent
    {
        private WalkingEnemy enemy;
        private Cat cat;
        private SoundEffect hitSound;
        private bool isHit = false;

        public CollisionManager(Game game,
            WalkingEnemy enemy,
            Cat cat,
            SoundEffect hitSound) : base(game)
        {
            this.enemy = enemy;
            this.cat = cat;
            this.hitSound = hitSound;
        }

        public bool IsHit { get => isHit; set => isHit = value; }

        public override void Update(GameTime gameTime)
        {
            Rectangle walkEnRect = enemy.getBounds();
            Rectangle catRect = cat.getBounds();
            if (catRect.Intersects(walkEnRect))
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
