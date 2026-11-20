using System.Windows;
using TodoListWpf.ViewModels;

namespace TodoListWpf;

/// <summary>
/// Ventana principal de la aplicación Todo List en WPF.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Tarea: Inicializa los componentes de la vista y vincula el DataContext con el ViewModel principal.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        var objViewModelPrincipal = new MainViewModel();
        DataContext = objViewModelPrincipal;
    }
}
