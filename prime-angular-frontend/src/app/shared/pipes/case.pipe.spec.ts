import { CasePipe } from './case.pipe';

describe('CasePipe', () => {
  let pipe: CasePipe;
  beforeEach(() => pipe = new CasePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should convert from space case to kebab case', () => {
    const result = pipe.transform('kebab case', 'space', 'kebab');
    expect(result).toBe('kebab-case');
  });

  it('should convert from snake case to space case', () => {
    const result = pipe.transform('space_case', 'space', 'kebab');
    expect(result).toBe('space case');
  });

  it('should not fail when passsed an empty string', () => {
    const result = pipe.transform('', 'space', 'kebab');
    expect(result).toBe('');
  });

  it('should not fail when no conversion exists between case types', () => {
    const result = pipe.transform('KebabCase', 'pascal', 'kebab');
    expect(result).toBe('KebabCase');
  });
});
