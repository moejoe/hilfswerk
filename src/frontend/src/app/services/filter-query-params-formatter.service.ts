import { Injectable } from '@angular/core';
import { HelferFilters, Taetigkeit } from '../models/graphql-models';
import { Params } from '@angular/router';

type HelferBooleanProps = Pick<HelferFilters, "hatAuto" |
  "istFreiwilliger" |
  "istRisikoGruppe" |
  "istZivildiener">;

const booleanProps: (keyof HelferBooleanProps)[] = ["hatAuto",
  "istFreiwilliger",
  "istRisikoGruppe",
  "istZivildiener"];

const plzParam = "plz";
const taetigkeitInParam = "taetigkeitIn";
const seperator = "-";

@Injectable({
  providedIn: 'root'
})
export class FilterQueryParamsFormatterService {

  constructor() { }

  fromQueryParams(params: Params): HelferFilters {
    let filters: HelferFilters = {};
    for (let p of booleanProps) {
      if (params[p] == "1") {
        filters[p] = true;
      }
    }
    if (params[plzParam]) {
      filters.inPlz = (<string>params[plzParam]).split(seperator).map(b => parseInt(b));
    }
    if (params[taetigkeitInParam]) {
      filters.taetigkeitIn = <Taetigkeit[]>(<string>params[taetigkeitInParam]).split(seperator);
    }
    return filters;
  }

  toQueryParams(filter: HelferFilters): Params {
    let params: Params = {};
    for (let p of booleanProps) {
      if (filter[p]) {
        params[p] = 1;
      }
    }
    if (filter.inPlz && filter.inPlz.length) {
      params[plzParam] = filter.inPlz.join(seperator);
    }
    if (filter.taetigkeitIn && filter.taetigkeitIn.length) {
      params[taetigkeitInParam] = filter.taetigkeitIn.join(seperator);
    }
    return params;
  }
}
