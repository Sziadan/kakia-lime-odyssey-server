namespace kakia_lime_odyssey_server.Network;

public class BackgroundTask
{
	private Task? _timerTask;
	private readonly PeriodicTimer _timer;
	private readonly CancellationTokenSource _cts = new();

	public Func<Task>? Run;

	public BackgroundTask(TimeSpan interval)
	{
		_timer = new PeriodicTimer(interval);
	}

	public void Start()
	{
		_timerTask = Task.Run(DoWorkAsync);
	}

	private async Task DoWorkAsync()
	{
		try
		{
			while (await _timer.WaitForNextTickAsync(_cts.Token))
			{
				if (Run is not null)
				{
					try { await Run.Invoke(); }
					catch (Exception ex)
					{
						// Handle or log the exception as needed
						Console.WriteLine($"An error occurred: {ex.Message}");
					}
				}
			}
		}
		catch (OperationCanceledException)
		{
			// Handle cancellation
		}
		catch (Exception ex)
		{
			// Handle unexpected exceptions
			Console.WriteLine($"An unexpected error occurred: {ex.Message}");
		}
	}

	public async Task StopAsync()
	{
		if (_timerTask is null)
			return;

		_cts.Cancel();
		await _timerTask;
		_cts.Dispose();
	}
}
