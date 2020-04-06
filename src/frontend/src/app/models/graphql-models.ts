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
    istAusgelastet?: boolean;
}

export interface HelferListenEintrag {
    id: string;
    kontakt: Kontakt;
    totalEinsaetze: number;
    istAusgelastet?: boolean;
}

export interface HelferDetail {
    id : string;
    istRisikoGruppe?: boolean;
    hatAuto?: boolean;
    istZivildiener?: boolean;
    istFreiwilliger?: boolean;
    anmerkung: string;
    taetigkeiten: Taetigkeit[];
    kontakt: Kontakt;
    einsaetze: EinsatzListenEintrag[];
}

export interface EinsatzListenEintrag {
    hilfesuchender: string;
    taetigkeit: Taetigkeit;
    anmerkungen: string;
    vermitteltDurch: string;
    vermitteltAm: Date;
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

export interface HelferEditInput {
    istRisikogruppe: boolean;
    hatAuto: boolean;
    anmerkung: string;
    taetigkeiten: Taetigkeit[];
    kontakt: Kontakt;
    istZivildiener: boolean;
    istFreiwilliger: boolean;
    istAusgelastet: boolean;
}

export interface HelferCreateResult {
    id: string;
    kontakt: Kontakt;
    errors: { message: string }[];
    isSuccess: boolean;
}

export interface HelferEditResult {
    isSuccess: boolean;
}

export interface EinsatzInput {
    hilfesuchender: string;
    taetigkeit: Taetigkeit;
    anmerkungen: string;
    vermitteltDurch: string;
    helferAusgelastet: boolean;
    stunden: number;
}
export interface EinsatzCreateResult {
    hilfesuchender: string;
    taetigkeit: Taetigkeit;
    errors: { message: string }[];
    isSuccess: boolean;
}
