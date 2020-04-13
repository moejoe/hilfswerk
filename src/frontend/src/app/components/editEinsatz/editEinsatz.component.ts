import { Component, OnInit, OnDestroy, ViewChild, ChangeDetectorRef } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Taetigkeit, EinsatzEditInput, EinsatzListenEintrag } from 'src/app/models/graphql-models';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Location } from '@angular/common';
import { NgForm } from "@angular/forms";

@Component({
  selector: 'app-editEinsatz',
  templateUrl: './editEinsatz.component.html',
  styleUrls: ['./editEinsatz.component.scss']
})
export class EditEinsatzComponent implements OnInit, OnDestroy {
  
  helferId: string;
  einsatzId: string;
  einsatz: EinsatzEditModel;
  paramSubscription: Subscription;
  dauer = {
    stunden: 0,
    minuten: 0,
  };

  @ViewChild("editEinsatzForm") editEinsatzForm: NgForm;

  constructor(private graphqlService: GraphqlService, private route: ActivatedRoute, private location: Location, private changeDetectorRef: ChangeDetectorRef) {
  }
  ngOnDestroy(): void {
    this.paramSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.einsatz = new EinsatzEditModel();
    this.paramSubscription = this.route.params.subscribe(params => {
      this.helferId = params['helferId'];
      this.einsatzId = params['einsatzId'];
      if (this.helferId && this.einsatzId) {
        this.graphqlService.getEinsatz(this.helferId, this.einsatzId).then(p => {
          this.einsatz = { ...this.einsatz, ...p }
          this.einsatz.vermitteltAm = new Date(this.einsatz.vermitteltAm);
          this.dauer.stunden = Math.floor(this.einsatz.dauer / 3600);
          this.dauer.minuten = Math.floor((this.einsatz.dauer - this.dauer.stunden*3600) / 60);
        });
      }
    });
    
  }

  async editEinsatz() {
    let result = await this.graphqlService.editEinsatz(this.helferId, this.einsatzId, {
      "vermitteltAm": this.einsatz.vermitteltAm,
      "anmerkungen": this.einsatz.anmerkungen,
      "dauer": this.einsatz.dauer
    });
    if (result.isSuccess) {
      this.back();
    }
  }
  back(): void {
    this.location.back();
  }
  onDurationChanged() {
    this.einsatz.dauer = (this.dauer.stunden || 0 )*3600 + (this.dauer.minuten || 0 )*60;
  }
}
class EinsatzEditModel implements EinsatzListenEintrag {
  constructor() {
    this.vermitteltAm = new Date();
    this.anmerkungen = "";
    this.dauer = 0;
    this.vermitteltDurch = "n/a";
    this.hilfesuchender = "n/a";
    this.id = "";
  }
  id: string;
  hilfesuchender: string;
  taetigkeit: Taetigkeit;
  vermitteltDurch: string;
  vermitteltAm: Date;
  anmerkungen: string;
  dauer: number;

}
