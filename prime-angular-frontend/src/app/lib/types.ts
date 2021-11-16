/**
 * @description
 * Exclude non-function properties through distributive conditional types.
 * @see https://www.typescriptlang.org/docs/handbook/2/conditional-types.html#distributive-conditional-types
 */
export type FunctionPropertyNames<T> = { [K in keyof T]: T[K] extends Function ? K : never }[keyof T];
export type FunctionProperties<T> = Pick<T, FunctionPropertyNames<T>>;

/**
 * @description
 * Exclude function properties through distributive conditional types.
 * @see https://www.typescriptlang.org/docs/handbook/2/conditional-types.html#distributive-conditional-types
 */
export type NonFunctionPropertyNames<T> = { [K in keyof T]: T[K] extends Function ? never : K }[keyof T];
export type NonFunctionProperties<T> = Pick<T, NonFunctionPropertyNames<T>>;
