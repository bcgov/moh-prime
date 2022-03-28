import { Pagination } from './pagination.model';

export interface PaginatedList<T> extends Pagination {
  results: T[];
}
