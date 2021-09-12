import { HubConnection, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import React, { useCallback, useContext, useEffect, useState } from "react";
import { getApiEndpoint } from "../api/utils";

export type IChatContext = {
    connection: HubConnection;
};

export default function useChatContext() {
    const context = useContext<IChatContext>(ChatContext);
    if (context === null) {
        throw new Error("useChatContext must be used within an ChatContext.Provider");
    }
    return context;
}

export const ChatContext = React.createContext<IChatContext>(null as any);

export const useChat = (chatId: string) => {
    const [connection] = useState(
        new HubConnectionBuilder()
            .withUrl(`${getApiEndpoint()}/api/signal/chat`)
            .withAutomaticReconnect()
            .build(),
    );

    const connect = useCallback(async () => {
        try {
            await connection.start();
        } catch (err) {
            console.log(err);
        }
        if (connection.state === HubConnectionState.Connected) {
            console.log("Subscribe to chat", chatId);
            connection.invoke("SubscribeToChat", chatId);
        }
        return connection;
    }, [chatId, connection]);

    useEffect(() => {
        connect();
        return () => {
            console.log("Unsubscribe from chat", chatId);
            connection.invoke("UnsubscribeFromChat", chatId);
        };
    }, [chatId, connect, connection]);

    const context: IChatContext = {
        connection,
    };

    return context;
};
