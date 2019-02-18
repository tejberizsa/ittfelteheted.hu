export interface Answer {
    id: number;
    answerBody: string;
    dateAdded: Date;
    like: number;
    userId: number;
    username?: string;
    photoUrl?: string;
}
