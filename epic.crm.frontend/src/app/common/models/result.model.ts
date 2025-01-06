export class Result {
  resultStatus!: ResultStatusEnum;
  errors!: string[];
}

export class DataResult<T> extends Result {
  data!: T | null;
}

export class QueryParams {
  sortOrder!: string | null;
  sortColumn!: string | null;
  searchString!: string | null;
  pageSize!: number | null;
  pageIndex!: number | null;
}

export class PageResult<T> extends DataResult<T> {
  queryParams!: QueryParams;
  itemCount!: number | null;
}

export enum ResultStatusEnum {
  Success,
  Fail
}
