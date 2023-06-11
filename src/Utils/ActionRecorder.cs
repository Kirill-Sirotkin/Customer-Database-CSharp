namespace Utils;

class ActionRecorder
{
    private readonly Stack<IAction> _actions = new();
    private readonly Stack<IAction> _rewoundActions = new();

    public void Record(IAction action)
    {
        _actions.Push(action);
        action.Execute();
    }
    public void Rewind()
    {
        IAction action = _actions.Pop();
        action.Undo();
        _rewoundActions.Push(action);
    }
    public void Redo()
    {
        IAction action = _rewoundActions.Pop();
        action.Execute();
    }
}