import { Component } from '@angular/core';
import { MatchesDto } from '../dto/matchesDto';
import { SubtextMatchService } from '../services/subtextMatch.service';

@Component({
  selector: 'app-find-matches',
  templateUrl: './find-matches.component.html'
})
export class FindMatchesComponent {
  public matches: MatchesDto = {};
  public text: string = "";
  public subText: string = "";
  
  constructor(private subtextMatchService: SubtextMatchService) { }

  public search() {
    this.subtextMatchService.matches(this.text, this.subText)
      .subscribe(
        result => this.matches = result,
        () => this.matches = null
      );
  }
}
