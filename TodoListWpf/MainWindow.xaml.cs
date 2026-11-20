using System.Windows;
using TodoListWpf.ViewModels;

namespace TodoListWpf;

/// <summary>
/// Ventana principal de la aplicación Todo List en WPF.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var objViewModelPrincipal = new MainViewModel();
        DataContext = objViewModelPrincipal;
    }
}
