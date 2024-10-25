using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;
using RTSPFrameSender.FrameProcessing;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.Build();

string hubUrl = configuration["SignalR:HubUrl"]!;

await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);

// Set the FFmpeg executables path to the root of the project
FFmpeg.SetExecutablesPath(Directory.GetCurrentDirectory());

var frameSender = new FrameSender(hubUrl);
await frameSender.StartAsync();

// Replace with your RTSP stream URL like this: rtsp://your_rtsp_stream_url. 
// But, for testing purposes, place a video file in the root `Videos` folder and name it `Video1.mp4`.
string rtspUrl = Path.Combine(Directory.GetCurrentDirectory(), "Videos", "Video1.mp4");
var frameCapture = new FrameCapture();

await frameCapture.CaptureFrames(rtspUrl, async frame =>
{
	await frameSender.SendFrameAsync(frame);
	await Task.Delay(100); // Adjust delay as needed
});
