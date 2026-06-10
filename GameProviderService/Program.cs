using Models;
using DTOs;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISessionService, SessionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () =>
{
    return Results.Ok();
});


app.MapPost("/Funds/CreatePlayer", (RequestDTO request, ISessionService sessionService) =>
{
    
    var player = new Player
    {
        ExternalId = request.ExternalId,
    };

    sessionService.AddPlayer(player);

    return Results.Ok(player);
})
.WithOpenApi();

app.MapGet("/Funds/GetBalance", (RequestDTO request, ISessionService sessionService) =>
{
    var balance = sessionService.GetBalance(request.ExternalId);

    if (balance == null)
    {
        return Results.NotFound(new ErrorResponse
        {
            ErrorCode = 404,
            ErrorMessage = "No player found."
        });
    }

    return Results.Ok(new { Balance = balance });
})
.WithOpenApi();

//app.MapPut("/Funds/Transfer", (TransferRequestDto request, ISessionService sessionService) =>
//{
//    var result = sessionService.Transfer(request);

//    if (!result.Success)
//    {
//        return Results.BadRequest(new ErrorResponse
//        {
//            ErrorCode = result.ErrorCode,
//            ErrorMessage = result.Error ?? "Unknown error"
//        });
//    }

//    return Results.Ok(new
//    {
//        Success = true,
//        ExternalId = result.Player!.ExternalId,
//        Balance = result.Player.Balance,
//        TransactionId = request.ExtTransactionId
//    });
//})
//.WithOpenApi();

app.Run();
