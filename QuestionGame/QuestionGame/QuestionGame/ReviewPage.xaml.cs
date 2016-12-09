using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace QuestionGame
{
    public partial class ReviewPage : ContentPage
    {
        public ReviewPage(Game game)
        {
            InitializeComponent();

            lblResult.Text = String.Format("Great! You got {0} out of {1} questions correct", game.GetNumberOfCorrectResponses(), game.NumberOfQuestions);

        }
    }
}
