import { RouteUtils } from '@lib/utils/route-utils.class';

describe('RouteUtils', () => {
  const util = RouteUtils;

  describe('currentRoutePath', () => {
    it('should provide the current route', () => {
      const currentRoute = 'currentRoute';
      const urls = [
        `/module/${currentRoute}`,
        `/module/${currentRoute}?filter=status`,
        `/module/${currentRoute}/1`,
        `/module/${currentRoute}/1?filter=status`,
        `/module/models/1/${currentRoute}?id=new`
      ];
      const results = urls.map(url => util.currentRoutePath(url));
      expect(results).toEqual([...Array(urls.length)].map(_ => currentRoute));
    });
  });
});
