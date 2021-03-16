import { FullnamePipe } from './fullname.pipe';

describe('FullnamePipe', () => {
  let pipe: FullnamePipe;
  beforeEach(() => pipe = new FullnamePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should concatenate first and last name', () => {
    const value = { firstName: 'Roger', lastName: 'Rabbit' };
    const result = pipe.transform(value);
    expect(result).toBe(`${value.firstName} ${value.lastName}`);
  });

  it('should not concatenate first and last name when first name is missing', () => {
    const value = { firstName: '', lastName: 'Rabbit' };
    const result = pipe.transform(value);
    expect(result).toBe('');
  });

  it('should not concatenate first and last name when last name is missing', () => {
    const value = { firstName: 'Roger', lastName: '' };
    const result = pipe.transform(value);
    expect(result).toBe('');
  });

  it('should not fail when passed a null', () => {
    const value = null;
    const result = pipe.transform(value);
    expect(result).toBeNull();
  });
});
