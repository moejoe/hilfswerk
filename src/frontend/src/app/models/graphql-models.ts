export enum Taetigkeit {
    BESORGUNG = "BESORGUNG",
    TELEFON_KONTAKT = "TELEFON_KONTAKT",
    GASSI_GEHEN = "GASSI_GEHEN",
    ANDERE = "ANDERE"
}

export interface HelferFilters {
    inPlz?: number;
    taetigkeitIn?: Taetigkeit[];
    istRisikoGruppe?: boolean;
    hatAuto?: boolean;
}

export interface HelferListenEintrag {
    kontakt: Kontakt;
    totalEinsaetze: number;
    anmerkung: string;
}

export interface Kontakt {
    vorname: string;
    nachname: string;
    strasse: string;
    plz: number;
}