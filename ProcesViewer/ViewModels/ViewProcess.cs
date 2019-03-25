using ProcesViewer.Command;
using ProcesViewer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProcesViewer.ViewModels
{
    public class ViewProcess : INotifyPropertyChanged
    {
        private MyProcess _process;
        public MyProcess MyProcess
        {
            get { return _process; }
            set
            {
                _process = value;
                NotifyPropertyChanged("MyProcess");
            }
        }

        private ObservableCollection<MyProcess> _processes;
        public ObservableCollection<MyProcess> Processes
        {
            get { return _processes; }
            set
            {
                _processes = value;
                NotifyPropertyChanged("Processes");
            }
        }

        public ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(SubmitExecute, CanSubmitExecute);
                }
                return _SubmitCommand;
            }
        }

        private void SubmitExecute(object parameter)
        {
            Process.Start(parameter.ToString());
        }
        private bool CanSubmitExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            else return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private MyProcess processTemp;
        public void AddProcess(MyProcess process)
        {
            Processes.Add(process);
        }

        public ViewProcess()
        {
            Processes = new ObservableCollection<MyProcess>();
            Process[] processes = Process.GetProcesses();

            foreach (var p in processes)
            {
                try
                {
                    processTemp = new MyProcess();
                    processTemp.Name = p.ProcessName;
                    processTemp.Id = p.Id;
                    processTemp.User = p.MachineName;
                    processTemp.Memory = p.PagedMemorySize64;
                    Processes.Add(processTemp);
                }
                catch (Exception)
                {
                }
               DispatcherTimer timer2 = new DispatcherTimer(
               new TimeSpan(0, 0, 5),
               DispatcherPriority.Normal,
               ReloadProcesses,
               App.Current.Dispatcher);
            }
        }
        void ReloadProcesses(object o, EventArgs e)
        {
            int? id = null;
            if (CurrentProcess != null)
                id = CurrentProcess.Id;
            Processes.Clear();

            Process[] processes = Process.GetProcesses();

            foreach (var p in processes)
            {
                try
                {
                    processTemp = new MyProcess();
                    processTemp.Name = p.ProcessName;
                    processTemp.Id = p.Id;
                    processTemp.User = p.MachineName;
                    processTemp.Memory = p.PagedMemorySize64;
                    Processes.Add(processTemp);
                }
                catch (Exception)
                {
                }
            }
        }
        MyProcess currentProcess;
        public MyProcess CurrentProcess
        {
            get
            {
                return currentProcess;
            }
            set
            {
                currentProcess = value;
                NotifyPropertyChanged(nameof(CurrentProcess));
            }
        }
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string newTask;
        public string NewTask
        {
            get { return newTask; }
            set
            {
                newTask = value;
                NotifyPropertyChanged(nameof(NewTask));
            }
        }      
        RelayCommand addProc;
        public RelayCommand AddProc
        {
            get
            {
                return addProc ?? (addProc = new RelayCommand((o) =>
                {
                    if (NewTask != null && NewTask.Length > 0)
                    {
                        try
                        {
                            Process.Start(NewTask);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        finally
                        {
                            NewTask = "";
                        }
                    }
                }));
            }
        }
    }
}
