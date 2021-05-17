using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CKC
{
    public class CKCViewModel : INotifyPropertyChanged
    {
        private double _outputResult;
        private string _fromUnit;
        private string _toUnit;
        private double _input;

        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand _clickCommand;
        private ICommand _closeCommand;
        private ICommand _minimizeMaximizeUiCommand;
        private bool _canExecute;

        private IFormulae _formula;
        public double OutputResult
        {
            get
            {
                return _outputResult;
            }
            set
            {
                _outputResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OutputResult"));
            }
        }
        public string FromUnit
        {
            get
            {
                return _fromUnit;
            }
            set
            {
                _fromUnit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FromUnit"));
            }
        }

        public string ToUnit
        {
            get
            {
                return _toUnit;
            }
            set
            {
                _toUnit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ToUnit"));
            }
        }
        public double Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Input"));
            }
        }

        public ObservableCollection<string> UnitsNameCollection
        {
            get;
            set;
        }

        public CKCViewModel()
        {
            UnitsNameCollection = new ObservableCollection<string>();
            UnitsNameCollection.Add("mil");
            UnitsNameCollection.Add("microns");
            UnitsNameCollection.Add("mm");
            UnitsNameCollection.Add("cm");
            UnitsNameCollection.Add("inch");
            UnitsNameCollection.Add("oz");
            _canExecute = true;
        }

        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => Caluclate(), _canExecute));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new CommandHandler(() => CloseApplication(), _canExecute));
            }
        }

        public ICommand MinimizeMaximizeUiCommand
        {
            get
            {
                return _minimizeMaximizeUiCommand ?? (_minimizeMaximizeUiCommand = new CommandHandler(() => MinimizeMaximizeUi(), _canExecute));
            }
        }

        public void MinimizeMaximizeUi()
        {
            Application.Current.MainWindow.Activate();
        }

        public void CloseApplication()
        {
            Application.Current.MainWindow.Close();
        }

        public void Caluclate()
        {
            Console.WriteLine(FromUnit);
            Console.WriteLine(ToUnit);
            Console.WriteLine(Input);
            Console.WriteLine(OutputResult);

            ///////////////////////////////////////////////////////////////
            // 1. Call Formula based on FromUnit to convert to Inch pass Input and Collect intermidiate result as return
            // 2. Call Formula based on ToUnit pass intermidiate result and collect result in Output
            //
            //////////////////////////////////////////////////////////////

            double midResult = 0.0;

            switch (FromUnit)
            {
                case Util.Mil:
                    _formula = new MilToInch();
                    midResult = _formula.Calculate(Input);
                    break;

                case Util.Microns:
                    _formula = new MicronsToInch();
                    midResult = _formula.Calculate(Input);
                    break;

                case Util.MM:
                    _formula = new MMToInch();
                    midResult = _formula.Calculate(Input);
                    break;

                case Util.CM:
                    _formula = new CMToInch();
                    midResult = _formula.Calculate(Input);
                    break;

                case Util.Inch:
                    midResult = Input;
                    break;

                case Util.OZ:
                    _formula = new OZToInch();
                    midResult = _formula.Calculate(Input);
                    break;
            }

            switch (ToUnit)
            {
                case Util.Mil:
                    _formula = new InchToMil();
                    OutputResult = _formula.Calculate(midResult);
                    break;

                case Util.Microns:
                    _formula = new InchToMicrons();
                    OutputResult = _formula.Calculate(midResult);
                    break;

                case Util.MM:
                    _formula = new InchToMM();
                    OutputResult = _formula.Calculate(midResult);
                    break;

                case Util.CM:
                    _formula = new InchToCM();
                    OutputResult = _formula.Calculate(midResult);
                    break;

                case Util.Inch:
                    _formula = new InchToInch();
                    OutputResult = _formula.Calculate(midResult);
                    break;

                case Util.OZ:
                    _formula = new InchToOZ();
                    OutputResult = _formula.Calculate(midResult);
                    break;
            }

            Console.WriteLine(OutputResult);

        }

    }

    public class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _action;
        private bool _canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
