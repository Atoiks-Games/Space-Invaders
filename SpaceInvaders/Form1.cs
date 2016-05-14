using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {

        private IList<PictureBox> enemies = new List<PictureBox>(16);
        private IList<Bullet> bullets = new List<Bullet>(16);
        private int level = 7;
        private int genTick = 0;
        private const int picWid = 16;
        private readonly Random RND = new Random();
        private const string OWNER_PLAYER = "player";
        private const string OWNER_ENEMY = "enemy";
        private long player1bulletTick = 0;

        private RUNNING_STATUS isRunning = RUNNING_STATUS.RUNNING;

        enum RUNNING_STATUS
        {
            RUNNING,
            WIN,
            LOST
        }

        public Form1()
        {
            InitializeComponent();

            DisableResumeRestartBtn();

            player1.Size = new Size(16, 16);
            player1.Location = new Point(this.Size.Width / 2, 256);
            player1.Image = imglist.Images[1]; // Player!!!

            MessageBox.Show("Left and right arrows to move. Space bar to start / fire bullet");

            InitializeGame();
        }

        private void InitializeGame()
        {
            isRunning = RUNNING_STATUS.RUNNING;
            int tmpWidth = 0;
            int tmpHeight = 0;

            int lines = level / 15 + 1;
            int currentIndex = 0;

            for (int line = 0; line < lines; line++)
            {
                for (int i = 0; i < level - line * 15; i++)
                {
                    currentIndex = line * 15 + i;

                    enemies.Add(new PictureBox());
                    enemies[currentIndex].Image = imglist.Images[0]; // Attackers!!!
                    enemies[currentIndex].Size = new Size(16, 16);
                    enemies[currentIndex].Location = new Point(tmpWidth + picWid, tmpHeight);
                    this.Controls.Add(enemies[i]);
                    tmpWidth += picWid * 2;
                }

                tmpWidth = 0;
                tmpHeight += picWid + 8;
            }

            atkTimer.Start();

            // Place other Init stuff here
        }

        private void genEnemyBullet(PictureBox enemy)
        {
            if (bullets.Count < 256)
            {
                if (RND.Next(100) % 17 == 0)
                {
                    if (genTick++ % 38 == 0)
                    {
                        genTick = 1;
                        var cbullet = new Bullet(new PictureBox(), OWNER_ENEMY);
                        cbullet.Box.Location = new Point(enemy.Location.X, enemy.Location.Y + enemy.Size.Height);
                        cbullet.Box.Image = imglist.Images[2];
                        cbullet.Box.Size = new Size(16, 16);
                        bullets.Add(cbullet);
                        Controls.Add(cbullet.Box);
                    }
                }
            }
        }

        private void moveBullets()
        {
            Bullet bul;
        start:
            for (int bulletLoop = 0; bulletLoop < bullets.Count(); bulletLoop++)
            {
                bul = bullets[bulletLoop];
                PictureBox enemy;
                for (int enemyLoop = 0; enemyLoop < enemies.Count(); enemyLoop++)
                {
                    enemy = enemies[enemyLoop];
                    if (bul.Owner == OWNER_ENEMY && bul.IntersectsWith(player1.Bounds))
                    {
                        isRunning = RUNNING_STATUS.LOST;
                        return;
                    }
                    if (bul.Owner == OWNER_PLAYER && bul.IntersectsWith(enemy.Bounds))
                    {
                        Controls.Remove(enemy);
                        enemies.RemoveAt(enemyLoop--); // i-- to decrease one index
                        enemy.Location = new Point(enemy.Location.X, -512);
                    }
                    enemyLoop = Math.Max(0, enemyLoop);
                    if (enemyLoop >= enemies.Count())
                    {
                        // Player wins!
                        isRunning = RUNNING_STATUS.WIN;
                        return;
                    }
                    genEnemyBullet(enemies[enemyLoop]);
                }
                if (enemies.Count() < 1)
                {
                    isRunning = RUNNING_STATUS.WIN;
                    return;
                }
                if (bul.Location.Y < 0 || bul.Location.Y > Size.Height)
                {
                    Controls.Remove(bul.Box);
                    bullets.RemoveAt(bulletLoop--); // i-- to decrease one index

                    goto start; // Restart the bullet iteration loop
                }
            }
            for (int i = 0; i < bullets.Count(); i++)
            {
                bul = bullets[i];
                if (bul.Owner == OWNER_PLAYER)
                    bul.Location = new Point(bul.Location.X, bul.Location.Y - 2);
                else
                    bul.Location = new Point(bul.Location.X, bul.Location.Y + 4);
            }
        }

        private void atkTimer_Tick(object sender, EventArgs e)
        {
            moveBullets();
            if (isRunning != RUNNING_STATUS.RUNNING)
            {
                atkTimer.Stop(); // Very important
                if (isRunning == RUNNING_STATUS.LOST)
                {
                    ClearBullets();
                    MessageBox.Show("Ha! Gotta be quicker boy! You died on level " + (level - 7));
                    btnRestart.Visible = btnRestart.Enabled = true;
                    return;
                }

                for (int i = 0; i < bullets.Count(); i++)
                {
                    if (bullets[i].Owner == OWNER_PLAYER)
                    {
                        Controls.Remove(bullets[i].Box);
                        bullets.RemoveAt(i--);
                    }
                }
                if (level == 60)
                {
                    MessageBox.Show("Yay! You beat the game!");
                    btnRestart.Visible = btnRestart.Enabled = true;
                }
                else
                {
                    var dr = MessageBox.Show("You won! Continue?", "Win", MessageBoxButtons.YesNo);
                    level++;
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            InitializeGame();
                            break;
                        default:
                            btnResume.Visible = btnResume.Enabled = true;
                            btnRestart.Visible = btnRestart.Enabled = true;
                            break;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isRunning == RUNNING_STATUS.RUNNING)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:
                        if (DateTime.Now.Ticks - player1bulletTick > 16000000L)
                        {
                            player1bulletTick = DateTime.Now.Ticks;
                            var cbullet = new Bullet(new PictureBox(), OWNER_PLAYER);
                            cbullet.Box.Location = new Point(player1.Location.X, player1.Location.Y);
                            cbullet.Box.Image = imglist.Images[2];
                            cbullet.Box.Size = new Size(16, 16);
                            bullets.Add(cbullet);
                            Controls.Add(cbullet.Box);
                        }
                        break;
                    case Keys.Left:
                        if (player1.Location.X > 0)
                            player1.Location = new Point(player1.Location.X - 2, player1.Location.Y);
                        break;
                    case Keys.Right:
                        if (player1.Location.X < Size.Width - 40)
                            player1.Location = new Point(player1.Location.X + 2, player1.Location.Y);
                        break;
                }
            }
        }

        private void ClearBullets()
        {
            for (int i = 0; i < bullets.Count(); i++)
            {
                Controls.Remove(bullets[i].Box);
                bullets.RemoveAt(i--);
            }
        }

        private void ClearEnemies()
        {
            for (int i = 0; i < enemies.Count(); i++)
            {
                Controls.Remove(enemies[i]);
                enemies.RemoveAt(i--);
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            DisableResumeRestartBtn();
            InitializeGame();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            DisableResumeRestartBtn();
            level = 7;
            ClearEnemies();
            ClearBullets();
            InitializeGame();
        }

        private void DisableResumeRestartBtn()
        {
            btnResume.Visible = btnResume.Enabled = false;
            btnRestart.Visible = btnRestart.Enabled = false;
        }
    }
}
