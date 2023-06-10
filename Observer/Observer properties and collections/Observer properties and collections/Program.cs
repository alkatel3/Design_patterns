using JetBrains.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Observer_properties_and_collections
{
    public class Person
        :INotifyPropertyChanged,
        INotifyPropertyChanging
    {
        private int age;

        public int Age { 
            get => age; 
            set
            {
                if (value == age) return;
                OnPropertyChanging();
                age = value;
                OnPropertyChanged(); //Age
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        protected virtual void OnPropertyChanging(
    [CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //winforms
            BindingList<string> s =new();

            s.Add("abc");

            //wpf
            ObservableCollection<string> c;
        }
    }
}