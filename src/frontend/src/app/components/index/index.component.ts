import { Component, OnInit } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Observable } from 'rxjs';
import { HelferListenEintrag, Taetigkeit } from 'src/app/models/graphql-models';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent implements OnInit {
  helfer$: Observable<HelferListenEintrag[]>;
  bezirke: any;
  displayedColumns: string[] = ['name', 'einsaetze', 'strasse', 'plz', 'anmerkung'];
  taetigkeiten = [{ name: "andere", id: Taetigkeit.ANDERE, checked: false },
  { name: "Besorgung", id: Taetigkeit.BESORGUNG, checked: false },
  { name: "Gassi gehen", id: Taetigkeit.GASSI_GEHEN, checked: false },
  { name: "Telefonkontakt", id: Taetigkeit.TELEFON_KONTAKT, checked: false }];
  hatAuto = false;
  istRisikoGruppe = false;
  gesetzeFilter: string[];

  constructor(private graphqlService: GraphqlService) {

  }

  ngOnInit(): void {
    this.helfer$ = this.graphqlService.queryHelferListe({});
    this.bezirke = [];
    for (let i = 1010; i <= 1230; i += 10) {
      this.bezirke.push({ name: `${i}`, checked: false, nummer: i });
    }
  }

  filterChange() {
    let inPlz = null;
    let taetigkeitIn = null
    let hatAuto = this.hatAuto ? true : null;
    let istRisikoGruppe = this.istRisikoGruppe ? true : null;
    if (this.bezirke.filter(b => b.checked).length > 0) {
      inPlz = this.bezirke.filter(v => v.checked).map(v => v.nummer);
    }
    if (this.taetigkeiten.filter(b => b.checked).length > 0) {
      taetigkeitIn = this.taetigkeiten.filter(v => v.checked).map(v => v.id);
    }
    this.gesetzeFilter = [
      ...this.bezirke.filter(v => v.checked).map(v => v.name),
      ...this.taetigkeiten.filter(v => v.checked).map(v => v.name),
      ...(hatAuto ? ["Hat Auto"] : []),
      ...(istRisikoGruppe ? ["Ist Risikogruppe"] : [])
    ];
    this.helfer$ = this.graphqlService.queryHelferListe({
      inPlz,
      taetigkeitIn,
      hatAuto,
      istRisikoGruppe
    });
  }
}
