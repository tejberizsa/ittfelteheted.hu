import { Topic } from './topic';
import { Answer } from './answer';

export interface Post {
    id: number;
    title: string;
    postBody: string;
    userId: number;
    topic: Topic[];
    dateAdded: Date;
    views: number;
    userName?: string;
    photoUrl?: string;
    answers?: Answer[];
}
