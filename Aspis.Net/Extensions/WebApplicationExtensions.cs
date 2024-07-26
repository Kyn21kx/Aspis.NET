namespace AspisNet.Extensions;

public static class WebApplicationExtensions
{
	public delegate void ManualExceptionHandler(Exception ex, bool isFatal);

	private static ManualExceptionHandler? ActiveExceptionHandler = null;
	/*
	TODO: Import WebApplication
	public static WebApplication UseGlobalErrorBoundary(this WebApplication app, ManualExceptionHandler handler)
	{
		ActiveExceptionHandler = handler;
		AppDomain.CurrentDomain.UnhandledException += HandleCurrentDomainExceptions;
		return app;
	}
	*/

	private static void HandleCurrentDomainExceptions(object sender, UnhandledExceptionEventArgs e)
	{
		var exception = e.ExceptionObject as Exception;
		ActiveExceptionHandler?.Invoke(exception, e.IsTerminating);
	}
}

