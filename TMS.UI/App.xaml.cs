namespace TMS.UI;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        DispatcherUnhandledException += Application_DispatcherUnhandledException;
    }

    private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        LoggingService.Write(DateTime.Now.Clean(), e.Exception.ToString());
    }
}