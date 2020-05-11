export interface MatchesDto {
  characterPositions?: number[];
  errorType?: MatchesErrorType;
}

export enum MatchesErrorType {
  SubtextNullOrEmpty
}
