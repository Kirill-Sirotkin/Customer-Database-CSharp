namespace Utils;

interface IAction
{
    void Execute();
    void Undo();
}