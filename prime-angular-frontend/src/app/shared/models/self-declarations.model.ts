export class SelfDeclaration {
  constructor(
    public selfDeclarationTypeCode: number,
    public selfDeclarationDetails: string,
    public documentGuids?: string[],
    public enrolleeId?: number,
    public id?: number
  ) { }
}
