using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DrawQuestionDesktopApp.model
{
    public class Question
    {
        private Image _questionFront;
        private Image _questionBack;
        private int _number;

        public Image QuestionFront => _questionFront;

        public Image QuestionBack => _questionBack;

        public int Number => _number;

        public Question(int no)
        {
            _number = no;

            _questionBack = new Image();
            _questionBack.Source = new BitmapImage(new Uri("Resources/questions/Back.png", UriKind.Relative));
            //_questionBack.Source = new BitmapImage(new Uri("pack://application:,,,/DrawQuestionDesktopApp;component/Resources/questions/Back.png"));

            _questionFront = new Image();
            String filename = (no < 10) ? "0" + no : ""+no;
            _questionFront.Source = new BitmapImage(new Uri($"/Resources/questions/{filename}.png", UriKind.Relative));
        }

    }
}
