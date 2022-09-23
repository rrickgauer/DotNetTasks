using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Wpf.Pages;

public interface IControlModel<T>
{
    public T ViewModel { get; set; }
}
