export abstract class BaseRoute {
  static MODULE_PATH = '/';
  static routePath(route: string): string {
    return `/${BaseRoute.MODULE_PATH}/${route}`;
  }
}
