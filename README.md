# RTSPFrameSender and ImageStreamReceiver

## Description

The task is to develop two .NET applications that communicate with each other using SignalR.

### App 1: RTSP Listener

- This application listens to an RTSP stream.
- It captures frames from the stream and sends them as images via SignalR to the second application.

### App 2: Image Receiver

- This application receives the image streams from App 1 using SignalR.
- It saves these images to the local file system.

## Requirements

- Use SignalR for real-time communication between the two applications.
- Ensure both applications are built with .NET.

## Setup Instructions

1. Clone the repository.
2. Navigate to each project directory and restore dependencies using `dotnet restore`.

## Running the Applications

**Important:** Run the projects in the following order:

### 1. ImageStreamReceiver

1. Open the `ImageStreamReceiver` project.
2. Run the application using `dotnet run`.
3. Images will be saved in the `Images` directory in the project root. There are some frame images already created in the repository as samples.

### 2. RTSPFrameSender

1. Open the `RTSPFrameSender` project.
2. Place your video file in the root `Videos` folder and name it `Video1.mp4`. There is one video file in the repository for testing purposes.
3. Run the application using `dotnet run`.

## Notes

- Ensure both applications are running on the same network.
- Adjust the SignalR URL in `RTSPFrameSender` if necessary.

## Dependencies

The project uses the following NuGet packages:

- `Microsoft.AspNetCore.SignalR.Client`
- `SkiaSharp`
- `Xabe.FFmpeg`
- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.Configuration.Json`

## Building and Running

1. **Restore Dependencies**: Run `dotnet restore` to install all required packages.
2. **Build the Project**: Use `dotnet build` to compile the project.
3. **Run the Application**: Execute `dotnet run` to start the application.

## Usage

- The application captures frames from a specified RTSP stream or video file.
- Progress is displayed in the console during frame capture.
- Ensure the SignalR server is running and accessible at the configured URL.

## Troubleshooting

- **Configuration Issues**: Ensure `appsettings.json` is correctly formatted and located in the project root.
- **Connection Errors**: Verify that the SignalR server URL and CORS settings are correct.

