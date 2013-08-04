namespace FakeWin8
{
    public interface IFakeFunc<out TResult>
    {
        TResult Invoke();
    }
}