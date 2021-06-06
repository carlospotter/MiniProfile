import { Links } from "./links";

export interface Profile {
    id: number;
    username: string;
    knownAs: string;
    photoUrl: string;
    userBio: string;
    links: Links[];
}