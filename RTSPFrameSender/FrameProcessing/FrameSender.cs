namespace RTSPFrameSender.FrameProcessing;

using Microsoft.AspNetCore.SignalR.Client;
using SkiaSharp;
using System.IO;
using System.Threading.Tasks;

public class FrameSender
{
	private readonly HubConnection _connection;

	public FrameSender(string hubUrl)
	{
		_connection = new HubConnectionBuilder()
			.WithUrl(hubUrl, options =>
			{
				options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
				options.CloseTimeout = TimeSpan.FromMinutes(2);
			})
			.Build();

		_connection.Closed += async (error) =>
		{
			Console.WriteLine("Connection closed. Attempting to reconnect...");
			await Task.Delay(1000);
			await _connection.StartAsync();
		};
	}

	public async Task StartAsync()
	{
		await _connection.StartAsync();
	}

	public async Task SendFrameAsync(SKBitmap frame)
	{
        using var ms = new MemoryStream();
        frame.Encode(ms, SKEncodedImageFormat.Jpeg, 100);
        var imageBytes = ms.ToArray();
        try
        {
            await _connection.InvokeAsync("SendImage", imageBytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending image: {ex.Message}");
        }
    }
}

