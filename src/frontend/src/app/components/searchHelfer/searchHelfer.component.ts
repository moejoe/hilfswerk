import { Component, OnInit } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Observable, Subject } from 'rxjs';
import { HelferListenEintrag } from 'src/app/models/graphql-models';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-index',
  templateUrl: './searchHelfer.component.html',
  styleUrls: ['./searchHelfer.component.scss'],
})
export class SearchHelferComponent implements OnInit {
  helfer$: Observable<HelferListenEintrag[]>;
  displayedColumns: string[] = ['name', 'einsaetze', 'strasse', 'plz'];
  selectedHelfer: HelferListenEintrag | null;
  searchTerms: string;
  filterChange = new Subject<string>();

  constructor(private graphqlService: GraphqlService) {
    this.filterChange.pipe(
      debounceTime(400),
      distinctUntilChanged())
      .subscribe(value => {
        this.searchTerms = value;
        this.updateList(value);
      });
  }

  private updateList(value: string)  {
    if (!value || value == '') {
      this.helfer$ = this.graphqlService.queryHelferListe({});
    }
    else {
      this.helfer$ = this.graphqlService.queryHelferByName(value);
    }
  }

  async ngOnInit() {
    this.helfer$ = this.graphqlService.queryHelferListe({});
  }

  async entlasten(helferId: string) {
    await this.graphqlService.setAusgelastet(helferId, false);
    this.updateList(this.searchTerms);
  }


}
