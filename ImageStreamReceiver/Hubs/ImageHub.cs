using Microsoft.AspNetCore.SignalR;

namespace ImageStreamReceiver.Hubs;

public class ImageHub : Hub
{
	public async Task SendImage(byte[] imageBytes)
	{
		try
		{
				// Navigate to the project root directory
			string projectRoot = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Images");
			string imagesPath = Path.GetFullPath(projectRoot);

			// Ensure the Images directory exists
			if (!Directory.Exists(imagesPath))
			{
				Directory.CreateDirectory(imagesPath);
			}
			else
			{
				// Remove all existing images
				foreach (var file in Directory.GetFiles(imagesPath, "*.jpg"))
				{
					File.Delete(file);
				}
			}

			// Create a unique file name for the image
			string filePath = Path.Combine(imagesPath, $"{Guid.NewGuid()}.jpg");

			// Save the image
			await File.WriteAllBytesAsync(filePath, imageBytes);
			Console.WriteLine($"Image saved to {filePath}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error saving image: {ex.Message}");
			throw;
		}
	}
}
