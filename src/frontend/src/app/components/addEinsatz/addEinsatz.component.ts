import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { EinsatzInput, Taetigkeit, EinsatzCreateResult } from 'src/app/models/graphql-models';

@Component({
  selector: 'app-addEinsatz',
  templateUrl: './addEinsatz.component.html',
  styleUrls: ['./addEinsatz.component.scss']
})
export class AddEinsatzComponent implements OnInit {

  @Input() vermitteltDurch: string;

  @Input() helferId: string;

  @Output() added = new EventEmitter<string>();

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

  constructor(private graphqlService: GraphqlService) {}

  ngOnInit(): void {
    this.einsatz = new EinsatzInputModel(this.vermitteltDurch);
    this.createResult = null;
    this.vermitteltAm = new Date();
    this.state = State.EDIT;
  }

  async addEinsatz() {
    this.state = State.SUCCESS;
    this.createResult = await this.graphqlService.addEinsatz(this.helferId, this.einsatz);
    if (this.createResult.isSuccess) {
      this.state = State.SUCCESS;
      this.added.emit(this.helferId);
    }
    else {
      this.state = State.ERROR;
    }
  }
  newEinsatz() : void {
    let newEinsatz =  new EinsatzInputModel(this.vermitteltDurch);
    newEinsatz.hilfesuchender = this.einsatz.hilfesuchender;
    this.einsatz = newEinsatz;
    this.state = State.EDIT;
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
  }
  hilfesuchender: string;
  taetigkeit: Taetigkeit;
  anmerkungen: string;
  vermitteltDurch: string;
  
}
