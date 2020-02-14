using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dino
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private int scoreP1;
        private int scoreP2;
        private int actualStep;


        private int totalCards = 16;
        private int qtd = 4;

        private List<Tuple<int, int>> cardsPlayer1;
        private List<Tuple<int, int>> cardsPlayer2;

        public MainPage()
        {
            InitializeComponent();
            InitializeCards();
        }

        private void InitializeCards()
        {
            lblP1Power.Text = String.Empty;
            lblP2Power.Text = String.Empty;
            actualStep = 0;
            scoreP1 = 0;
            scoreP2 = 0;

            #region Inicia aleatoriamente o poder das cartas
            var rnd = new Random();
            var cardsPowerP1 = Enumerable.Range(100, 1000).OrderBy(x => rnd.Next()).Take(totalCards).ToArray();
            var cardsPowerP2 = Enumerable.Range(100, 1000).OrderBy(x => rnd.Next()).Take(totalCards).ToArray();
            #endregion

            #region Seleciona aleatoriamente uma qtd de cartas para cada jogador
            var cardsP1 = Enumerable.Range(1, totalCards).OrderBy(x => rnd.Next()).Take(qtd).ToArray();
            var cardsP2 = Enumerable.Range(1, totalCards).OrderBy(x => rnd.Next()).Take(qtd).ToArray();
            #endregion

            cardsPlayer1 = new List<Tuple<int, int>>();
            cardsPlayer2 = new List<Tuple<int, int>>();

            for (int i=0; i < qtd; i++)
            {
                cardsPlayer1.Add(new Tuple<int,int>(cardsP1[i], cardsPowerP1[i]));
                cardsPlayer2.Add(new Tuple<int, int>(cardsP2[i], cardsPowerP2[i]));
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Tuple<int,int> cardP1;
            Tuple<int, int> cardP2;

            Button btn = (Button)sender;

            if(actualStep == qtd)
            {
                btn.Text = "De novo!!!";
                lblResult.Text = (scoreP1 > scoreP2) ? "Você venceu" : ((scoreP1 < scoreP2) ? "Você Perdeu" : "Empatou!!!");
                InitializeCards();
                return;
            }
            btn.Text = "Lutar";
            lblResult.Text = String.Empty;

            cardP1 = cardsPlayer1[actualStep];
            cardP2 = cardsPlayer2[actualStep];

            p1.Source = string.Format("d{0}.png", Convert.ToString(cardP1.Item1));
            p2.Source = string.Format("d{0}.png", Convert.ToString(cardP2.Item1));

            lblP1Power.Text = Convert.ToString(cardP1.Item2);
            lblP2Power.Text = Convert.ToString(cardP2.Item2);

            if(cardP1.Item2 != cardP2.Item2)
            {
                if(cardP1.Item2 > cardP2.Item2)
                {
                    //Player 1 Wins!!
                    scoreP1 += 1;
                }
                else
                {
                    scoreP2 += 1;
                }
            }else
            {
                //empate
            }

            lblScore1.Text = Convert.ToString(scoreP1);
            lblScore2.Text = Convert.ToString(scoreP2);

            actualStep += 1;
        }
    }
}
