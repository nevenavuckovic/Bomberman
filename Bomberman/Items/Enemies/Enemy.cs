using Bomberman.Game;
using System.Threading;
using System.Windows.Controls;

namespace Bomberman.Items.Enemies
{
    public abstract class Enemy
    {
        protected Thread thread;

        public int X { get; protected set; }

        public int Y { get; protected set; }
        public int Points { get; protected set; }
        public bool Alive { get; protected set; }

        public Image Image { get; protected set; }

        protected Enemy(int x, int y) {
            SetImage();
            SetPoints();
            Alive = true;
            X = x;
            Y = y;
        }

        protected abstract void SetImage();

        public void StartMoving(NewGame newGame)
        {
            thread = new Thread(() => Move(newGame));
            thread.Start();
        }
        protected abstract void Move(NewGame newGame);

        protected abstract void SetPoints();
        public void DestroyMe(NewGame newGame, bool killed) {
            Alive = false;
            thread.Abort();
            if (killed)
            {
                newGame.Player.Score += Points;
            }

            newGame.Enemies[X, Y] = null;
            newGame.GameWindow.LevelMap.Children.Remove(Image);
        }
    }
}
