import { Component, OnInit, OnDestroy, ViewChild, ChangeDetectorRef } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { EinsatzInput, Taetigkeit, EinsatzCreateResult } from 'src/app/models/graphql-models';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';
import { Subscription } from 'rxjs';
import { NgForm } from "@angular/forms";

@Component({
  selector: 'app-addEinsatz',
  templateUrl: './addEinsatz.component.html',
  styleUrls: ['./addEinsatz.component.scss']
})
export class AddEinsatzComponent implements OnInit, OnDestroy {
  State = State;

  einsatz: EinsatzInput;
  createResult: EinsatzCreateResult;
  taetigkeiten = [
    { "key": Taetigkeit.GASSI_GEHEN, "label": "Gassi gehen" },
    { "key": Taetigkeit.BESORGUNG, "label": "Besorgung" },
    { "key": Taetigkeit.ANDERE, "label": "Andere" },
    { "key": Taetigkeit.TELEFON_KONTAKT, "label": "Telefon kontakt" }
  ];
  vermitteltAm: Date;
  state: State;
  paramSubscription: Subscription;
  authServiceSubscription: Subscription;
  helferId: any;
  userinfo: { name: string; };

  @ViewChild("addEinsatzForm") addEinsatzForm: NgForm;

  constructor(private graphqlService: GraphqlService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private location: Location,
    private changeDetectorRef: ChangeDetectorRef) { }
  ngOnDestroy(): void {
    this.paramSubscription.unsubscribe();
  }

  resetEinsatzForm(vorherigerEinsatz: EinsatzInputModel) {
    let neuerEinsatz = new EinsatzInputModel(this.userinfo.name);
    if (vorherigerEinsatz) {
      neuerEinsatz.hilfesuchender = vorherigerEinsatz.hilfesuchender;
      neuerEinsatz.helferAusgelastet = vorherigerEinsatz.helferAusgelastet;
    }
    this.addEinsatzForm.resetForm();
    this.changeDetectorRef.detectChanges();
    this.einsatz = neuerEinsatz;
  }

  ngOnInit(): void {
    this.einsatz = new EinsatzInputModel("n/a");
    this.authServiceSubscription = this.authService.getUserInfo().subscribe(resp => {
      this.userinfo = { ...resp.body };
      this.resetEinsatzForm(null);
    });
    this.paramSubscription = this.route.params.subscribe(params => {
      this.helferId = params['helferId'];
    });
    this.createResult = null;
    this.vermitteltAm = new Date();
    this.state = State.EDIT;
  }

  async addEinsatz() {
    this.state = State.SAVING;
    this.createResult = await this.graphqlService.addEinsatz(this.helferId, this.einsatz);
    if (this.createResult.isSuccess) {
      this.state = State.SUCCESS;
      this.resetEinsatzForm(this.einsatz);
    }
    else {
      this.state = State.ERROR;
    }
  }
  back(): void {
    this.location.back();
  }
  createError(): void {
    this.state = State.ERROR;
    this.createResult = {
      "hilfesuchender": null,
      "taetigkeit": null,
      "errors": [{ "message": "Das ist eine Testfehlermeldung." }],
      "isSuccess": false
    };
  }
}
enum State {
  EDIT,
  SAVING,
  SUCCESS,
  ERROR
}
class EinsatzInputModel implements EinsatzInput {
  constructor(vermittler: string) {
    this.vermitteltDurch = vermittler;
    this.anmerkungen = "";
    this.helferAusgelastet = false;
    this.stunden = null;
    this.vermitteltAm = new Date();
  }
  vermitteltAm: Date;
  hilfesuchender: string;
  taetigkeit: Taetigkeit;
  anmerkungen: string;
  vermitteltDurch: string;
  helferAusgelastet: boolean;
  stunden: number;
}
