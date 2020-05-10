export enum Taetigkeit {
    BESORGUNG = "BESORGUNG",
    TELEFON_KONTAKT = "TELEFON_KONTAKT",
    GASSI_GEHEN = "GASSI_GEHEN",
    ANDERE = "ANDERE"
}

export interface HelferFilters {
    inPlz?: number[];
    taetigkeitIn?: Taetigkeit[];
    istRisikoGruppe?: boolean;
    hatAuto?: boolean;
    istZivildiener?: boolean;
    istFreiwilliger?: boolean;
    istAusgelastet?: boolean;
    istDSGVOKonform?: boolean;
}

export interface HelferListenEintrag {
    id: string;
    kontakt: Kontakt;
    totalEinsaetze: number;
    istAusgelastet?: boolean;
}

export interface HelferDetail {
    id: string;
    istRisikoGruppe?: boolean;
    hatAuto?: boolean;
    istZivildiener?: boolean;
    istFreiwilliger?: boolean;
    istDSGVOKonform?: boolean;
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
    dauer: number;
    id: string;
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
    istDSGVOKonform: boolean;
}

export interface EinsatzEditInput {
    anmerkungen: string;
    vermitteltAm: Date;
    dauer: number;
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
    istDSGVOKonform: boolean;
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
    dauer: number;
    vermitteltAm: Date;
}
export interface EinsatzCreateResult {
    hilfesuchender: string;
    taetigkeit: Taetigkeit;
    errors: { message: string }[];
    isSuccess: boolean;
}

export interface EinsatzEditResult {
    hilfesuchender: string;
    taetigkeit: Taetigkeit;
    errors: { message: string }[];
    isSuccess: boolean;
}

export interface ReportByMonth {
    helferInnenGesamt: number;
    helferInnenEingesetzt: number;
    groups: ReportGroupMonth[]
}
export interface ReportGroupMonth {
    year: number,
    month: number,
    helferInnen: number;
    details: ReportDetail[]
}
export interface ReportDetail {
    taetigkeit: Taetigkeit,
    einsaetze: number,
    helferInnen: number,
    dauer: number
}