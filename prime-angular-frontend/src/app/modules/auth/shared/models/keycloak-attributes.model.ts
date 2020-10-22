export interface KeycloakAttributes {
  attributes: {
    birthdate: string[];
    country: string[];
    region: string[]; // Province
    streetAddress: string[];
    locality: string[]; // City
    postalCode: string[];
    givenNames: string[];
  };
}
