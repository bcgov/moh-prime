export class Pagination {
  public page?: number;
  public perPage?: number;
  public total?: number;

  constructor({ page, perPage: perPage }: Partial<Pagination> = { page: 1, perPage: 20 }) {
    this.page = page || 1;
    this.perPage = perPage || 20;
    this.total = 0;
  }
}
