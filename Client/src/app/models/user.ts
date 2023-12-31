import Message from "./message";

export default class User {
    id: number = -1;
    firstName: string = "";
    lastName: string = "";
    userName: string = "";
    password: string = "";
    email: string = "";
    photoUrl: string = "";
    message: string = "";
    statusCode: string = "";
    user !: User;
    messages: Array<Message> = [];
}