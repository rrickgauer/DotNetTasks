using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tasks.Wpf.Services;

namespace Tasks.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class ContainerWindow : Window
    {
        private readonly WpfApplicationServices _applicationServices;

        public ContainerWindow(WpfApplicationServices applicationServices)
        {
            _applicationServices = applicationServices;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
