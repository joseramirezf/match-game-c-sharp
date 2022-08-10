namespace Match_Game
{
    public partial class Game : Form
    {
        private Random random = new Random();
        private List<string> icons = new List<string>()
       {
           "!" , "!" , "N" , "N" ,
           "O" , "O" , "," , "," ,
           "B" , "B" , "V" , "V" ,
           "W" , "W" , "Z" , "Z"
       };

        Label? firstClicked = null;
        Label? secondClicked = null;

        public Game()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in table.Controls)
            {
                Label? iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                return;
            }
            Label? clickLabel = sender as Label;
            if (clickLabel != null)
            {
                if (clickLabel.ForeColor == Color.Black)
                {
                    return;
                }
                if (firstClicked == null)
                {
                    firstClicked = clickLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                secondClicked = clickLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in table.Controls)
            {
                Label? iconLabel = control as Label;
                if(iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }
                }
            }
            MessageBox.Show("You Won the match game!", "Congratulations!");
            Close();
        }
    }
}

       