import { Component, OnInit, OnDestroy } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { EinsatzInput, Taetigkeit, EinsatzCreateResult } from 'src/app/models/graphql-models';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';
import { Subscription } from 'rxjs';

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
    { "key": Taetigkeit.GASSI_GEHEN, "label": "Gassi gehen"},
    { "key": Taetigkeit.BESORGUNG, "label": "Besorgung"},
    { "key": Taetigkeit.ANDERE, "label": "Andere"},
    { "key": Taetigkeit.TELEFON_KONTAKT, "label": "Telefon kontakt"}
  ];
  vermitteltAm: Date;
  state: State;
  paramSubscription: Subscription;
  authServiceSubscription: Subscription;
  helferId: any;
  userinfo: { name: string; };

  constructor(private graphqlService: GraphqlService,  private route: ActivatedRoute, private authService: AuthService, private location: Location ) {}
  ngOnDestroy(): void {
    this.paramSubscription.unsubscribe();
  }

  ngOnInit(): void {
    
    this.authServiceSubscription = this.authService.getUserInfo().subscribe(resp => {
      this.userinfo = { ...resp.body };
      this.einsatz.vermitteltDurch = this.userinfo.name;
    });
    this.paramSubscription = this.route.params.subscribe(params => {
      this.helferId = params['helferId'];
    });
    this.einsatz = new EinsatzInputModel("n/a");
    this.createResult = null;
    this.vermitteltAm = new Date();
    this.state = State.EDIT;
  }

  async addEinsatz() {
    this.state = State.SUCCESS;
    this.createResult = await this.graphqlService.addEinsatz(this.helferId, this.einsatz);
    if (this.createResult.isSuccess) {
      this.state = State.SUCCESS;
      this.back();
    }
    else {
      this.state = State.ERROR;
    }
  }
  back() : void {
    this.location.back();
  }
  createError(): void {
    this.state = State.ERROR;
    this.createResult = {
      "hilfesuchender": null,
      "taetigkeit": null,
      "errors": [{"message": "Das ist eine Testfehlermeldung."}],
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
  }
  hilfesuchender: string;
  taetigkeit: Taetigkeit;
  anmerkungen: string;
  vermitteltDurch: string;
  helferAusgelastet: boolean;  
  stunden: number;
}
