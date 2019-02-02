export interface Answer {
    id: number;
    answerBody: string;
    dateAdded: Date;
    like: number;
    userId: number;
    userName?: string;
    photoUrl?: string;
}
