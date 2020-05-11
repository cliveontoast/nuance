import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-find-matches',
  templateUrl: './find-matches.component.html'
})
export class FindMatchesComponent {
  public forecasts: MatchesDto;
  public text: string;
  public subText: string;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.text = "subtext";
    this.subText = "Text";
  }

  public search() {
    this.http.get<MatchesDto>(this.baseUrl + 'api/SubtextMatch',
      {
        params: {
          text: this.text,
          subText: this.subText
        }
      })
      //http.get<MatchesDto[]>('https://localhost:44398/api/SubtextMatch?text=hello%20hello&subText=e',)
      .subscribe(result => {
        this.forecasts = result;
        result.characterPositions.length > 0
      }, error => this.forecasts = null);
  }
}

export interface MatchesDto {
  characterPositions?: number[];
  error?: MatchesErrorType;
}

export enum MatchesErrorType {
  SubtextNullOrEmpty
}
