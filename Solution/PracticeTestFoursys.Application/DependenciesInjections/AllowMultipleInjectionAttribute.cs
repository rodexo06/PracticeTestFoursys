namespace PracticeTestFoursys.Application.DependenciesInjections {
    /// <summary>
    /// Esse atributo sinaliza que a interface permite que sejam registradas múltiplas implementações para a injeção de dependência.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class AllowMultipleInjectionAttribute : Attribute {

    }
}
