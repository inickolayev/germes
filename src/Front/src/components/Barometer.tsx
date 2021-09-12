import { message, Spin } from "antd";
import React, { CSSProperties, useEffect } from "react";
import ReactSpeedometer from "react-d3-speedometer";
import { BarometerValue } from "../api/client";
import { sessionClient } from "../api/httpClient";
import useApi from "../api/useApi";
import useChatContext from "./ChatHubContext";

const minValue = 0;
const maxValue = 1000;

type BarometerProps = {
    chatId: string;
};

export const Barometer: React.FC<BarometerProps> = ({ chatId }) => {
    const { loading, data, fetch, setData } = useApi({
        initial: {} as BarometerValue,
        fetchData: sessionClient.barometer,
    });

    useEffect(() => {
        fetch(chatId).catch((e) => message.error(e.message));
    }, [chatId, fetch]);

    const { connection } = useChatContext();

    useEffect(() => {
        connection.on("NewBarometer", (message) => {
            setData(JSON.parse(message));
        });
        return () => connection.off("NewBarometer");
    }, [connection, setData]);

    if (loading) {
        return <Spin />;
    }

    return (
        <div style={style}>
            <ReactSpeedometer value={data.value} maxValue={maxValue} minValue={minValue} />
        </div>
    );
};

const style: CSSProperties = {
    width: "40rem",
    height: "13rem",
    display: "flex",
    justifyContent: "center",
};
