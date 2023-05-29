using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Go.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            //var handlers = PropertyChanged;
            //if (handlers is null) return;

            //var invocation_list = handlers.GetInvocationList();

            //var arg = new PropertyChangedEventArgs(PropertyName);
            //foreach (var action in invocation_list)
            //    if (action.Target is DispatcherObject disp_object)
            //        disp_object.Dispatcher.Invoke(action, this, arg);
            //    else
            //        action.DynamicInvoke(this, arg);
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = "")
        {
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
