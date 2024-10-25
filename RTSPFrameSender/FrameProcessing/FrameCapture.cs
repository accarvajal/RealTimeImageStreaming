namespace RTSPFrameSender.FrameProcessing;

using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Xabe.FFmpeg;

public class FrameCapture
{
	public async Task CaptureFrames(string rtspUrl, Func<SKBitmap, Task> processFrame)
	{
		string tempPath = Path.GetTempPath();
		string outputPath = Path.Combine(tempPath, "frame_%03d.jpg");

		// Clean up old JPEG files
		foreach (var file in Directory.GetFiles(tempPath, "frame_*.jpg"))
		{
			File.Delete(file);
		}

		var conversion = FFmpeg.Conversions.New()
			.AddParameter($"-i {rtspUrl}")
			.AddParameter("-vf fps=1") // Adjust fps as needed
			.SetOutput(outputPath);

		conversion.OnProgress += (sender, args) =>
		{
			Console.WriteLine($"Progress: {args.Percent}%");
		};

		conversion.OnDataReceived += (sender, args) =>
			{
				Console.WriteLine(args.Data);
			};

		await conversion.Start();

		// Wait for a reasonable time to allow all frames to be generated
		await Task.Delay(TimeSpan.FromSeconds(5)); // Adjust based on video length and fps

		int frameIndex = 1;
		while (true)
		{
			string framePath = Path.Combine(tempPath, $"frame_{frameIndex:D3}.jpg");
			if (!File.Exists(framePath))
			{
				break; // Exit loop if no more frames are found
			}

			using (var stream = File.OpenRead(framePath))
			{
				var frame = SKBitmap.Decode(stream);
				await processFrame(frame);
			}

			File.Delete(framePath);
			frameIndex++;
		}
	}
}
