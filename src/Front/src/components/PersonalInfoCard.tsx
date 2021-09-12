import { Card, message, Spin } from "antd";
import React, { useEffect } from "react";
import { sessionClient, userClient } from "../api/httpClient";
import useApi from "../api/useApi";

type PersonalInfoCardProps = {
    chatId: string;
};

const cardStyle = {
    width: "22rem",
    margin: "1rem 1rem 2rem 1rem",
};

export const PersonalInfoCard: React.FC<PersonalInfoCardProps> = ({ chatId }) => {
    const {
        loading,
        data: info,
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

    if (loading) {
        return <Spin />;
    }

    const { username, name, age } = info;

    return (
        <Card title="Personal info" style={cardStyle}>
            <p>
                <b>Username: </b>
                <span>{username}</span>
            </p>
            <p>
                <b>Name: </b>
                <span>{name}</span>
            </p>
            <p>
                <b>Age: </b>
                <span>{age}</span>
            </p>
        </Card>
    );
};
