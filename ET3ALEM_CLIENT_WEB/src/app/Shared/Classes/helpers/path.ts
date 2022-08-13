export class Path {
  public static join(...paths: string[]): string {
    let res = '';
    for (let path of paths) {
      if (path.endsWith('/')) {
        path = path.slice(0, -1);
      }
      res += path + '/';
    }
    return res;
  }
}
