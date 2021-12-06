import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { UserContext } from "./App";

interface IMessage
{
    id: string;
    user: string;
    content: string;
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5299/chatHub")
    .build();

function All()
{
    const [messages, setMessages] = useState([] as IMessage[]);
    const [message, setMessage] = useState("");

    const username = React.useContext(UserContext);

    useEffect(() =>
    {
        connection.start().then(async () =>
        {
            const allMessages = await connection.invoke("GetAllMessages");
            setMessages(allMessages);
        });
    }, []);

    connection.on("ReceiveMessage", (message: IMessage) =>
    {
        setMessages([...messages, message]);
    });

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) =>
    {
        e.preventDefault();

        connection.invoke("SendMessage", username, message);

        setMessage("");
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    {
        setMessage(e.currentTarget.value);
    }

    return (
        <>
            <ul className="messages">
                {messages.map(m => (
                    username === m.user
                        ? <li className="message-own" key={m.id}>
                            <div className="content">{m.content}</div>
                        </li>
                        : <li key={m.id}>
                            <div className="user">{m.user[0]}</div>
                            <div className="content">{m.content}</div>
                        </li>
                ))}
            </ul>
            <div className="actions">
                <form onSubmit={handleSubmit}>
                    <input value={message} onChange={handleChange} />
                    <button>Senden</button>
                </form>
            </div>
        </>
    )
}

export default All;
