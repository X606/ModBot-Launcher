using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainDelegateHolder
{
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
    }
}
