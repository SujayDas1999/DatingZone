import { Photo } from './Photo.model';

export interface Member {
  id: number;
  username: string;
  photoUrl: string;
  emailAddress?: any;
  age: number;
  knownAs: string;
  profileCreated: Date;
  lastActive: Date;
  gender: string;
  introduction: string;
  lookingFor: string;
  interests: string;
  city: string;
  country: string;
  photos: Photo[];
}
