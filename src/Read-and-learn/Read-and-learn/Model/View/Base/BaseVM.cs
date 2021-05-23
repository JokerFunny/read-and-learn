using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Read_and_learn.Model.View
{
    /// <summary>
    /// Base class for any view model.
    /// </summary>
    public abstract class BaseVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Event to handle property change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Action when property was changed.
        /// </summary>
        /// <param name="name">Allows you to obtain the method or property name of the caller to the method</param>
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
