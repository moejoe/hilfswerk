import { Component, OnInit, OnDestroy } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Taetigkeit, Kontakt, HelferEditInput } from 'src/app/models/graphql-models';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-editHelfer',
  templateUrl: './editHelfer.component.html',
  styleUrls: ['./editHelfer.component.scss']
})
export class EditHelferComponent implements OnInit, OnDestroy {
  helferId: string;
  helfer: HelferEditInput;
  blaSub: Subscription;

  constructor(private graphqlService: GraphqlService, private route: ActivatedRoute, private location: Location) {
  }
  ngOnDestroy(): void {
    this.blaSub.unsubscribe();
  }

  ngOnInit(): void {
    this.blaSub = this.route.params.subscribe(params => {
      this.helferId = params['helferId'];
      if (this.helferId) {
        this.graphqlService.getHelferEditModel(this.helferId).then(p => {
          this.helfer = p
        });
      }
    });
    this.helfer = new HelferEditModel();
  }

  async editHelfer() {
    let result = await this.graphqlService.editHelfer(this.helferId, this.helfer);
    if (result.isSuccess) {
      this.back();
    }
  }
  back() : void {
    this.location.back();
  }
}
class HelferEditModel implements HelferEditInput {
  constructor() {
    this.kontakt = new KontaktEditModel();
    this.taetigkeiten = [];
    this.anmerkung = "";
    this.hatAuto = false;
    this.istRisikogruppe = false;
    this.istZivildiener = false;
    this.istFreiwilliger = false;
    this.istDSGVOKonform = false;
  }
  istAusgelastet: boolean;
  istRisikogruppe: boolean;
  hatAuto: boolean;
  anmerkung: string;
  taetigkeiten: Taetigkeit[];
  kontakt: Kontakt;
  istZivildiener: boolean;
  istFreiwilliger: boolean;
  istDSGVOKonform: boolean;
}
class KontaktEditModel implements Kontakt {
  vorname: string;
  nachname: string;
  plz: number;
  strasse: string;
  telefon: string;
  email: string;
}
