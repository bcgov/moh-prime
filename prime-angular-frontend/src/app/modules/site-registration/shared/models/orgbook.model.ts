/**
 * Interface may only contain the minimum keys to integrate OrgBook.
 * @see https://www.orgbook.gov.bc.ca/api
 */
export interface OrgBookAutocompleteHttpResponse {
  first_index: number;
  last_index: number;
  results: OrgBookAutocompleteResult[];
  total: number;
}

/**
 * Interface may only contain the minimum keys to integrate OrgBook.
 * @see https://www.orgbook.gov.bc.ca/api
 */
export interface OrgBookAutocompleteResult {
  id: number;
  inactive: false;
  names: {
    credential_id: number;
    id: number;
    language: string;
    text: string;
    type: string;
  }[];
}

/**
 * Interface may only contain the minimum keys to integrate OrgBook.
 * @see https://www.orgbook.gov.bc.ca/api
 */
export interface OrgBookFacetHttpResponse {
  facets: any;
  objects: {
    results: [
      {
        topic: {
          id: number;
          source_id: string; // Registration ID
        };
        related_topics: any[];
      }
    ]
  };
}

/**
 * Interface may only contain the minimum keys to integrate OrgBook.
 * @see https://www.orgbook.gov.bc.ca/api
 */
export interface OrgBookDetailHttpResponse {
  id: number; // topic ID URL param for API requests
  create_timestamp: string;
  source_id: string;
  type: string; // URL param: `registration`
  names: {
    id: number,
    credential_id: number,
    last_updated: string;
    inactive: boolean;
    text: string;
    language: string;
    issuer: any,
    type: string;
  }[];
  addresses: any[];
  attributes: {
    id: number;
    credential_id: number;
    credential_type_id: 1;
    last_updated: string;
    inactive: boolean;
    type: string;
    format: string;
    value: string;
  }[];
}

/**
 * Interface may only contain the minimum keys to integrate OrgBook.
 * @see https://www.orgbook.gov.bc.ca/api
 */
export interface OrgBookRelatedHttpResponse {
  topic_id: number;
  relation_id: number;
  credential: any;
  topic: {
    id: number;
    source_id: string;
    type: string;
    names: {
      id: number;
      credential_id: number;
      last_updated: string;
      inactive: boolean;
      text: string;
      language: any;
      issuer: any;
      type: string;
    }[];
  };
  related_topic: {
    id: number;
    type: string; // URL param: registration
    names: {
      id: number;
      text: string; // Name parameter
    }[];
    addresses: any[];
    attributes: {
      id: number;
      credential_id: number;
      credential_type_id: number;
      last_updated: string;
      inactive: boolean;
      type: string; // Possible type: relationship_description
      format: string;
      value: string;
    }[]
  };
  attributes: {
    id: number;
    type: string; // Possible type: relationship_description
    format: string;
    value: string; // Possible value: `Does Business As`
    credential_id: number;
  }[];
}
