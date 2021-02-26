using System;

public class AppDomainDelegate : MarshalByRefObject
{
    public void Execute(Action action)
    {
        action();
    }

    public void Execute<T>(T parameter, Action<T> action)
    {
        action(parameter);
    }
    public T Execute<T>(Func<T> action)
    {
        return action();
    }

    public TResult Execute<T, TResult>(T parameter, Func<T, TResult> action)
    {
        return action(parameter);
    }

    public TResult Execute<T, TResult>(T parameter, AppDomain domain, Func<T, AppDomain, TResult> action)
    {
        return action(parameter, domain);
    }
}