import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Observable } from 'rxjs';
import { HelferListenEintrag } from 'src/app/models/graphql-models';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent implements OnInit {
  helfer$: Observable<HelferListenEintrag[]>;
  bezirke: any;

  constructor(private graphqlService: GraphqlService) { }

  ngOnInit(): void {
    this.helfer$ = this.graphqlService.queryHelferListe({});

    this.bezirke = [];
    for (let i = 1010; i <= 1230; i += 10) {
      this.bezirke.push({ name: `${i}`, checked: false, nummer: i });
    }
  }

  filterChange() {
    let inPlz = null;
    if (this.bezirke.filter(b => b.checked).length > 0) {
      inPlz = this.bezirke.filter(v => v.checked).map(v => v.nummer);
    }
    this.helfer$ = this.graphqlService.queryHelferListe({ inPlz: inPlz });
  }
}
