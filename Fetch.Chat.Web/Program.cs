using Fetch.Chat.Domain;
using Fetch.Chat.Infrastructure;
using Fetchchat;
using Grpc.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IChatService chatService = new ChatService();
builder.Services.AddSingleton<IChatService>(chatService);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

var port = 30051;
var server = new Server
{
    Services = { ChatRoom.BindService(new ChatRoomService(chatService)) },
    Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
};