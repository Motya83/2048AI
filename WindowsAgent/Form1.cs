using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agent2048;
using System.Drawing.Imaging;

namespace WindowsAgent
{
    public partial class BoardEstimator : Form
    {
        public Color hex2color(string hex)
        {
            int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(255, r, g, b);
        }

        public BoardEstimator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<Color, int> colorMap = new Dictionary<Color, int>() { 
				{ hex2color("eee4da"), 1},
				{ hex2color("ede0c8"), 2},
				{ hex2color("f2b179"), 3},
				{ hex2color("f59563"), 4},
				{ hex2color("f67c5f"), 5},
				{ hex2color("f65e3b"), 6},
				{ hex2color("edcf72"), 7},
				{ hex2color("edcc61"), 8},
				{ hex2color("edc850"), 9},
				{ hex2color("edc53f"), 10},
				{ hex2color("edc22e"), 11},
				{ hex2color("3c3a32"), 12}

			};

            Point loc = estimateLocationOfGameOnScreen(Color.FromArgb(255, 187, 173, 160));
            Size size = new Size(500, 500);

            State2048 initS = estimateBoardStateFromScreen(loc, size, colorMap);
            this.txtBoardEstimate.Text = initS.ToString();
        }

        private State2048 estimateBoardStateFromScreen(Point loc, Size size, Dictionary<Color, int> colorMap)
        {
            Bitmap gameBmp = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(gameBmp);
            g.CopyFromScreen(loc, new Point(0, 0), size);
            g.Flush();

            int xOffset = (int)(size.Width * .05);
            int yOffset = (int)(size.Height * .05);

            State2048 state = new State2048(4, 4);
            for (int r = 0; r < state.rows; r++)
            {
                for (int c = 0; c < state.cols; c++)
                {
                    int sampleX = c * size.Width / state.cols + xOffset;
                    int sampleY = r * size.Height / state.rows + xOffset;

                    int tileVal;
                    if (colorMap.TryGetValue(gameBmp.GetPixel(sampleX, sampleY), out tileVal))
                        state.grid[r, c] = tileVal;
                    else
                        state.grid[r, c] = 0;

                    g.FillRectangle(Pens.White.Brush, sampleX, sampleY, 1, 1);
                }
            }
            this.pbScreenGrab.Image = gameBmp;
            return state;
        }

        private static Point estimateLocationOfGameOnScreen(Color targetColor)
        {
            Bitmap gameBmp = new Bitmap(3200, 1080);
            Graphics g = Graphics.FromImage(gameBmp);
            g.CopyFromScreen(0, 0, 0, 0, gameBmp.Size);
            g.Flush();

            BitmapData locked = gameBmp.LockBits(new Rectangle(0, 0, gameBmp.Width, gameBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            bool found = false;
            int r = -1;
            int c = -1;



            unsafe
            {
                PixelData* pixelPtr = (PixelData*)(void*)locked.Scan0;

                //Iterate through rows and columns
                for (int row = 0; row < gameBmp.Height & !found; row++)
                {
                    for (int col = 0; col < gameBmp.Width & !found; col++)
                    {
                        if (pixelPtr->R == targetColor.R &&
                            pixelPtr->G == targetColor.G &&
                            pixelPtr->B == targetColor.B)
                        {
                            //found= true;
                            if (r == -1 || (col < c))
                            {
                                r = row;
                                c = col;
                            }
                        }

                        //Update the pointer
                        pixelPtr++;
                    }
                }
            }

            return new Point(c, r);

        }
		
    }
}
