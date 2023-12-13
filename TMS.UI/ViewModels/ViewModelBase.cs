using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TMS.UI.ViewModels
{
    /*
     * This class ViewModelBase implements the INotifyPropertyChanged interface
     * Which is a key component in the WPF's data binding mechanism. This communicats property
     * Changes to the View UI enabling it to automatically update when data changes.
     * */
    internal class ViewModelBase : INotifyPropertyChanged
    {
        //Event is created , the propertyChangedHandler is a delegate for property change notifications.
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
