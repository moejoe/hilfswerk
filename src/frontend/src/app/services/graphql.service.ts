import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { HelferFilters, HelferListenEintrag } from '../models/graphql-models';

@Injectable({
  providedIn: 'root'
})
export class GraphqlService {

  constructor(private httpClient: HttpClient) { }

  queryHelferListe(filters: HelferFilters) {
    let query = `query HelferListe(
  $inPlz: [Int]
  $taetigkeitIn: [Taetigkeit]
  $istRisikoGruppe: Boolean
  $hatAuto: Boolean
) {
  helfer(inPlz: $inPlz, taetigkeitIn: $taetigkeitIn,istRisikoGruppe : $istRisikoGruppe, hatAuto: $hatAuto) {
    id
    kontakt {
      nachname,
      vorname,
      strasse,
      plz
    },
    totalEinsaetze,
    anmerkung
  }
}`;
    return this.httpClient.post<{ data: { helfer: HelferListenEintrag[] } }>(`${environment.apiUrl}/graphql`, {
      query: query,
      variables: filters,
    }).pipe(map(d => {
      let res: HelferListenEintrag[] = [];
      for(let i = 0; i < 100; i++) {
        for (let h of d.data.helfer) {
          res.push(h);
        }
      }
      return res;
    }));
  }
}
