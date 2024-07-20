namespace PracticeTestFoursys.Application.DependenciesInjections {
    /// <summary>
    /// Define o tempo de vida de injeção para uma implementação como Scoped.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ScopedLifetimeAttribute : Attribute {
    }

}
