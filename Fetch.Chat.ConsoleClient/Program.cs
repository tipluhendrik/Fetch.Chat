// See https://aka.ms/new-console-template for more information
using Fetchchat;
using Grpc.Core;

Console.WriteLine("Hello, World!");
var channel = new Channel("127.0.0.1:30051", ChannelCredentials.Insecure);

var client = new ChatRoom.ChatRoomClient(channel);

await client.SendAsync(new() { Message = "Hallo", User = "console" });
var listOfChatMessages = await client.GetAllMessagesAsync(new());

foreach (var message in listOfChatMessages.Messages)
{
    Console.WriteLine(message);
}