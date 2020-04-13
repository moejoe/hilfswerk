import { Component, OnInit, OnDestroy } from '@angular/core';
import { GraphqlService } from 'src/app/services/graphql.service';
import { Subscription } from 'rxjs';
import { HelferListenEintrag, Taetigkeit, HelferDetail, HelferFilters } from 'src/app/models/graphql-models';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { AuthService, UserInfo } from 'src/app/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FilterQueryParamsFormatterService } from 'src/app/services/filter-query-params-formatter.service';
import { NavigationPresitenceService } from 'src/app/services/navigation-presitence.service';
import { tap } from 'rxjs/operators';

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
  helfer: HelferListenEintrag[];
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
  nichtDSGVOKonform = false;
  gesetzeFilter: string[];
  selectedHelfer: HelferListenEintrag | null;
  selectedHelferDetail: HelferDetail | null;
  userinfo: UserInfo;
  firstTimeUrlFiltersOnly = false;
  sub: Subscription;

  constructor(private graphqlService: GraphqlService,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private queryParamsService: FilterQueryParamsFormatterService,
    private navigationPersistence: NavigationPresitenceService) {

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
        let querySub = this.graphqlService.queryHelferListe(filters)
          // side-effect: select the "last helfer" again
          .pipe(tap(helfer => {
            let lastHelferId = this.navigationPersistence.getLastHelfer();
            if (lastHelferId) {
              let eintrag = helfer.find(d => d.id == lastHelferId);
              if (eintrag) {
                this.selectHelfer(eintrag);
              }
            }
          })).subscribe(helfer => {
            this.helfer = helfer;
            querySub.unsubscribe();
          });
        this.updateFilterUi(filters);
      }
    });
  }

  private updateFilterUi(filters: HelferFilters) {
    if (filters.inPlz) {
      for (let plz of filters.inPlz) {
        this.bezirke.find(b => b.nummer == plz).checked = true;
      }
    }
    this.istFreiwilliger = filters.istFreiwilliger;
    this.istRisikoGruppe = filters.istRisikoGruppe;
    this.hatAuto = filters.hatAuto;
    this.istZivildiener = filters.istZivildiener;
    this.nichtDSGVOKonform = filters.istDSGVOKonform === false;

    if (filters.taetigkeitIn) {
      for (let t of filters.taetigkeitIn) {
        this.taetigkeiten.find(b => b.id == t).checked = true;
      }
    }
    this.updateGesetzteFilter();
  }

  onEinsatzAdded() {
    this.selectedHelfer.totalEinsaetze++;
  }

  private async selectHelfer(eintrag: HelferListenEintrag) {
    this.selectedHelfer = this.selectedHelfer === eintrag ? null : eintrag;
    this.selectedHelferDetail = null;
    if (null != this.selectedHelfer) {
      var detail = await this.graphqlService.getHelferDetail(this.selectedHelfer.id);
      //sort einsätze by date desc.
      detail.einsaetze = detail.einsaetze.sort((p1, p2) => new Date(p2.vermitteltAm).getTime() - new Date(p1.vermitteltAm).getTime());
      this.selectedHelferDetail = detail;
      this.navigationPersistence.storeLastHelfer(this.selectedHelfer.id);
    }
  }

  async rowClick(row: HelferListenEintrag) {
    await this.selectHelfer(row);
  }

  updateGesetzteFilter() {
    this.gesetzeFilter = [
      ...this.bezirke.filter(v => v.checked).map(v => v.name),
      ...this.taetigkeiten.filter(v => v.checked).map(v => v.name),
      ...(this.hatAuto ? ["Auto verfügbar"] : []),
      ...(this.istRisikoGruppe ? ["Ist Risikogruppe"] : []),
      ...(this.istFreiwilliger ? ["Ist FW"] : []),
      ...(this.istZivildiener ? ["Ist ZDL"] : []),
      ...(this.nichtDSGVOKonform ? ["DSVGO fehlt"] : [])
    ];
  }

  filterChange() {
    this.navigationPersistence.clearLastHelfer();
    let inPlz = null;
    let taetigkeitIn = null
    let hatAuto = this.hatAuto ? true : null;
    let istRisikoGruppe = this.istRisikoGruppe ? true : null;
    let istZivildiener = this.istZivildiener ? true : null;
    let istFreiwilliger = this.istFreiwilliger ? true : null;
    let istDSGVOKonform = this.nichtDSGVOKonform === true ? false : null;

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
      istAusgelastet: false,
      istDSGVOKonform
    };
    let sub = this.graphqlService.queryHelferListe(filter).subscribe(helfer => {
      this.helfer = helfer;
      sub.unsubscribe();
    })
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams: this.queryParamsService.toQueryParams(filter),
        replaceUrl: true
      });
  }
}
