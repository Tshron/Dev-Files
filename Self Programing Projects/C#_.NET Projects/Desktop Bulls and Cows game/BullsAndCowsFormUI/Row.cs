using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    public class Row
    {
        private static int m_NumberOfExistingRows = 0;

        public static int NumberOfExistingRows
        {
            set
            {
                m_NumberOfExistingRows = 0;
            }
        }

        public int m_RowWidth = 380;

        private int m_Width = 278;
        public int Width
        {
            get { return m_Width; }
        }

        private int m_Height = 64;
        public int Height
        {
            get { return m_Height; }
        }

        private int m_YAxis;
        public int YAxis
        {
            get { return m_YAxis; }
        }

        private Player[] m_Cells;
        public Player[] Cells
        {
            get { return m_Cells; }
            set { m_Cells = value; }
        }

        public int Columns
        {
            get { return m_Cells.Length; }
        }

        private PictureBox m_Picture = new PictureBox();
        public PictureBox Picture
        {
            get { return m_Picture; }
        }

        private PictureBox m_Statistic = new PictureBox();
        public PictureBox Statistic
        {
            get { return m_Statistic; }
        }

        private Statistic[] m_Hits;
        public Statistic[] Hits
        {
            get { return m_Hits; }
            set { m_Hits = value; }
        }

        public string[] ColorsInRow
        {
            get
            {
                string[] colors = new string[4];
                for(int i = 0; i < m_Cells.Length; i++)
                {
                    if(m_Cells[i].Color != null)
                    {
                        colors[i] = m_Cells[i].Color;
                    }
                }
                
                return colors;
            }
        }

        private Player[] m_ResultCells;
        public Player[] ResultCells
        {
            get
            {
                return m_ResultCells;
            }

            set
            {
                m_ResultCells = value;
            }
        }

        public static PictureBox setComputerRow()
        {
            PictureBox computerRow = new PictureBox();
            computerRow.Image = Image.FromFile("../../img/rowComputer3.jpg");
            computerRow.Size = new System.Drawing.Size(278, 64);
            computerRow.SizeMode = PictureBoxSizeMode.StretchImage;
            computerRow.Location = new Point(0, 0);
            return computerRow;
        }

        public Row(int i_PatternLength)
        {
            m_NumberOfExistingRows++;
            m_YAxis = m_Height * m_NumberOfExistingRows;
            m_Cells = new Player[i_PatternLength];
            for (int i = 0; i < m_Cells.Length; i++)
            {
                m_Cells[i] = new Player(i, m_NumberOfExistingRows - 1);
            }
            m_Hits = new Statistic[i_PatternLength];
            for (int i = 0; i < m_Hits.Length; i++)
            {
                m_Hits[i] = new Statistic(i, m_NumberOfExistingRows - 1);
            }
            setRow();
            setStatistic();
        }
        private void setRow()
        {
            m_Picture.Image = Image.FromFile(@"../../img/row7.jpg");
            m_Picture.Size = new Size(m_Width, m_Height);
            m_Picture.SizeMode = PictureBoxSizeMode.StretchImage;
            m_Picture.Location = new Point(0, m_YAxis);
        }
        private void setStatistic()
        {
            m_Statistic.Name = m_NumberOfExistingRows.ToString();
            m_Statistic.Image = Image.FromFile("../../img/lampoff.jpg");
            m_Statistic.Size = new Size(m_RowWidth - m_Width, m_Height);
            m_Statistic.SizeMode = PictureBoxSizeMode.StretchImage;
            m_Statistic.Location = new Point(m_Width, m_YAxis);
            m_Statistic.Enabled = false;
        }

       

        public bool IsRowReady()
        {
            bool isReady = ColorsInRow.Count(x => (x != null) == true) == Cells.Length ? true : false;
            
            return isReady;
        }
    }
}
