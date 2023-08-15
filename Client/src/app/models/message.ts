import User from "./user";

export default class Message {
    id: number = -1;
    content: string = "";
    userId: number = -1;
    receiverId: number = -1;
    user: User | null = null; // Set default value to null or an empty User object
}