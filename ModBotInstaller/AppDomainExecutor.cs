using System;
using System.IO;

public static class AppDomainExecutor
{
    public static void Execute(Action action)
    {
        AppDomain domain = null;

        try
        {
            domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid());

            AppDomainDelegate domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(typeof(AppDomainDelegate).Assembly.FullName, typeof(AppDomainDelegate).FullName);
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

            AppDomainDelegate domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(typeof(AppDomainDelegate).Assembly.FullName, typeof(AppDomainDelegate).FullName);
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

            AppDomainDelegate domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(typeof(AppDomainDelegate).Assembly.FullName, typeof(AppDomainDelegate).FullName);
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

            AppDomainDelegate domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(typeof(AppDomainDelegate).Assembly.FullName, typeof(AppDomainDelegate).FullName);
            return domainDelegate.Execute(parameter, action);
        }
        finally
        {
            if (domain != null)
                AppDomain.Unload(domain);
        }
    }

    public static TResult Execute<T, TResult>(T parameter, Func<T, AppDomain, TResult> action, string probingPath = null)
    {
        AppDomain domain = null;

        try
        {
            if (probingPath == null)
            {
                domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid());
            }
            else
            {
                domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid(), null, new AppDomainSetup { ApplicationName = "Mod-Bot Launcher", DynamicBase = new DirectoryInfo(probingPath).Parent.Parent.FullName, PrivateBinPath = probingPath });
            }

            AppDomainDelegate domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(typeof(AppDomainDelegate).Assembly.FullName, typeof(AppDomainDelegate).FullName);
            return domainDelegate.Execute(parameter, domain, action);
        }
        finally
        {
            if (domain != null)
                AppDomain.Unload(domain);
        }
    }
}