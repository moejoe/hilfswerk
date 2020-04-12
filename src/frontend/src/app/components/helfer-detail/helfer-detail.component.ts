import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { HelferDetail, Taetigkeit } from 'src/app/models/graphql-models';
import { GraphqlService } from 'src/app/services/graphql.service';

@Component({
  selector: 'helfer-detail',
  templateUrl: './helfer-detail.component.html',
  styleUrls: ['./helfer-detail.component.scss']
})
export class HelferDetailComponent implements OnInit, OnChanges {

  @Input()
  public helfer: HelferDetail;

  taetigkeiten: string;
  labels: string;

  formatTaetigkeit(t: Taetigkeit) {
    switch (t) {
      case Taetigkeit.BESORGUNG:
        return "Besorgung";
      case Taetigkeit.GASSI_GEHEN:
        return "Gassi gehen";
      case Taetigkeit.TELEFON_KONTAKT:
        return "Telefonkontakt";
      case Taetigkeit.ANDERE:
      default:
        return "andere";
    }
  }

  constructor(private graphQlService: GraphqlService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (this.helfer) {
      this.taetigkeiten = this.helfer.taetigkeiten.map(this.formatTaetigkeit).join(", ");
      this.labels = [
        this.helfer.hatAuto ? "Auto verfügbar" : null,
        this.helfer.istFreiwilliger ? "Ist FW" : null,
        this.helfer.istZivildiener ? "Ist ZDL" : null,
        this.helfer.istRisikoGruppe ? "Ist Risikogruppe" : null,
      ].filter(v => v).join(", ");
    }
    else {
      this.taetigkeiten = this.labels = "";
    }
  }
  async removeEinsatz(einsatzId: string) {
    await this.graphQlService.removeEinsatz(this.helfer.id, einsatzId);
    let removedEinsatz = this.helfer.einsaetze.find(el => el.id == einsatzId);
    this.helfer.einsaetze.splice(this.helfer.einsaetze.indexOf(removedEinsatz), 1);
  }

  ngOnInit(): void {
    this.taetigkeiten = this.labels = "";
  }

}
