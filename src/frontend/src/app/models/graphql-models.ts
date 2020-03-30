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
    istZivildiener?: boolean;
    istFreiwilliger?: boolean;
}

export interface HelferListenEintrag {
    id: string;
    kontakt: Kontakt;
    totalEinsaetze: number;
    anmerkung: string;
}

export interface Kontakt {
    vorname: string;
    nachname: string;
    plz: number;
    strasse: string;
    telefon: string;
    email: string;
}

export interface HelferCreateInput {
    istRisikogruppe: boolean;
    hatAuto: boolean;
    anmerkung: string;
    taetigkeiten: Taetigkeit[];
    kontakt: Kontakt;
    istZivildiener: boolean;
    istFreiwilliger: boolean;
}

export interface HelferCreateResult {
    id: string;
    kontakt: Kontakt;
    errors: { message: string }[];
    isSuccess: boolean;
}

export interface EinsatzInput {
    hilfesuchender: string;
    taetigkeit: Taetigkeit;
    anmerkungen: string;
    vermitteltDurch: string;
}
export interface EinsatzCreateResult {
    hilfesuchender: string;
    taetigkeit: Taetigkeit;
    errors: { message: string }[];
    isSuccess: boolean;
}
