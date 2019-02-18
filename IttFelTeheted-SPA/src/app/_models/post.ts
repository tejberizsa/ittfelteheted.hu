import { Topic } from './topic';
import { Answer } from './answer';
import { Photo } from './photo';

export interface Post {
    id: number;
    title: string;
    dateAdded: Date;
    views: number;
    answerCount: number;
    userPhotoUrl?: string;
    postBody?: string;
    userId?: number;
    topic?: Topic[];
    userName?: string;
    photoUrl?: string;
    answers?: Answer[];
    photos?: Photo[];
}
