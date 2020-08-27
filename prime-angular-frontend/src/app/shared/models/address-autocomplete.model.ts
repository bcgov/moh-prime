export interface AddressAutocompleteFindResponse {
  id: string;
  text: string;
  highlight: string;
  cursor: number;
  description: string;
  next: string;
}

export interface AddressAutocompleteRetrieveResponse {
  id: string;
  domesticId: string;
  language: string;
  languageAlternatives: string;
  department: string;
  company: string;
  subBuilding: string;
  buildingNumber: string;
  buildingName: string;
  secondaryStreet: string;
  street: string;
  block: string;
  neighbourhood: string;
  district: string;
  city: string;
  line1: string;
  line2: string;
  line3: string;
  line4: string;
  line5: string;
  adminAreaName: string;
  adminAreaCode: string;
  province: string;
  provinceName: string;
  provinceCode: string;
  postalCode: string;
  countryName: string;
  countryIso2: string;
  countryIso3: string;
  countryIsoNumber: number;
  sortingNumber1: string;
  sortingNumber2: string;
  barcode: string;
  poBoxNumber: string;
  label: string;
  dataLevel: string;
}
