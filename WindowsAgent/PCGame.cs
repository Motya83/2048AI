using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agent2048;

namespace WindowsAgent
{
    public partial class PCGame : Form
    {
        State2048 currentGame = null;
        BackgroundWorker bw = new BackgroundWorker();
        Graphics g = null;


        private int Depth  { get; set; }
        private string Algorithm { get; set; }

        public PCGame()
        {
            InitializeComponent();
            g = this.CreateGraphics();            
        }

        private Image GetIcon(int tileValue)
        {
            Image result = null;
            switch(tileValue)
            {
                case 0: 
                    result =  ImageResources._0;
                    break;
                case 1:
                    result = ImageResources._2;
                    break;
                case 2:
                    result = ImageResources._4;
                    break;
                case 3:
                    result = ImageResources._8;
                    break;
                case 4:
                    result = ImageResources._16;
                    break;
                case 5:
                    result = ImageResources._32;
                    break;
                case 6:
                    result = ImageResources._64;
                    break;
                case 7:
                    result = ImageResources._128;
                    break;
                case 8:
                    result = ImageResources._256;
                    break;
                case 9:
                    result = ImageResources._512;
                    break;
                case 10:
                    result = ImageResources._1024;
                    break;
                case 11:
                    result = ImageResources._2048b;
                    break;
                case 12:
                    result = ImageResources._4096;
                    break;
                case 13:
                    result = ImageResources._8192;
                    break;
                case 14:
                    result = ImageResources._16384;
                    break;
                case 15:
                    result = ImageResources._32768;
                    break;

            }
            return result;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        public void NewGame()
        {
            this.lblGameStatus.Text = "In progress";
            State2048 newState = new State2048(4, 4, true);
            //this.txtBoard.Text = newState.ToString();
            this.currentGame = newState;
            g.DrawImage(ImageResources.Board, 0, 0);
            RenderState(currentGame);
            
        }

        private void btnAIStart_Click(object sender, EventArgs e)
        {
            
            if (bw.IsBusy != true)
            {
                btnAIStart.Text = "Stop AI";
                bw.RunWorkerAsync(sender);
            }
            else
            {
                bw.CancelAsync();
                btnAIStart.Text = "Start AI";
            }
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            List<StateTrans> movesAvailable = currentGame.getAllMoveStates();
            if (movesAvailable.Count != 0)
            {
                bool boardMoved = false;
                // user turn
                if (keyData == Keys.Left && movesAvailable.Count(x => x.dir == MoveDir.Left) > 0)
                {
                    boardMoved = true;
                    currentGame.pushLeft();
                }
                else if (keyData == Keys.Right && movesAvailable.Count(x => x.dir == MoveDir.Right) > 0)
                {
                    boardMoved = true;
                    currentGame.pushRight();
                }
                else if (keyData == Keys.Up && movesAvailable.Count(x => x.dir == MoveDir.Up) > 0)
                {
                    boardMoved = true;
                    currentGame.pushUp();
                }
                else if (keyData == Keys.Down && movesAvailable.Count(x => x.dir == MoveDir.Down) > 0)
                {
                    boardMoved = true;
                    currentGame.pushDown();
                }

                if (boardMoved)
                {
                    // adversary
                    currentGame.spawnRandom();

                    // ai for debugging
                    double bestScore = double.MinValue;
                    List<StateTrans> movesBest = new List<StateTrans>();

                    StringBuilder sb = new StringBuilder();

                    List<StateTrans> moves = currentGame.getAllMoveStates();
                    foreach (StateTrans move in moves)
                    {
                        double moveRating = 0;
                        switch (this.Algorithm)
                        {
                            case "Expectimax":
                                moveRating = State2048.expectiminimax(move.state, Depth, false);
                                break;
                            case "Alpha-Beta":
                                moveRating = State2048.alphabetarate(move.state, Depth, double.MinValue, double.MaxValue, false);
                                break;
                            default:
                                moveRating = State2048.minimax(move.state, Depth, false);
                                break;
                        } 
                        sb.AppendLine(move.dir.ToString());
                        sb.AppendLine("best score: " + moveRating.ToString("F2"));
                        sb.AppendLine();
                        if (moveRating > bestScore)
                        {
                            bestScore = moveRating;
                            movesBest.Clear();
                        }

                        if (moveRating == bestScore)
                        {
                            movesBest.Add(move);
                        }
                    }

                    // render board
                    this.txtBoard.Text = sb.ToString();// + //currentGame.SnakeString();
                    this.RenderState(currentGame);
                }
            }
            else
                this.lblGameStatus.Text = "Fail";
            return true; // base.ProcessCmdKey(ref msg, keyData);
        }

        private void PCGame_Load(object sender, EventArgs e)
        {
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            ddlDepth.SelectedText = this.Depth.ToString();
            Algorithm = ddlAlgorithm.SelectedText = "Expectimax";
            NewGame();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (this.currentGame != null)
            {
                
                State2048 s = currentGame;
                while (true)
                {
                    double bestScore = double.MinValue;
                    List<StateTrans> movesBest = new List<StateTrans>();

                    List<StateTrans> moves = s.getAllMoveStates();
                    foreach (StateTrans move in moves)
                    {
                        double moveRating = 0;
                        switch (this.Algorithm)
                        {
                            case "Expectimax":
                                moveRating = State2048.expectiminimax(move.state, Depth, false);
                                break;
                            case "Alpha-Beta": 
                                moveRating = State2048.alphabetarate(move.state, Depth, double.MinValue, double.MaxValue, false);
                                break;
                            default:
                                moveRating = State2048.minimax(move.state, Depth, false);
                                break;
                        } 

                        if (moveRating > bestScore)
                        {
                            bestScore = moveRating;
                            movesBest.Clear();
                        }

                        if (moveRating == bestScore)
                        {
                            movesBest.Add(move);
                        }
                    }

                    if (movesBest.Count == 0)
                        break;

                    int sleepPause = 10;
                    System.Threading.Thread.Sleep(sleepPause);

                    // our turn

                    #region AI
                    if (movesBest[0].dir == MoveDir.Left)
                        s.pushLeft();

                    if (movesBest[0].dir == MoveDir.Right)
                        s.pushRight();

                    if (movesBest[0].dir == MoveDir.Up)
                        s.pushUp();

                    if (movesBest[0].dir == MoveDir.Down)
                        s.pushDown();
                    #endregion

                    // adversary turn
                    s.spawnRandom();

                    // update interface
                    worker.ReportProgress(0, s);

                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }

                }
            }
            
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bool gameover = false;
            State2048 s = e.UserState as State2048;
            
            // draw current state
            //this.txtBoard.Text = s.ToString();
            this.RenderState(s);

            // loss?
            if (s.getAllMoveStates().Count == 0)
            {
                this.lblGameStatus.Text = "Fail";
                gameover = true;
            }
            
            // win?
            if (s.getMaxValue() >= 11 && !cbKeepGoing.Checked)
            {
                this.lblGameStatus.Text = "Win";
                gameover = true;
            }

            if (gameover)
            {
                bw.CancelAsync();
                btnAIStart.Text = "Start AI";
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // nothing
        }

        private void RenderState(State2048 s)
        {
            try
            {
                int rows = 4, cols = 4;
                int imgx = 0, imgy = 0;

                for (int r = 0; r < rows; r++)
                {
                    imgy = 15 * (r + 1) + (106 * r);
                    for (int c = 0; c < cols; c++)
                    {
                        int tileValue = s.grid[r, c];
                        Image tileImage = GetIcon(tileValue);
                        imgx = 15 * (c + 1) + (106 * c);
                        g.DrawImage(tileImage, imgx, imgy, 107, 107);
                    }
                }

                g.Flush();
            }
            catch(Exception ex)
            {
                this.lblGameStatus.Text = "Rendering error: " + ex.Message;
            }
        }

        private void PCGame_Activated(object sender, EventArgs e)
        {
            this.RenderState(this.currentGame);
        }

        private void btnSetSettings_Click(object sender, EventArgs e)
        {
            Button btnSave = sender as Button;
            if (btnSave != null)
            {
                if (btnSave.Text == "Save Settings")
                {
                    ddlAlgorithm.Enabled = false;
                    ddlDepth.Enabled = false;
                    btnSave.Text = "Change Settings";
                }
                else
                {
                    ddlAlgorithm.Enabled = true;
                    ddlDepth.Enabled = true;
                    btnSave.Text = "Save Settings";
                }
            }
        }

        private void ddlDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedDepth = 0;
            ComboBox cbDepth = sender as ComboBox;
            int.TryParse(cbDepth.SelectedItem.ToString(), out selectedDepth);
            this.Depth = selectedDepth;
        }

        private void ddlAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbAlgorithm = sender as ComboBox;
            this.Algorithm = cbAlgorithm.SelectedItem.ToString();
        }
    }
}
