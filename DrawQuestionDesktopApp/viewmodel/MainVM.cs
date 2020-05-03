using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DrawQuestionDesktopApp.common;
using DrawQuestionDesktopApp.model;
using DrawQuestionDesktopApp.viewmodel.handler;

namespace DrawQuestionDesktopApp.viewmodel
{
    public class MainVM:INotifyPropertyChanged
    {
        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);

        private ObservableCollection<QuestionView> _questions;
        private ObservableCollection<int> _takenQuestions;
        private int _selectedNo;
        private QuestionView _selectedQuestion;
        private readonly RelayCommand _shuffle;
        private readonly RelayCommand _start;
        private bool _isNotStartet;

        public MainVM()
        {
            _questions = new ObservableCollection<QuestionView>();
            _takenQuestions = new ObservableCollection<int>();
            _shuffle = new RelayCommand(ShuffleQuestions);
            _start = new RelayCommand(StartUpQuestions);
            _isNotStartet = true;
            _selectedNo = 0;
        }

        


        public RelayCommand Shuffle => _shuffle;
        public RelayCommand Start => _start;
        public ObservableCollection<QuestionView> Questions => _questions;
        public ObservableCollection<int> TakenQuestions => _takenQuestions;

        public bool IsStartet => IsStartetMethod();
        public bool IsNotStartet => IsNotStartetMethod();


        public int SelectedNo
        {
            get => _selectedNo;
            set
            {
                _selectedNo = value;
                OnPropertyChanged("SelectedNo");
            }
        }

        private bool IsStartetMethod()
        {
            return !_isNotStartet;
        }
        private bool IsNotStartetMethod()
        {
            return _isNotStartet;
        }

        private void StartUpQuestions()
        {
            _isNotStartet = false;
            OnPropertyChanged("IsStartet");
            OnPropertyChanged("IsNotStartet");
            GenerateQuestions(_selectedNo);
            ShuffleQuestions();
            _takenQuestions.Clear();
        }

        private void ShuffleQuestions()
        {
            List<QuestionView> tmpList = new List<QuestionView>();
            foreach (QuestionView qw in _questions)
            {
                if (!qw.IsUsed) tmpList.Add(qw);
                else _takenQuestions.Add(qw.Number);
            }

            _questions.Clear();

            int numbersOfQuestionLeft = tmpList.Count;
            for (int i = numbersOfQuestionLeft; i > 0; i--)
            {
                int nextIx = _rand.Next(i);
                _questions.Add(tmpList[nextIx]);
                tmpList.RemoveAt(nextIx);
            }

            if (_questions.Count == 0)
            {
                _isNotStartet = true;
                OnPropertyChanged("IsStartet");
                OnPropertyChanged("IsNotStartet");
            }
        }

        private void GenerateQuestions(int selectedNo)
        {
            _questions.Clear();

            for (int i = 1; i <= selectedNo; i++)
            {
                _questions.Add(new QuestionView(new Question(i)));    
            }
            OnPropertyChanged("Questions");
        }

        public QuestionView SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                if (value != null)
                {
                    _selectedQuestion = value;
                    _selectedQuestion.Turn();
                    OnPropertyChanged("SelectedQuestion");
                }
            }
        }

        


        public event PropertyChangedEventHandler PropertyChanged;

        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
