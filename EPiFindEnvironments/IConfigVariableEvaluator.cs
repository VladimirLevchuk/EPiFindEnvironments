namespace EPiFindEnvironments
{
    public interface IConfigVariableEvaluator
    {
        bool IsVariable(string name);
        string Evaluate(string name);
    }
}