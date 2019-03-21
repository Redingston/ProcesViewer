using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesViewer.Models
{
    public class MyProcess : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
               // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
               // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id"));
            }
        }
        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
               // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
            }
        }
        private long _memory;
        public long Memory
        {
            get { return _memory; }
            set
            {
                _memory = value;
               // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Memory"));
            }
        }
    }
}
