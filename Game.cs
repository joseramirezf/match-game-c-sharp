using System.Media;

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
            timer2.Start();
        }

        SoundPlayer sounds = new SoundPlayer();
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
                    sounds.Stream = Properties.Resources.click;
                    sounds.Play();
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
                    sounds.Stream = Properties.Resources.correct;
                    sounds.Play();
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
                        sounds.Stream = Properties.Resources.error;
                        sounds.Play(); 
                        return;
                    }
                }
            }
            timer2.Stop();
            MessageBox.Show("You Won the match game!" + "\nCongratulations!\n" + myLabel);
            Close();
        }

        private int i = 0;

        private string myLabel;

        private void timer2_Tick(object sender, EventArgs e)
        {
            i++;
            myLabel = "Match Duration:" + i.ToString() + "seconds";
        }
    } 
}



