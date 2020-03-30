import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { HelferFilters, HelferListenEintrag, HelferCreateInput, EinsatzInput, Kontakt, HelferCreateResult, EinsatzCreateResult, Taetigkeit } from '../models/graphql-models';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class GraphqlService {

  constructor(private httpClient: HttpClient, private authService: AuthService) { }


  private headers() {
    return {
      "Authorization": `Bearer ${this.authService.getToken()}`
    }
  }

  queryHelferListe(filters: HelferFilters) {
    let query = `query HelferListe(
  $inPlz: [Int]
  $taetigkeitIn: [Taetigkeit]
  $istRisikoGruppe: Boolean
  $hatAuto: Boolean
  $istZivildiener: Boolean
  $istFreiwilliger: Boolean
) {
  helfer(inPlz: $inPlz, taetigkeitIn: $taetigkeitIn, istRisikoGruppe : $istRisikoGruppe, hatAuto: $hatAuto, istZivildiener: $istZivildiener, istFreiwilliger: $istFreiwilliger) {
    id
    kontakt {
      email,
      telefon,
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
    }, { headers: this.headers() }).pipe(map(d => {
      return d.data.helfer;
    }));
  }

  async createHelfer(helfer: HelferCreateInput) {
    let mutation = `
    mutation createHelfer($helfer: HelferInput!) {
      createHelfer(helfer: $helfer) {
        id
        kontakt {
          vorname
          nachname
          plz
        }
      }
    }`;
    var request = {
      "query": mutation,
      variables: {
        "helfer": helfer
      }
    };
    try {

      var result = await this.httpClient.post<{ data: { createHelfer: { id: string, kontakt: Kontakt } }, errors: any[] }>(`${environment.apiUrl}/graphql`, request,
        { headers: this.headers() }).toPromise();
      if (result.errors) {
        return new ErrorCreateResult(result.errors);
      }
      return new SuccessCreateResult(result.data.createHelfer.id, result.data.createHelfer.kontakt);
    }
    catch {
      return new ErrorCreateResult([{ message: "Error sending graphQL mutation." }])
    }
  }
  async addEinsatz(helferId: string, einsatz: EinsatzInput) {
    let mutation = `
    mutation ($helferId: String!, $einsatz: EinsatzInput!) {
      createEinsatz(helferId: $helferId, einsatz: $einsatz) {
        hilfesuchender
        taetigkeit
      }
    }`;
    var request = {
      "query": mutation,
      variables: {
        "helferId": helferId,
        "einsatz": einsatz
      }
    };
    try {
      var result = await this.httpClient.post<{ data: { createEinsatz: { hilfesuchender: string, taetigkeit: Taetigkeit } }, errors: any[] }>(`${environment.apiUrl}/graphql`, request,
        { headers: this.headers() }).toPromise();
      if (result.errors) {
        return new EinsatzCreateErrorResult(result.errors);
      }
      return new AddEinsatzSuccessResult(result.data.createEinsatz.hilfesuchender, result.data.createEinsatz.taetigkeit);
    }
    catch {
      return new EinsatzCreateErrorResult([{ message: "Error sending graphQL mutation." }]);
    }
  }

}
class AddEinsatzSuccessResult implements EinsatzCreateResult{
  constructor(hilfesuchender: string, taetigkeit: Taetigkeit) {
    this.hilfesuchender = hilfesuchender;
    this.taetigkeit = taetigkeit;
    this.isSuccess = true;
  }
  hilfesuchender: string;
  taetigkeit: Taetigkeit;
  errors: { message: string; }[];
  isSuccess: boolean;
}
class EinsatzCreateErrorResult implements EinsatzCreateResult {
  constructor(errors: { message: string; }[]) {
    this.errors = errors;
    this.isSuccess = false;
  }
  hilfesuchender: string;
  taetigkeit: Taetigkeit;
  errors: { message: string; }[];
  isSuccess: boolean;
}


class SuccessCreateResult implements HelferCreateResult{
  constructor(id: string, kontakt: Kontakt) {
    this.id = id;
    this.errors = null;
    this.kontakt = kontakt;
    this.isSuccess = true;
  }
  id: string;
  kontakt: Kontakt;
  errors: { message: string; }[];
  isSuccess: boolean;
}
class ErrorCreateResult implements HelferCreateResult {
  constructor(errors: { message: string; }[]) {
    this.id = null;
    this.errors = errors;
    this.isSuccess = false;
  };
  id: string;
  kontakt: Kontakt;
  errors: { message: string; }[];
  isSuccess: boolean;
}
