import { Component, OnInit } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { HelferCreateInput, Taetigkeit, Kontakt, HelferCreateResult } from 'src/app/models/graphql-models';

@Component({
  selector: 'app-createHelfer',
  templateUrl: './createHelfer.component.html',
  styleUrls: ['./createHelfer.component.scss']
})
export class CreateHelferComponent implements OnInit {

  helfer: HelferCreateInput;
  createResults: HelferCreateResult[];

  constructor(private graphqlService: GraphqlService) {
    this.helfer = new HelferCreateModel();
    this.createResults = [];
  }

  ngOnInit(): void {

  }

  private _reset(): void {
    this.helfer = new HelferCreateModel();
  }

  async createHelfer() {
    let result = await this.graphqlService.createHelfer(this.helfer);
    this.createResults.push(result);
    if (result) {
      this._reset();
    }
  }
}
class HelferCreateModel implements HelferCreateInput {
  constructor() {
    this.kontakt = new KontaktCreateModel();
    this.taetigkeiten = [];
    this.anmerkung = "";
    this.hatAuto = false;
    this.istRisikogruppe = false;
  }
  istRisikogruppe: boolean;
  hatAuto: boolean;
  anmerkung: string;
  taetigkeiten: Taetigkeit[];
  kontakt: Kontakt;
}
class KontaktCreateModel implements Kontakt {
  vorname: string;
  nachname: string;
  plz: number;
  strasse: string;
  telefon: string;
  email: string;
}
