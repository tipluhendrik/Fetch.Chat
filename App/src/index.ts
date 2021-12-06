import * as signalR from "@microsoft/signalr";

import "./index.scss";

const enterUsername = () =>
{
    const username = prompt("Bitte Anzeigename eingeben.");

    if (username === null || username.length === 0)
    {
        enterUsername();
    }
    else
    {
        enterChat(username);
    }
}

const enterChat = (username: string) =>
{
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5299/chatHub")
        .build();

    connection.start().then(allMessages => console.log(allMessages))
}

enterUsername();