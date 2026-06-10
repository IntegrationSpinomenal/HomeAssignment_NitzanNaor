using System.Security.Cryptography;
using System.Text;

public class Md5AuthMiddleware
{
	private readonly RequestDelegate _next;
	private const string PrivateKey = "otjc193!tNT";
	private const string Secret = "otjc193!tNT"; 

	public Md5AuthMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var receivedHash = context.Request.Headers["X-Auth-Hash"].FirstOrDefault();
		if (string.IsNullOrEmpty(receivedHash))
		{
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync("Missing MD5 hash.");
			return;
		}

		var dataToHash = Secret; 
		using var md5 = MD5.Create();
		var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
		var expectedHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

		if (receivedHash != expectedHash)
		{
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync("Invalid MD5 hash.");
			return;
		}

		await _next(context);
	}
}
