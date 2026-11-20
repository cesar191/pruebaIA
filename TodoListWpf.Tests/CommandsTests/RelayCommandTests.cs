using TodoListWpf.Commands;
using Xunit;

namespace TodoListWpf.Tests.CommandsTests;

public class RelayCommandTests
{
    /// <summary>
    /// Tarea: Valida que RelayCommand ejecute la acción asignada al llamarse Execute.
    /// </summary>
    [Fact]
    public void RelayCommand_Execute_EjecutaAccionAsociada()
    {
        // Arrange
        bool boolAccionEjecutada = false;
        var cmd = new RelayCommand(_ => boolAccionEjecutada = true);

        // Act
        cmd.Execute(null);

        // Assert
        Assert.True(boolAccionEjecutada);
    }

    /// <summary>
    /// Tarea: Valida que RelayCommand evalúe correctamente el predicado de CanExecute.
    /// </summary>
    [Fact]
    public void RelayCommand_CanExecute_EvaluaPredicadoCorrectamente()
    {
        // Arrange
        bool boolPermitir = false;
        var cmd = new RelayCommand(_ => { }, _ => boolPermitir);

        // Act & Assert
        Assert.False(cmd.CanExecute(null));

        boolPermitir = true;
        Assert.True(cmd.CanExecute(null));
    }
}
