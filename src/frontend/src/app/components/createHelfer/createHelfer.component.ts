import { Component, OnInit } from '@angular/core';
import { GraphqlService, HelferCreateResult } from 'src/app/services/graphql.service';
import { HelferCreateInput, Taetigkeit, Kontakt } from 'src/app/models/graphql-models';

@Component({
  selector: 'app-createHelfer',
  templateUrl: './createHelfer.component.html',
  styleUrls: ['./createHelfer.component.scss']
})
export class CreateHelferComponent implements OnInit {

  helfer: HelferCreateInput;
  taetigkeiten: any;
  createResult: HelferCreateResult;

  constructor(private graphqlService: GraphqlService) {
    this.helfer = new HelferCreateModel();
    this.taetigkeiten = {
    };
    this.createResult = null;
  }

  ngOnInit(): void {

  }

  private _reset(): void {
    this.helfer = new HelferCreateModel();
    this.taetigkeiten = {};
  }

  async createHelfer() {
    this.createResult = await this.graphqlService.createHelfer(this.helfer);
    if (this.createResult.isSuccess) {
      this._reset();
    }
  }

  public updateTaetigkeiten() {
    var taetigkeitenMap = this.taetigkeiten;
    this.helfer.taetigkeiten = [];
    Object.keys(taetigkeitenMap).map(function (key, index, taetigkeiten) {
      if (taetigkeitenMap[key] === true) {
        this.push(key);
      }
    }, this.helfer.taetigkeiten);
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
