import User from "./user";

export default class Login {
    _user!: User;
    token: string = "";
    message: string = "";
    statusCode: string = "";
}