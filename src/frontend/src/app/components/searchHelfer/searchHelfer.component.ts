import { Component, OnInit } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Observable } from 'rxjs';
import { HelferListenEintrag } from 'src/app/models/graphql-models';

@Component({
  selector: 'app-index',
  templateUrl: './searchHelfer.component.html',
  styleUrls: ['./searchHelfer.component.scss'],
})
export class SearchHelferComponent implements OnInit {
  helfer$: Observable<HelferListenEintrag[]>;
  displayedColumns: string[] = ['name', 'einsaetze', 'strasse', 'plz', 'anmerkung'];
  selectedHelfer: HelferListenEintrag | null;
  searchTerms: string;

  constructor(private graphqlService: GraphqlService) {

  }

  async ngOnInit() {
    this.helfer$ = this.graphqlService.queryHelferListe({});
  }


  filterChange() {
    if(!this.searchTerms || this.searchTerms == '') {
      this.helfer$ = this.graphqlService.queryHelferListe({});
    }
    else {
      this.helfer$ = this.graphqlService.queryHelferByName(this.searchTerms);
    }
  }
}
