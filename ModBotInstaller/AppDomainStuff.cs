using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AppDomainDelegateHolder;

public static class NewAppDomain
{
    public static void Execute(Action action)
    {
        AppDomain domain = null;

        try
        {
            domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid());

            var domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(
                typeof(AppDomainDelegate).Assembly.FullName,
                typeof(AppDomainDelegate).FullName);

            domainDelegate.Execute(action);
        }
        finally
        {
            if (domain != null)
                AppDomain.Unload(domain);
        }
    }

    public static void Execute<T>(T parameter, Action<T> action)
    {
        AppDomain domain = null;

        try
        {
            domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid());

            var domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(
                typeof(AppDomainDelegate).Assembly.FullName,
                typeof(AppDomainDelegate).FullName);

            domainDelegate.Execute(parameter, action);
        }
        finally
        {
            if (domain != null)
                AppDomain.Unload(domain);
        }
    }
    public static T Execute<T>(Func<T> action)
    {
        AppDomain domain = null;

        try
        {
            domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid());

            var domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(
                typeof(AppDomainDelegate).Assembly.FullName,
                typeof(AppDomainDelegate).FullName);

            return domainDelegate.Execute(action);
        }
        finally
        {
            if (domain != null)
                AppDomain.Unload(domain);
        }
    }

    public static TResult Execute<T, TResult>(T parameter, Func<T, TResult> action)
    {
        AppDomain domain = null;

        try
        {
            domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid());

            var domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(
                typeof(AppDomainDelegate).Assembly.FullName,
                typeof(AppDomainDelegate).FullName);

            return domainDelegate.Execute(parameter, action);
        }
        finally
        {
            if (domain != null)
                AppDomain.Unload(domain);
        }
    }
}