export interface MatchesDto {
  characterPositions?: number[];
  error?: MatchesErrorType;
}

export enum MatchesErrorType {
  SubtextNullOrEmpty,
  ServerError
}
