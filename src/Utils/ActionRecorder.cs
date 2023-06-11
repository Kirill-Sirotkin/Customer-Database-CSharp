namespace Utils;

class ActionRecorder
{
    private readonly Stack<IAction> _actions = new();

    public void Record(IAction action)
    {
        _actions.Push(action);
        action.Execute();
    }
    public void Rewind()
    {
        IAction action = _actions.Pop();
        action.Undo();
    }
}