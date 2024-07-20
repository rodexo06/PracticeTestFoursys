namespace PracticeTestFoursys.Application.DependenciesInjections {
    /// <summary>
    /// Sinaliza a interface que deve ser considerada na injeção de dependência de uma implementação.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class InjectableAttribute : Attribute {

    }
}
