using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using DrawQuestionDesktopApp.model;

namespace DrawQuestionDesktopApp.viewmodel.handler
{
    public class QuestionView:INotifyPropertyChanged
    {
        private Question _question;
        private Image _current;
        private bool _isUsed;


        public Image Current => _current;

        public bool IsUsed => _isUsed;

        public int Number => (_isUsed)? _question.Number : -1;

        public QuestionView(Question question)
        {
            _question = question;
            _current = question.QuestionBack;
        }

        public void Turn()
        {
            _current = _question.QuestionFront;
            _isUsed = true;

            OnPropertyChanged("Current");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
