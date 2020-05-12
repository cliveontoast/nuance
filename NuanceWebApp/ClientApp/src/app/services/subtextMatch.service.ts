import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MatchesDto } from '../dto/matchesDto';

@Injectable()
export class SubtextMatchService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  matches(text: string, subText: string): Observable<MatchesDto> {
    return this.http.get<MatchesDto>(this.baseUrl + 'api/SubtextMatch',
      {
        params: {
          text: text,
          subText: subText
        }
      });
  }
}
