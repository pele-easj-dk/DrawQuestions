using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DrawQuestionsApp.Annotations;
using DrawQuestionsApp.common;
using DrawQuestionsApp.model;
using DrawQuestionsApp.viewmodel.handler;

namespace DrawQuestionsApp.viewmodel
{
    public class MainVM:INotifyPropertyChanged
    {
        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);

        private ObservableCollection<QuestionView> _questions;
        private ObservableCollection<int> _takenQuestions;
        private int _selectedNo;
        private QuestionView _selectedQuestion;
        private RelayCommand _shuffle;
        private RelayCommand _start;
        private bool _notStartet;




        public MainVM()
        {
            _questions = new ObservableCollection<QuestionView>();
            _takenQuestions = new ObservableCollection<int>();
            _shuffle = new RelayCommand(ShuffleQuestions, IsStartet);
            _start = new RelayCommand(StartUpQuestions, IsNotStartet);
            _notStartet = true;
        }

        


        public RelayCommand Shuffle => _shuffle;
        public RelayCommand Start => _start;
        public ObservableCollection<QuestionView> Questions => _questions;
        public ObservableCollection<int> TakenQuestions => _takenQuestions;


        public int SelectedNo
        {
            get => _selectedNo;
            set
            {
                _selectedNo = value;
                OnPropertyChanged();
            }
        }

        private bool IsStartet()
        {
            return !_notStartet;
        }
        private bool IsNotStartet()
        {
            return _notStartet;
        }

        private void StartUpQuestions()
        {
            GenerateQuestions(_selectedNo);
            ShuffleQuestions();
            _notStartet = false;
            _takenQuestions.Clear();
            OnPropertyChanged("Shuffle");
            OnPropertyChanged("Start");
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
                _notStartet = true;
                OnPropertyChanged("Shuffle");
                OnPropertyChanged("Start");
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
                    OnPropertyChanged();
                }
            }
        }

        


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
