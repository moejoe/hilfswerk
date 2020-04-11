import { Component, OnInit, OnDestroy } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Observable, Subscription } from 'rxjs';
import { HelferListenEintrag, Taetigkeit, HelferDetail, HelferFilters } from 'src/app/models/graphql-models';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { AuthService, UserInfo } from 'src/app/services/auth.service';
import { Router, Params, ActivatedRoute } from '@angular/router';
import { FilterQueryParamsFormatterService } from 'src/app/services/filter-query-params-formatter.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class IndexComponent implements OnInit, OnDestroy {
  helfer$: Observable<HelferListenEintrag[]>;
  bezirke: any;
  displayedColumns: string[] = ['name', 'einsaetze', 'strasse', 'plz'];
  taetigkeiten = [{ name: "andere", id: Taetigkeit.ANDERE, checked: false },
  { name: "Besorgung", id: Taetigkeit.BESORGUNG, checked: false },
  { name: "Gassi gehen", id: Taetigkeit.GASSI_GEHEN, checked: false },
  { name: "Telefonkontakt", id: Taetigkeit.TELEFON_KONTAKT, checked: false }];
  hatAuto = false;
  istRisikoGruppe = false;
  istZivildiener = false;
  istFreiwilliger = false;
  gesetzeFilter: string[];
  selectedHelfer: HelferListenEintrag | null;
  selectedHelferDetail: HelferDetail | null;
  userinfo: UserInfo;
  firstTimeUrlFiltersOnly = false;
  sub: Subscription;

  constructor(private graphqlService: GraphqlService, private authService: AuthService,
    private router: Router, private activatedRoute: ActivatedRoute, private queryParamsService: FilterQueryParamsFormatterService) {

  }
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  async ngOnInit() {
    this.bezirke = [];
    for (let i = 1010; i <= 1230; i += 10) {
      this.bezirke.push({ name: `${i}`, checked: false, nummer: i });
    }
    this.authService.getUserInfo().subscribe(resp => {
      this.userinfo = { ...resp.body };
    });
    this.sub = this.activatedRoute.queryParams.subscribe(params => {
      if (!this.firstTimeUrlFiltersOnly) {
        this.firstTimeUrlFiltersOnly = true;
        let filters = this.queryParamsService.fromQueryParams(params);
        filters.istAusgelastet = false;
        this.helfer$ = this.graphqlService.queryHelferListe(filters);
        if (filters.inPlz) {
          for (let plz of filters.inPlz) {
            this.bezirke.find(b => b.nummer == plz).checked = true;
          }
        }
        this.istFreiwilliger = filters.istFreiwilliger;
        this.istRisikoGruppe = filters.istRisikoGruppe;
        this.hatAuto = filters.hatAuto;
        this.istZivildiener = filters.istZivildiener;
        if (filters.taetigkeitIn) {
          for (let t of filters.taetigkeitIn) {
            this.taetigkeiten.find(b => b.id == t).checked = true;
          }
        }
        this.updateGesetzteFilter();
      }
    });
  }

  onEinsatzAdded(event: string) {
    this.selectedHelfer.totalEinsaetze++;
  }

  async rowClick(row: HelferListenEintrag) {
    this.selectedHelfer = this.selectedHelfer === row ? null : row;
    this.selectedHelferDetail = null;
    if (null != this.selectedHelfer) {
      var detail = await this.graphqlService.getHelferDetail(this.selectedHelfer.id);
      //sort einsätze by date desc.
      detail.einsaetze = detail.einsaetze.sort((p1, p2) => new Date(p2.vermitteltAm).getTime() - new Date(p1.vermitteltAm).getTime());
      this.selectedHelferDetail = detail;
    }
  }

  updateGesetzteFilter() {
    this.gesetzeFilter = [
      ...this.bezirke.filter(v => v.checked).map(v => v.name),
      ...this.taetigkeiten.filter(v => v.checked).map(v => v.name),
      ...(this.hatAuto ? ["Auto verfügbar"] : []),
      ...(this.istRisikoGruppe ? ["Ist Risikogruppe"] : []),
      ...(this.istFreiwilliger ? ["Ist FW"] : []),
      ...(this.istZivildiener ? ["Ist ZDL"] : [])
    ];
  }

  filterChange() {
    let inPlz = null;
    let taetigkeitIn = null
    let hatAuto = this.hatAuto ? true : null;
    let istRisikoGruppe = this.istRisikoGruppe ? true : null;
    let istZivildiener = this.istZivildiener ? true : null;
    let istFreiwilliger = this.istFreiwilliger ? true : null;
    if (this.bezirke.filter(b => b.checked).length > 0) {
      inPlz = this.bezirke.filter(v => v.checked).map(v => v.nummer);
    }
    if (this.taetigkeiten.filter(b => b.checked).length > 0) {
      taetigkeitIn = this.taetigkeiten.filter(v => v.checked).map(v => v.id);
    }
    this.updateGesetzteFilter();
    let filter: HelferFilters = {
      inPlz,
      taetigkeitIn,
      hatAuto,
      istRisikoGruppe,
      istZivildiener,
      istFreiwilliger,
      istAusgelastet: false
    };
    this.helfer$ = this.graphqlService.queryHelferListe(filter);
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams: this.queryParamsService.toQueryParams(filter),
        replaceUrl: true
      });
  }
}
