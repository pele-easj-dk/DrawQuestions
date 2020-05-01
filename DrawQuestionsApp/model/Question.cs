using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DrawQuestionsApp.model
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
            _questionBack.Source = new BitmapImage(new Uri("ms-appx:///Assets/questions/Back.png"));

            _questionFront = new Image();
            String filename = (no < 10) ? "0" + no : ""+no;
            _questionFront.Source = new BitmapImage(new Uri($"ms-appx:///Assets/questions/{filename}.png"));
        }

    }
}
