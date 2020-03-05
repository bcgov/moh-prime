import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { Pagination } from '@core/models/pagination.model';

export interface CrudHttpResponse<T> extends ApiHttpResponse<T>, Pagination { }
