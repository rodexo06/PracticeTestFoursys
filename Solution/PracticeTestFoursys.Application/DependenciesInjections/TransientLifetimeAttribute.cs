namespace PracticeTestFoursys.Application.DependenciesInjections {
    /// <summary>
    /// Define o tempo de vida de injeção para uma implementação como Transient.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TransientLifetimeAttribute : Attribute {
    }
}
