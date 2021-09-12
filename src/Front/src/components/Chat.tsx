import { SendOutlined } from "@ant-design/icons";
import { Button, Input, message, Spin } from "antd";
import dayjs from "dayjs";
import React, { CSSProperties, FormEvent, useEffect, useState } from "react";
import { useLocation, useRouteMatch } from "react-router";
import { sessionClient, userClient } from "../api/httpClient";
import useApi from "../api/useApi";
import { Bubble } from "./Bubble";
import useChatContext from "./ChatHubContext";

const mainContainerStyle: CSSProperties = {
    width: "100%",
    display: "flex",
    flexDirection: "column",
};

const titleStyle: CSSProperties = {
    margin: "0 2rem",
    paddingBottom: "1rem",
    borderBottom: "1px solid lightgray",
};

const chatContainerStyle: CSSProperties = {
    width: "100%",
    display: "flex",
    flex: "1 1 0%",
    flexDirection: "column-reverse",
    overflow: "auto",
    paddingLeft: "2.5rem",
    paddingRight: "2.5rem",
    paddingBottom: "1rem",
};

const formStyle: CSSProperties = {
    marginLeft: "auto",
    marginRight: "auto",
    width: "100%",
    display: "flex",
    padding: "2.5rem",
};

const inputGroupStyle: CSSProperties = {
    display: "flex",
};

const inputStyle: CSSProperties = {
    width: "100%",
    borderTopLeftRadius: "0.5rem",
    borderBottomLeftRadius: "0.5rem",
    padding: "0.5rem 0.75rem",
};

const sendButtonStyle: CSSProperties = {
    display: "flex",
    alignItems: "center",
    borderTopRightRadius: "0.5rem",
    borderBottomRightRadius: "0.5rem",
    padding: "1.2rem 1rem",
    marginLeft: -1,
};

const loadingStyle: CSSProperties = {
    display: "flex",
    justifyContent: "space-around",
};

export interface ChatProps {
    username: string;
    chatId: string;
}

export const Chat: React.FC<ChatProps> = ({ chatId, username }) => {
    const { loading, data, fetch, setData } = useApi({
        initial: [],
        fetchData: sessionClient.messages,
    });

    useEffect(() => {
        fetch(chatId).catch((e) => message.error(e.message));
    }, [chatId, fetch]);

    const { connection } = useChatContext();

    useEffect(() => {
        connection.on("NewMessage", (message) => {
            setData((prev) => [JSON.parse(message), ...prev]);
        });
        return () => connection.off("NewMessage");
    }, [connection, setData]);

    useEffect(() => {
        fetch(chatId).catch((e) => message.error(e.message));
    }, [chatId, fetch]);

    const [text, setText] = useState<string>("");
    const onSend = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            await sessionClient.send({ chatId, text });
            setText("");
        } catch (error) {
            message.error("Error sending message");
        }
    };

    const {
        data: userInfo,
        fetch: fetchInfo,
    } = useApi({
        initial: {},
        fetchData: userClient.info,
    });
    const { fetch: fetchUser } = useApi({
        initial: {},
        fetchData: sessionClient.user,
    });
    useEffect(() => {
        fetchUser(chatId)
            .then((user) => fetchInfo(user.id))
            .catch((e) => message.error(e.message));
    }, [chatId, fetchInfo, fetchUser]);

    const spinner = (
        <div key={"spin"} style={loadingStyle}>
            <div>
                <Spin />
                <span> Loading...</span>
            </div>
        </div>
    );

    return (
        <div style={mainContainerStyle}>
            <div style={titleStyle}>
                Продаваемый товар: <b>{userInfo.label ?? <Spin />}</b>
            </div>
            <div style={chatContainerStyle}>
                {loading
                    ? spinner
                    : data.map((item, index) => (
                          <Bubble
                              right={username === item.username}
                              username={item.username}
                              time={dayjs(item.createdAt).format("HH:mm")}
                              message={item.text}
                              key={item.id}
                          />
                      ))}
            </div>
            <form style={formStyle} onSubmit={onSend}>
                <Input.Group style={inputGroupStyle}>
                    <Input
                        required={true}
                        style={inputStyle}
                        value={text}
                        onChange={({ target: { value } }) => setText(value)}
                        placeholder="Message..."
                    />
                    <Button style={sendButtonStyle} htmlType="submit">
                        Send <SendOutlined />
                    </Button>
                </Input.Group>
            </form>
        </div>
    );
};
