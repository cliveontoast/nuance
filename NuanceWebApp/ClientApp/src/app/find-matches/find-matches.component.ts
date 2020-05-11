import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatchesDto, MatchesErrorType } from '../../dto/matchesDto';

@Component({
  selector: 'app-find-matches',
  templateUrl: './find-matches.component.html'
})
export class FindMatchesComponent {
  public matches: MatchesDto = {};
  public text: string;
  public subText: string;
  private http: HttpClient;
  private baseUrl: string;
  public serverError: MatchesErrorType.ServerError;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.text = "";
    this.subText = "";
  }

  public search() {
    this.http.get<MatchesDto>(this.baseUrl + 'api/SubtextMatch',
      {
        params: {
          text: this.text,
          subText: this.subText
        }
      })
      .subscribe(result => {
        this.matches = result;
      }, () => {
          this.matches = null;
      });
  }
}
