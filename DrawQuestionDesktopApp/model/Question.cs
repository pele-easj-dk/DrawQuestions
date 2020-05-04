using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DrawQuestionDesktopApp.model
{
    public class Question
    {
        private Image _questionFront;
        private static Image _questionBack = new Image(){Source = new BitmapImage(new Uri("Resources/questions/Back.png", UriKind.Relative)) };
        private int _number;

        public Image QuestionFront => _questionFront;

        public Image QuestionBack => _questionBack;

        public int Number => _number;

        public Question(int no)
        {
            _number = no;

            _questionFront = new Image();
            String filename = (no < 10) ? "0" + no : ""+no;
            _questionFront.Source = new BitmapImage(new Uri($"Resources/questions/{filename}.png", UriKind.Relative));
        }

    }
}
