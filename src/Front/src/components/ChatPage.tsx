import React, { CSSProperties } from "react";
import { useParams } from "react-router";
import { Barometer } from "./Barometer";
import { Chat } from "./Chat";
import { ChatContext, useChat } from "./ChatHubContext";
import { PersonalInfoCard } from "./PersonalInfoCard";
import { SuggestionsCard } from "./SuggestionsCard";

const containerStyle: CSSProperties = {
    height: "calc(100vh - 90px)",
    width: "100%",
    maxWidth: "100%",
    display: "flex",
};

const secondColumnStyle: CSSProperties = {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
};

type ChatPageParams = {
    chatId: string;
};

export const ChatPage: React.FC = () => {
    const { chatId } = useParams<ChatPageParams>();

    const context = useChat(chatId);

    return (
        <ChatContext.Provider value={context}>
            <div style={containerStyle}>
                <Chat chatId={chatId} username="Admin" />
                <div style={secondColumnStyle}>
                    <PersonalInfoCard chatId={chatId} />
                    <Barometer chatId={chatId} />
                    <SuggestionsCard chatId={chatId} />
                </div>
            </div>
        </ChatContext.Provider>
    );
};
